using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Object : ISerializable
    {
        private static readonly string s_sepObj = "#[O]#";
        private static readonly string s_indent = "\t\t";
        private static readonly string s_nl = Environment.NewLine;

        private string m_name;
        private string m_type;
        private bool m_isString;
        private readonly string m_serializedProperties;
        private List<Property> m_properties;

        public string Name { get => m_name; set => m_name = value; }
        public string Type { get => m_type; set => m_type = value; }

        public Object(string type, string name, bool isString)
        {
            m_type = type;
            m_name = name;
            m_isString = isString;
            m_serializedProperties = string.Empty;
            m_properties = new List<Property>();
        }

        public Object(string type, string name, string properties) : this(type, name, properties, false) { }

        public Object(string type, string name, string properties, bool isString) : this(type, name, isString)
        {
            m_serializedProperties = properties;
        }

        public Object(string type, string name, List<Property> properties) : this(type, name, false)
        {
            if (properties != null)
                m_properties.AddRange(properties);
        }

        public Object() : this(string.Empty, string.Empty, false) { }

        public string Serialize()
        {
            string s = m_type + s_sepObj + m_name + s_sepObj + m_isString.ToString();
            s += s_sepObj + m_serializedProperties;
            return s;
        }

        public static Object Deserialize(string stream)
        {
            List<Property> properties = null;

            string[] fragments = stream.Split(new[] { s_sepObj }, StringSplitOptions.None);
            string type = fragments[0];
            string name = fragments[1];
            if (!string.IsNullOrEmpty(fragments[3]))
            {
                //do not deserialize properties if object is of type String
                //in this case, the fragment contains just the string content for direct use
                if (Convert.ToBoolean(fragments[2]))
                    return new Object(type, name, fragments[3], true);
                else
                    properties = Property.DeserializeList(fragments[3]);
            }
            return new Object(type, name, properties);
        }

        public string Format()
        {
            string properties;
            //string carries text instead of (serialized) properties
            if (m_isString)
                properties = m_serializedProperties;
            else
                properties = ProcessProperties();

            string o = string.Empty;
            string scope = "public static ";

            switch (m_type)
            {
                case "Synonym":
                    o += s_indent + scope + properties + ";";
                    break;

                case "String":
                    o += s_indent + scope + "string " + m_name;
                    if (!string.IsNullOrEmpty(properties))
                        o += " = " + properties;
                    o += ";";
                    break;

                default:
                    //Ways never have properties, but need to be instantiated nonetheless
                    //if (!string.IsNullOrEmpty(props) || string.Compare(type, "Way", true) == 0 || string.Compare(type, "Skill", true) == 0)
                    {
                        o += s_indent + scope + m_type + " " + m_name + " = new " + m_type + "()";

                        if (!string.IsNullOrEmpty(properties))
                        {
                            o += s_nl + s_indent + "{" + s_nl;
                            o += properties + s_nl;
                            o += s_indent + "}";
                        }
                        o += ";";
                    }
                    break;
            }

            return o;
        }

        private string ProcessProperties()
        {
            string p = string.Empty;
            PreProcessorStack<ObjectData> stack = new PreProcessorStack<ObjectData>();
            ObjectData objectData = stack.Content;

            foreach (Property property in m_properties)
            {
                switch (property.Name)
                {
                    case "#if":
                        //update preprocessor stack and obtain new dataset
                        objectData = stack.Update(property.Name, property.Values[0]);
                        break;

                    case "#else":
                        //move all previously collected properties into data list
                        objectData.PropertyData = ProcessObjectData(objectData);
                        //update preprocessor stack and move to else branch of active dataset
                        objectData = stack.Update(property.Name);
                        break;

                    case "#endif":
                        //move all previously collected properties into data list
                        objectData.PropertyData = ProcessObjectData(objectData);
                        //update preprocessor stack, get previous dataset
                        objectData = stack.Update(property.Name);
                        break;

                    default:
                        //add property to active dataset
                        AddProperty(property, objectData.Properties);
                        break;
                }
            }

            //take care of properties not enclosed by any preprocessor directive
            if (string.IsNullOrEmpty(stack.Condition))
            {
                objectData.PropertyData = ProcessObjectData(objectData);
            }

            objectData = stack.Merge();

            //copy standard properties to output
            p += objectData.PropertyData.Properties;

            //handle palette range definitions
            if (!string.IsNullOrEmpty(objectData.PropertyData.Ranges))
            {
                p += s_nl + s_indent + "\tRange = new[,]" + s_nl + s_indent + "\t{" + s_nl + objectData.PropertyData.Ranges;
                p += s_nl + s_indent + "\t}";
            }

            //handle UI control definitions
            if (!string.IsNullOrEmpty(objectData.PropertyData.Controls))
            {
                p += s_nl + s_indent + "\tControls = new UIControl[]" + s_nl + s_indent + "\t{ " + s_nl + objectData.PropertyData.Controls;
                p += s_nl + s_indent + "\t}";
            }

            return p;
        }

        private PropertyData ProcessObjectData(ObjectData active)
        {
            //Synonyms need special treatment: convert from WDL object with properties to C# object reference
            if (m_type.Equals("Synonym"))
                return ProcessSynonym(active);
            else
                return ProcessProperties(active);

        }

        private PropertyData ProcessSynonym(ObjectData active)
        {
            //Current implementation is not compatible with preprocessor directives - most likely not relevant for any A3 game ever created
            Property property;
            PropertyData data = new PropertyData();
            //workaround build synonym definition in property
            //Type declares datatype of Synonym
            property = active.Properties.Where(x => x.Name.Equals("Type")).FirstOrDefault();
            if (property != null)
            {
                string synType = Formatter.FormatObject(property.Values[0]);
                //"Action" keyword is reserved in C# -> use "Function" instead (mandatory)
                if (synType.Equals("Action"))
                    synType = "Function";
                //Scripts don't distinguish between object types and just use BaseObject which just carries all properties - like WDL
                //Trying to keep this more strict results in all kind of type problems in WDL actions
                if (synType.Equals("Wall") || synType.Equals("Thing") || synType.Equals("Actor"))
                    synType = "BaseObject";

                data.Properties = synType + " " + m_name;

                //Default declares default assignment of Synonym (optional)
                property = active.Properties.Where(x => x.Name.Equals("Default")).FirstOrDefault();
                if (property != null)
                {
                    if (!property.Values[0].Equals("null"))
                    {
                        data.Properties += " = " + Formatter.FormatIdentifier(property.Values[0]);
                    }
                }
            }

            return data;
        }

        private PropertyData ProcessProperties(ObjectData active)
        {
            PropertyData data = new PropertyData();

            List<string> ranges = new List<string>();
            List<string> controls = new List<string>();
            List<string> properties = new List<string>();

            foreach (Property property in active.Properties)
            {
                switch (property.Name)
                {
                    case "Range":
                        ranges.Add(property.Format(m_type));
                        break;

                    case "Digits":
                    case "Hbar":
                    case "Vbar":
                    case "Hslider":
                    case "Vslider":
                    case "Picture":
                    case "Window":
                    case "Button":
                        controls.Add(property.Format(m_type));
                        break;

                    default:
                        properties.Add(property.Format(m_type));
                        break;
                }
            }

            //create comma separated list of ranges
            ranges.Sort();
            data.Ranges += string.Join(s_nl, ranges.Select(x => s_indent + "\t\t" + x + ","));

            //create comma separated list of controls
            controls.Sort();
            data.Controls += string.Join(s_nl, controls.Select(x => s_indent + "\t\t" + x + ","));

            //create comma separated list of properties
            properties.Sort();
            data.Properties += string.Join(s_nl, properties.Select(x => s_indent + "\t" + x + ","));

            return data;
        }

        private void AddProperty(Property property, List<Property> properties)
        {
            List<string> propertyNames = properties.Select(x => x.Name).ToList();
            //let preprocessor instructions always pass check
            if (propertyNames.Contains(property.Name))
            {
                //Eliminate double definitions of properties only where their values can be merged
                if (property.AllowMerge)
                {
                    int i = propertyNames.IndexOf(property.Name);
                    properties[i].Values.AddRange(property.Values);
                }
                else if (property.AllowMultiple)
                {
                    properties.Add(property);
                }
                else
                {
                    Console.WriteLine("(W) OBJECTS ignore double definition of property: " + property.Name);
                }
            }
            else
            {
                properties.Add(property);
            }
        }
    }
}
