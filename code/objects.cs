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
        private static List<Property> s_properties = new List<Property>();
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
                            o += props;
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

            //Deserialize and merge properties 
            //TODO: evaluate #ifdefs during merge (create stack with Lists)
            List<Property> properties = Property.DeserializeList(stream);
            /*PreProcessorStack<Data> stack = new PreProcessorStack<Data>();
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "#if":
                        break;

                    case "#else":
                        break;

                    case "#end":
                        break;

                    default:
                        AddProperty(property, stack.Content.Properties);
                        break;
                }
            }*/
            properties.ForEach(x => AddProperty(x, s_properties));


            //Synonyms are just plain variables in C# - special case
            if (string.Equals(type, "Synonym"))
            {
                Property property;

                //workaround build synonym definition in property
                //Type declares datatype of Synonym
                property = s_properties.Where(x => x.Name.Equals("Type")).FirstOrDefault();
                if (property != null)
                {
                    string synType = Formatter.FormatObject(property.Values[0]);
                    //"Action" keyword is reserved in C# -> use "Function" instead (mandatory)
                    if (string.Equals(synType, "Action"))
                        synType = "Function";
                    //Scripts don't distinguish between object types and just use BaseObject which just carries all properties - like WDL
                    //Trying to keep this more strict results in all kind of type problems in WDL actions
                    if (string.Equals(synType, "Wall") || string.Equals(synType, "Thing") || string.Equals(synType, "Actor"))
                        synType = "BaseObject";

                    o += synType + " " + name;

                    //Default declares default assignment of Synonym (optional)
                    property = s_properties.Where(x => x.Name.Equals("Default")).FirstOrDefault();
                    if (property != null)
                    {
                        if (!property.Values[0].Equals("null"))
                        {
                            o += " = " + Formatter.FormatIdentifier(property.Values[0]);
                        }
                    }
                }
            }
            else
            {
                List<string> ranges = new List<string>();
                List<string> controls = new List<string>();
                List<string> formattedProperties = new List<string>();

                foreach (Property property in s_properties)
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
                            if (property.Name[0] == '#')
                            {
                                //TODO: switch preprocessor stack
                                if (property.Values.Count > 0)
                                    formattedProperties.Add(property.Name + " " + property.Values[0]);
                                else
                                    formattedProperties.Add(property.Name);
                            }
                            else
                            {
                                formattedProperties.Add(property.Format(type));
                            }
                            break;
                    }
                }

                //handle palette range definitions
                if (ranges.Count > 0)
                {
                    string p = "Range = new[,] {" + string.Join(", ", ranges) + "}";
                    formattedProperties.Add(p);
                }

                //handle panel control definitions
                if (controls.Count > 0)
                {
                    formattedProperties.Add(BuildControls(controls));
                }

                formattedProperties.Sort();
                foreach (string s in formattedProperties)
                {
                    if (s[0] == '#')
                        o += s + s_nl;
                    else
                        o += s_indent + "\t" + s + ", " + s_nl;
                }
            }

            //move object into object lists
            if (s_objects.TryGetValue(type, out Dictionary<string, string> obj))
            {
                if (obj.ContainsKey(name))
                    Console.WriteLine("(W) OBJECTS ignore double definition of object: " + name);
                else
                    obj.Add(name, o); //TODO: change to List with names only
            }

            //Clean up
            s_properties.Clear();
            return BuildObject(type, name, o);
        }

        private static string BuildControls(List<string> controls)
        {
            string c = "Controls = new UIControl[]" + s_nl + s_indent + "\t{ " + s_nl;
            c += string.Join("," + s_nl, controls.Select(x => s_indent + "\t\t" + x));
            c += s_nl + s_indent + "\t}";
            return c;
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

        public static void AddPropertyValue(string value)
        {
            s_values.Insert(0, value);
        }

        class Data
        {
            private List<Property> m_properties;
            private List<string> m_ranges;
            private List<string> m_controls;

            public List<string> Ranges { get => m_ranges; set => m_ranges = value; }
            public List<string> Controls { get => m_controls; set => m_controls = value; }
            public List<Property> Properties { get => m_properties; set => m_properties = value; }
        }
    }
}
