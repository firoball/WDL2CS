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
        private static readonly string s_indentInit = "\t\t\t";
        private static readonly string s_nl = Environment.NewLine;

        private string m_name;
        private string m_type;
        private bool m_isString;
        private readonly bool m_isInitialized;
        private readonly string m_serializedProperties;
        private List<Property> m_properties;

        public string Name { get => m_name; set => m_name = value; }

        public Object(string type, string name, bool isString)
        {
            m_type = type;
            m_name = name;
            m_isString = isString;
            m_serializedProperties = string.Empty;
            m_properties = new List<Property>();
            //global skills are initialized
            if (m_name.StartsWith("Skills."))
                m_isInitialized = true;
            //user defined skills are allocated statically
            else
                m_isInitialized = false;
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

        public bool IsInitialized()
        {
            return m_isInitialized;
        }

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
            if ((fragments.Length > 3) && !string.IsNullOrEmpty(fragments[3]))
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
                        string indent = s_indent;
                        if (!IsInitialized())
                        {
                            indent = s_indent;
                            o += indent + scope + m_type + " ";
                        }
                        else
                        {
                            indent = s_indentInit;
                            o += indent;
                        }

                        o += m_name + " = new " + m_type + "()";

                        if (!string.IsNullOrEmpty(properties))
                        {
                            o += s_nl + indent + "{" + s_nl;
                            o += properties + s_nl;
                            o += indent + "}";
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
            objectData.ParentObject = this; //required for callback in objectData.Format()

            foreach (Property property in m_properties)
            {
                switch (property.Name)
                {
                    case "#if":
                        //update preprocessor stack and obtain new dataset
                        objectData = stack.Update(property.Name, property.Values[0]);
                        objectData.ParentObject = this; //required for callback in objectData.Format()
                        break;

                    case "#else":
                        //update preprocessor stack and move to else branch of active dataset
                        objectData = stack.Update(property.Name);
                        objectData.ParentObject = this; //required for callback in objectData.Format()
                        break;

                    case "#endif":
                        //update preprocessor stack, get previous dataset
                        objectData = stack.Update(property.Name);
                        objectData.ParentObject = this; //required for callback in objectData.Format()
                        break;

                    default:
                        //add property to active dataset
                        AddProperty(property, objectData.Properties);
                        break;
                }
            }

            objectData = stack.Merge();

            //copy standard properties to output
            p += objectData.PropertyStream;

            //handle palette range definitions
            if (objectData.RangeStream.Length > 0)
            {
                p += s_nl + s_indent + "\tRange = new[,]" + s_nl + s_indent + "\t{" + s_nl + objectData.RangeStream;
                p += s_nl + s_indent + "\t}";
            }

            //handle UI control definitions
            if (objectData.ControlStream.Length > 0)
            {
                p += s_nl + s_indent + "\tControls = new UIControl[]" + s_nl + s_indent + "\t{ " + s_nl + objectData.ControlStream;
                p += s_nl + s_indent + "\t}";
            }

            //report any shadow properties (workaround: C# does not allow double-init on construction)
            if (objectData.ShadowStream.Length > 0)
            {
                Sections.ShadowDefinitions += s_nl + objectData.ShadowStream;
            }

            return p;
        }

        public void ProcessObjectData(ObjectData objectData)
        {
            //Synonyms need special treatment: convert from WDL object with properties to C# object reference
            if (m_type.Equals("Synonym"))
                ProcessSynonym(objectData);
            else
                ProcessProperties(objectData);

        }

        private void ProcessSynonym(ObjectData objectData)
        {
            //Current implementation is not compatible with preprocessor directives - most likely not relevant for any A3 game ever created
            Property property;
            //workaround: build synonym definition in property
            //Type declares datatype of Synonym
            property = objectData.Properties.Where(x => x.Name.Equals("Type")).FirstOrDefault();
            if (property != null)
            {
                string synType = Formatter.FormatObject(property.Values[0]);
                //use C# strings -> convert to "string"
                if (synType.Equals("String"))
                    synType = "string";
                //"Action" keyword is reserved in C# -> use "Function" instead (mandatory)
                if (synType.Equals("Action"))
                    synType = "Function";
                //Scripts don't distinguish between object types and just use BaseObject which just carries all properties - like WDL
                //Trying to keep this more strict results in all kind of type problems in WDL actions
                if (synType.Equals("Wall") || synType.Equals("Thing") || synType.Equals("Actor"))
                    synType = "BaseObject";

                objectData.PropertyStream.Append(synType + " " + m_name);

                //Default declares default assignment of Synonym (optional)
                property = objectData.Properties.Where(x => x.Name.Equals("Default")).FirstOrDefault();
                if (property != null)
                {
                    if (!property.Values[0].Equals("null"))
                    {
                        objectData.PropertyStream.Append(" = " + Formatter.FormatIdentifier(property.Values[0]));
                    }
                }
            }
        }

        private void ProcessProperties(ObjectData objectData)
        {
            List<string> ranges = new List<string>();
            List<string> controls = new List<string>();
            List<string> properties = new List<string>();
            List<string> shadows = new List<string>();

            foreach (Property property in objectData.Properties)
            {
                bool isShadow = false;
                if (objectData.ParentContains(property.Name))
                {
                    Console.WriteLine("(I) OBJECT found shadow property: " + property.Name);
                    isShadow = true;
                }
                switch (property.Name)
                {
                    case "Flags":
                        if (!isShadow)
                            goto default;
                        else
                            shadows.Add($"{m_name}.{property.Format(m_type).Replace("=", "|=")}"); //apply patch for shadow definition
                        break;

                    case "Range":
                        if (!isShadow)
                            ranges.Add(property.Format(m_type));
                        else
                            //shadows.Add($"{m_name}.{property.Name}.Concat({property.Format(m_type)})"); -- not supported by C# for 2D arrays
                            Console.WriteLine("(W) OBJECT unable to build shadow property: " + property.Name);
                        break;

                    case "Digits":
                    case "Hbar":
                    case "Vbar":
                    case "Hslider":
                    case "Vslider":
                    case "Picture":
                    case "Window":
                    case "Button":
                        if (!isShadow)
                            controls.Add(property.Format(m_type));
                        else
                            shadows.Add($"{m_name}.Controls.Concat(new UIControl[] {{ {property.Format(m_type)} }})"); //apply patch for shadow definition
                        break;

                    default:
                        properties.Add(property.Format(m_type));
                        break;
                }
            }

            //initialized object definitions need different indention
            string indent = s_indent;
            if (IsInitialized())
                indent = s_indentInit;
            //create comma separated list of ranges
            ranges.Sort();
            objectData.RangeStream.Append(string.Join(s_nl, ranges.Select(x => indent + "\t\t" + x + ",")));

            //create comma separated list of controls
            controls.Sort();
            objectData.ControlStream.Append(string.Join(s_nl, controls.Select(x => indent + "\t\t" + x + ",")));

            //create comma separated list of properties
            properties.Sort();
            objectData.PropertyStream.Append(string.Join(s_nl, properties.Select(x => indent + "\t" + x + ",")));

            //create semicolon separated list of shadow properties (workaround: these need to be set in initialization routine of script)
            shadows.Sort();
            objectData.ShadowStream.Append(string.Join(s_nl, shadows.Select(x => s_indentInit + x + ";")));
        }

        private void AddProperty(Property property, List<Property> properties)
        {
            List<string> propertyNames = properties.Select(x => x.Name).ToList();
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
