using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Objects
    {
        private static Dictionary<string, Dictionary<string, string>> s_objects = new Dictionary<string, Dictionary<string, string>>();

        private static readonly string s_indent = "\t\t";
        private static List<string> s_values = new List<string>();

        private static readonly string s_nl = Environment.NewLine;

        static Objects()
        {
            s_objects.Add("Synonym", new Dictionary<string, string>());
            s_objects.Add("String", new Dictionary<string, string>());
            s_objects.Add("Skill", new Dictionary<string, string>());
            s_objects.Add("Palette", new Dictionary<string, string>());
            s_objects.Add("Texture", new Dictionary<string, string>());
            s_objects.Add("Wall", new Dictionary<string, string>());
            s_objects.Add("Region", new Dictionary<string, string>());
            s_objects.Add("Thing", new Dictionary<string, string>());
            s_objects.Add("Actor", new Dictionary<string, string>());
            s_objects.Add("Way", new Dictionary<string, string>());
            s_objects.Add("Text", new Dictionary<string, string>());
            s_objects.Add("Overlay", new Dictionary<string, string>());
            s_objects.Add("Panel", new Dictionary<string, string>());
            s_objects.Add("View", new Dictionary<string, string>());
        }

        public static bool Is(string obj, string name)
        {
            if (s_objects.TryGetValue(obj, out Dictionary<string, string> skills))
            {

                return skills.ContainsKey(name);
            }
            return false;
        }

        public static bool Is(out string obj, string name)
        {
            foreach (KeyValuePair<string, Dictionary<string, string>> kvp in s_objects)
            {
                if (kvp.Value.ContainsKey(name))
                {
                    obj = kvp.Key;
                    return true;
                }
            }
            obj = string.Empty;
            return false;
        }

        private static string BuildObject(string type, string name, string props)
        {
            string o = string.Empty;
            string scope = "public static ";

            switch (type)
            {
                case "Synonym":
                    o += s_indent + scope + props + ";";
                    break;

                case "String":
                    o += s_indent + scope + "string " + name;
                    if (!string.IsNullOrEmpty(props))
                        o += " = " + props;
                    o += ";";
                    break;

                default:
                    //Ways never have properties, but need to be instantiated nonetheless
                    //if (!string.IsNullOrEmpty(props) || string.Compare(type, "Way", true) == 0 || string.Compare(type, "Skill", true) == 0)
                    {
                        if (name.StartsWith("Skills."))
                            o += s_indent + "/*" + name + " = new " + type + "()"; //TODO: this needs to be moved to Constructor later on - PATCHED
                        else
                            o += s_indent + scope + type + " " + name + " = new " + type + "()";

                        if (!string.IsNullOrEmpty(props))
                        {
                            o += s_nl + s_indent + "{" + s_nl;
                            o += props + s_nl;
                            o += s_indent + "}";
                        }
                        o += ";";
                        if (name.StartsWith("Skills.")) o += "*/"; //PATCHED
                    }
                    break;
            }

            return o;
        }

        public static string AddStringObject(string type, string name, string text)
        {
            //move object into object lists
            if (s_objects.TryGetValue(type, out Dictionary<string, string> obj))
            {
                if (obj.ContainsKey(name))
                    Console.WriteLine("(W) OBJECTS ignore double definition: " + name);
                else
                    obj.Add(name, text);//TODO: change to List with names only
            }
            return BuildObject(type, name, text);

        }

        public static string AddObject(string type, string name)
        {
            return BuildObject(type, name, string.Empty);
        }

        public static string AddObject(string type, string name, string stream)
        {
            string o = string.Empty;

            List<Property> properties = Property.DeserializeList(stream);
            PreProcessorStack<ObjectData> stack = new PreProcessorStack<ObjectData>();
            ObjectData objectData = stack.Content;

            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "#if":
                        //update preprocessor stack and obtain new dataset
                        objectData = stack.Update(property.Name, property.Values[0]);
                        break;

                    case "#else":
                        //move all previously collected properties into data list
                        objectData.PropertyData = ProcessObjectData(objectData, type, name);
                        //update preprocessor stack and move to else branch of active dataset
                        objectData = stack.Update(property.Name);
                        break;

                    case "#endif":
                        //move all previously collected properties into data list
                        objectData.PropertyData = ProcessObjectData(objectData, type, name);
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
                objectData.PropertyData = ProcessObjectData(objectData, type, name);
            }

            objectData = stack.Merge();

            //copy standard properties to output
            o += objectData.PropertyData.Properties;

            //handle palette range definitions
            if (!string.IsNullOrEmpty(objectData.PropertyData.Ranges))
            {
                o += s_nl + s_indent + "\tRange = new[,]" + s_nl + s_indent + "\t{" + s_nl + objectData.PropertyData.Ranges;
                o += s_nl + s_indent + "\t}";
            }

            //handle UI control definitions
            if (!string.IsNullOrEmpty(objectData.PropertyData.Controls))
            {
                o += s_nl + s_indent + "\tControls = new UIControl[]" + s_nl + s_indent + "\t{ " + s_nl + objectData.PropertyData.Controls;
                o += s_nl + s_indent + "\t}";
            }

            //move object into object lists
            if (s_objects.TryGetValue(type, out Dictionary<string, string> obj))
            {
                if (obj.ContainsKey(name))
                    Console.WriteLine("(W) OBJECTS ignore double definition of object: " + name);
                else
                    obj.Add(name, o); //TODO: change to List with names only
            }

            return BuildObject(type, name, o);
        }

        private static PropertyData ProcessObjectData(ObjectData active, string type, string name)
        {
            //Synonyms need special treatment: convert from WDL object with properties to C# object reference
            if (type.Equals("Synonym"))
                return ProcessSynonym(active, name);
            else
                return ProcessProperties(active, type);

        }

        private static PropertyData ProcessSynonym(ObjectData active, string name)
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

                data.Properties = synType + " " + name;

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

        private static PropertyData ProcessProperties(ObjectData active, string type)
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
                        ranges.Add(property.Format(type));
                        break;

                    case "Digits":
                    case "Hbar":
                    case "Vbar":
                    case "Hslider":
                    case "Vslider":
                    case "Picture":
                    case "Window":
                    case "Button":
                        controls.Add(property.Format(type));
                        break;

                    default:
                        properties.Add(property.Format(type));
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

        private static void AddProperty(Property property, List<Property> properties)
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


        public static string CreatePreProcIfNotCondition(string expr, string stream)
        {
            string s = string.Empty;

            s += new Property("#if", "!(" + expr + ")").Serialize();
            s += stream;

            return s;
        }

        public static string CreatePreProcIfCondition(string expr, string stream)
        {
            string s = string.Empty;

            s += new Property("#if", expr).Serialize();
            s += stream;

            return s;
        }

        public static string CreatePreProcElseCondition(string ifstream, string elsestream)
        {
            string s = string.Empty;

            s += ifstream;
            s += new Property("#else").Serialize();
            s += elsestream;
            s += new Property("#endif").Serialize();

            return s;
        }

        public static string CreatePreProcEndCondition(string stream)
        {
            string s = string.Empty;

            s += stream;
            s += new Property("#endif").Serialize();

            return s;
        }

        public static string CreateProperty(string property)
        {
            Property prop = new Property(property, s_values);

            //Clean up
            s_values.Clear();
            return prop.Serialize();
        }

        public static void AddPropertyValue(string value)
        {
            s_values.Insert(0, value);
        }


        class PropertyData
        {
            private string m_ranges;
            private string m_controls;
            private string m_properties;

            public PropertyData()
            {
                m_ranges = string.Empty;
                m_controls = string.Empty;
                m_properties = string.Empty;
            }

            public string Ranges { get => m_ranges; set => m_ranges = value; }
            public string Controls { get => m_controls; set => m_controls = value; }
            public string Properties { get => m_properties; set => m_properties = value; }
        }

        class ObjectData : PreProcessorData
        {
            private List<Property> m_properties;
            private PropertyData m_propertyData;

            public ObjectData() : base()
            {
                m_properties = new List<Property>();
                m_propertyData = new PropertyData();
            }

            public List<Property> Properties { get => m_properties; set => m_properties = value; }
            public PropertyData PropertyData { get => m_propertyData; set => m_propertyData = value; }

            public override void Add(PreProcessorData data)
            {
                if ((data != null) && data is ObjectData)
                {
                    PropertyData propertyData = ((ObjectData)data).PropertyData;

                    //append nested data if available
                    if (!string.IsNullOrEmpty(m_propertyData.Ranges) && !string.IsNullOrEmpty(propertyData.Ranges))
                        m_propertyData.Ranges += s_nl + propertyData.Ranges;
                    else
                        m_propertyData.Ranges += propertyData.Ranges;

                    if (!string.IsNullOrEmpty(m_propertyData.Controls) && !string.IsNullOrEmpty(propertyData.Controls))
                        m_propertyData.Controls += s_nl + propertyData.Controls;
                    else
                        m_propertyData.Controls += propertyData.Controls;

                    if (!string.IsNullOrEmpty(m_propertyData.Properties) && !string.IsNullOrEmpty(propertyData.Properties))
                        m_propertyData.Properties += s_nl + propertyData.Properties;
                    else
                        m_propertyData.Properties += propertyData.Properties;
                }
            }

            public override void Merge(string condition, PreProcessorData ifData, PreProcessorData elseData)
            {
                //Append data from if-branch
                Add(ifData);

                if (!string.IsNullOrEmpty(condition))
                {
                    //insert preprocessor activation condition if required
                    if (!string.IsNullOrEmpty(m_propertyData.Ranges))
                        m_propertyData.Ranges = "#if " + condition + s_nl + m_propertyData.Ranges;
                    if (!string.IsNullOrEmpty(m_propertyData.Controls))
                        m_propertyData.Controls = "#if " + condition + s_nl + m_propertyData.Controls;
                    if (!string.IsNullOrEmpty(m_propertyData.Properties))
                        m_propertyData.Properties = "#if " + condition + s_nl + m_propertyData.Properties;

                    //take care of else branch if available
                    if ((elseData != null) && elseData is ObjectData)
                    {
                        ObjectData data = (ObjectData)elseData;
                        //insert preprocessor activation condition if required
                        if (!string.IsNullOrEmpty(data.PropertyData.Ranges))
                            m_propertyData.Ranges += s_nl + s_else;
                        if (!string.IsNullOrEmpty(data.PropertyData.Controls))
                            m_propertyData.Controls += s_nl + s_else;
                        if (!string.IsNullOrEmpty(data.PropertyData.Properties))
                            m_propertyData.Properties += s_nl + s_else;
                    }
                }

                //Append data from else-branch
                Add(elseData);

                if (!string.IsNullOrEmpty(condition))
                {
                    //append preprocessor end definition if present and required
                    if (!string.IsNullOrEmpty(m_propertyData.Ranges))
                        m_propertyData.Ranges += s_nl + s_end;
                    if (!string.IsNullOrEmpty(m_propertyData.Controls))
                        m_propertyData.Controls += s_nl + s_end;
                    if (!string.IsNullOrEmpty(m_propertyData.Properties))
                        m_propertyData.Properties += s_nl + s_end;
                }
            }
        }
    }
}
