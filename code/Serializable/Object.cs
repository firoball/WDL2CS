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
        private Object m_way; //"inlined" Way object

        public string Name { get => m_name; set => m_name = value; }
        public string Type { get => m_type; }

        public Object(string type, string name, bool isInitialized, bool isString)
        {
            m_type = type;
            m_name = name;
            m_isString = isString;
            m_serializedProperties = string.Empty;
            m_properties = new List<Property>();
            m_isInitialized = isInitialized;
        }

        public Object(string type, string name, bool isInitialized, string properties) : this(type, name, isInitialized, properties, false) { }

        public Object(string type, string name, bool isInitialized, string properties, bool isString) : this(type, name, isInitialized, isString)
        {
            //special case: string content is treated like serialied property
            m_serializedProperties = properties;
        }

        public Object(string type, string name, bool initialized, List<Property> properties) : this(type, name, initialized, false)
        {
            if (properties != null)
                m_properties.AddRange(properties);
        }

        public Object() : this(string.Empty, string.Empty, false, false) { }

        public bool IsInitialized()
        {
            return m_isInitialized;
        }

        public string Serialize()
        {
            string s = m_type + s_sepObj + m_name + s_sepObj + m_isInitialized + s_sepObj + m_isString.ToString();
            s += s_sepObj + m_serializedProperties;
            return s;
        }

        public static Object Deserialize(ref string stream)
        {
            List<Property> properties = null;

            string[] fragments = stream.Split(new[] { s_sepObj }, StringSplitOptions.None);
            string type = fragments[0];
            string name = fragments[1];
            bool initialized = Convert.ToBoolean(fragments[2]);
            if ((fragments.Length > 4) && !string.IsNullOrEmpty(fragments[4]))
            {
                //do not deserialize properties if object is of type String
                //in this case, the fragment contains just the string content for direct use
                if (Convert.ToBoolean(fragments[3]))
                    return new Object(type, name, false, fragments[4], true);
                else
                    properties = Property.DeserializeList(fragments[4]);
            }
            return new Object(type, name, initialized, properties);
        }

        public void Format(StringBuilder sb)
        {
            m_type = Formatter.FormatReserved(m_type);
            if (m_isInitialized)
                m_name = Formatter.FormatSkill(m_name);
            else
                m_name = Formatter.FormatObjectId(m_name);

            string properties;
            //string carries text instead of (serialized) properties
            if (m_isString)
                properties = m_serializedProperties;
            else
                properties = ProcessProperties();

            string scope = "public static ";

            switch (m_type)
            {
                case "Synonym":
                    sb.Append(s_indent + scope + properties + ";");
                    break;

                case "String":
                    sb.Append(s_indent + scope + "string " + m_name);
                    if (!string.IsNullOrEmpty(properties))
                        sb.Append(" = " + properties);
                    sb.Append(";");
                    break;

                default:
                    //if "inlined" Way definition was found earlier, prepend Way definition here 
                    if (m_way != null)
                    {
                        m_way.Format(sb);
                        sb.AppendLine();
                    }
                    //Ways never have properties, but need to be instantiated nonetheless
                    //if (!string.IsNullOrEmpty(props) || string.Compare(type, "Way", true) == 0 || string.Compare(type, "Skill", true) == 0)
                    {
                        string indent = s_indent;
                        if (!IsInitialized())
                        {
                            indent = s_indent;
                            sb.Append(indent + scope + m_type + " ");
                        }
                        else
                        {
                            indent = s_indentInit;
                            sb.Append(indent);
                        }

                        sb.Append(m_name + " = new " + m_type + "()");

                        if (!string.IsNullOrEmpty(properties))
                        {
                            sb.Append(s_nl + indent + "{");
                            sb.Append(properties + s_nl);
                            sb.Append(indent + "}");
                        }
                        sb.Append(";");
                    }
                    break;
            }
        }

        private string ProcessProperties()
        {
            //sort properties alphabetically
            m_properties = m_properties.OrderBy(x => x.Name).ToList(); //TODO: sort AFTER formatting (as previously)

            string p = string.Empty;
            PreProcessorStack<ObjectData> stack = new PreProcessorStack<ObjectData>();
            ObjectData objectData = stack.Content;
            objectData.ParentObject = this; //required for callback in objectData.Format()

            foreach (Property property in m_properties)
            {
                //add property to active dataset
                AddProperty(property, objectData.Properties);
            }

            objectData = stack.Merge();

            //copy standard properties to output
            p += objectData.PropertyStream;

            //handle palette range definitions
            if (objectData.RangeStream.Length > 0)
            {
                p += s_nl + s_indent + "\tRange = new[,]" + s_nl + s_indent + "\t{" + objectData.RangeStream;
                p += s_nl + s_indent + "\t}";
            }

            //handle UI control definitions
            if (objectData.ControlStream.Length > 0)
            {
                p += s_nl + s_indent + "\tControls = new UIControl[]" + s_nl + s_indent + "\t{ " + objectData.ControlStream;
                p += s_nl + s_indent + "\t}";
            }

            //report any shadow properties (workaround: C# does not allow double-init on construction)
            if (objectData.ShadowStream.Length > 0)
            {
                Sections.ShadowDefinitions += objectData.ShadowStream;
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
                string synType = Formatter.FormatReserved(property.Values[0]);
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
            /*List<string> ranges = new List<string>();
            List<string> controls = new List<string>();
            List<string> properties = new List<string>();
            List<string> shadows = new List<string>();*/

            //initialized object definitions need different indention
            string indent = s_indent;
            if (IsInitialized())
                indent = s_indentInit;

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
                        {
                            //                            shadows.Add($"{m_name}.{property.Format(m_type).Replace("=", "|=")}"); //apply patch for shadow definition
                            objectData.ShadowStream.Append(s_nl);
                            objectData.ShadowStream.Append(s_indentInit);
                            objectData.ShadowStream.Append($"{m_name}.{property.Format(m_type).Replace("=", "|=")}"); //apply patch for shadow definition
                            objectData.ShadowStream.Append(";");
                        }
                        break;

                    case "Range":
                        if (!isShadow)
                        {
                            //                            ranges.Add(property.Format(m_type));
                            objectData.RangeStream.Append(s_nl);
                            objectData.RangeStream.Append(indent + "\t\t");
                            objectData.RangeStream.Append(property.Format(m_type));
                            objectData.RangeStream.Append(",");
                        }
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
                        //does not work for definitions with "allowMultiple" flag.
                        //if (!isShadow)
                        //                            controls.Add(property.Format(m_type));
                        objectData.ControlStream.Append(s_nl);
                        objectData.ControlStream.Append(indent + "\t\t");
                        objectData.ControlStream.Append(property.Format(m_type));
                        objectData.ControlStream.Append(",");
                        //else
                        //    shadows.Add($"{m_name}.Controls.Concat(new UIControl[] {{ {property.Format(m_type)} }})"); //apply patch for shadow definition
                        break;

                    case "Way":
                        //Ways can be "inlined" in object definitions
                        //if way is not yet defined, create and register it outside of serialized parser stream
                        //during formatting, formatted way object will be printend along with this object
                        if (!Identifiers.Is("Way", property.Values[0]))
                        {
                            Console.WriteLine("(I) OBJECT add missing Way definition for: " + property.Values[0]);
                            string obj = Objects.AddObject("Way", property.Values[0]);
                            m_way = Deserialize(ref obj);
                        }
                        goto default;

                    default:
                        //                        properties.Add(property.Format(m_type));
                        objectData.PropertyStream.Append(s_nl);
                        objectData.PropertyStream.Append(indent + "\t");
                        objectData.PropertyStream.Append(property.Format(m_type));
                        objectData.PropertyStream.Append(",");
                        break;
                }
            }
/*
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
            */
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
                    Console.WriteLine("(W) OBJECT ignore double definition of property: " + property.Name);
                }
            }
            else
            {
                properties.Add(property);
            }
        }
    }
}
