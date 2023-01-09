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
        private static string s_ranges = string.Empty;
        private static string s_type = string.Empty;
        private static List<string> s_controls = new List<string>();
        private static List<string> s_properties = new List<string>();
        private static List<List<string>> s_propertyValues = new List<List<string>>();
        private static List<string> s_values = new List<string>();
        private static List<string> s_formattedProperties = new List<string>();

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

        public static string BuildObjects()
        {
            string o = string.Empty;
            string scope = "public ";

            //generate declarations
            foreach (KeyValuePair<string, Dictionary<string, string>> objs in s_objects)
            {
                string type = objs.Key;
                switch (type)
                {
                    case "Skill":
                        foreach (KeyValuePair<string, string> obj in objs.Value)
                        {
                            //Global skills don't need an extra declaration
                            if (string.Compare("Globals.", 0, obj.Key, 0, 8, true) != 0)
                            {
                                o += s_indent + scope + type + " " + obj.Key + ";" + s_nl;
                            }
                        }
                        break;

                    case "String":
                        foreach (KeyValuePair<string, string> obj in objs.Value)
                        {
                            o += s_indent + scope + "string " + obj.Key;
                            if (!string.IsNullOrEmpty(obj.Value))
                                o += " = " + obj.Value;
                            o += ";" + s_nl;
                        }
                        break;

                    case "Synonym":
                        //Synonyms carry full definition in obj.Value - only add indent and scope
                        foreach (KeyValuePair<string, string> obj in objs.Value)
                        {
                            o += s_indent + scope + obj.Value + ";" + s_nl;
                        }
                        break;

                    default:
                        foreach (KeyValuePair<string, string> obj in objs.Value)
                        {
                            o += s_indent + scope + type + " " + obj.Key + ";" + s_nl;
                        }
                        break;
                }
            }
            o += s_nl;

            //generate definitions
            foreach (KeyValuePair<string, Dictionary<string, string>> objs in s_objects)
            {
                string type = objs.Key;
                switch (type)
                {
                    //Snyonyms and strings don't require extra definition, they are handled at definition
                    case "Synonym":
                    case "String":
                        break;

                    default:
                        foreach (KeyValuePair<string, string> obj in objs.Value)
                        {
                            //Ways never have properties, but need to be instantiated nonetheless
                            //Strings don't need definitions, skip them
                            if (!string.IsNullOrEmpty(obj.Value) || string.Compare(type, "Way", true) == 0)
                            {
                                o += s_indent + obj.Key + " = new " + type + "()" + s_nl;
                                o += s_indent + "{" + s_nl;
                                o += obj.Value;
                                o += s_indent + "};" + s_nl;
                            }
                        }
                        break;
                }
            }
            o += s_nl;

            return o;
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
            //o += s_nl;

            return o;
        }

        public static string AddObject(string type, string name, string text)
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
            string o = string.Empty;

            //Synonyms are just plain variables in C# - special case
            if (string.Compare(type, "Synonym", true) == 0)
            {
                string prop;
                string synType = string.Empty;
                int i;

                //workaround build synonym definition in property
                //Type declares datatype of Synonym
                prop = "Type";
                if (s_properties.Contains(prop))
                {
                    i = s_properties.IndexOf(prop);
                    synType = Formatter.FormatObject(s_propertyValues[i][0]);
                    //"Action" keyword is reserved in C# -> use "Function" instead (mandatory)
                    if (string.Compare(synType, "Action", true) == 0)
                        synType = "Function";

                    o += synType + " " + name;

                    //Default declares default assignment of Synonym (optional)
                    prop = "Default";
                    if (s_properties.Contains(prop))
                    {
                        i = s_properties.IndexOf(prop);
                        o += " = " + Formatter.FormatObject(s_propertyValues[i][0]);
                    }
                }
                //Console.WriteLine("Synonym: " + o);
            }
            else
            {

                //handle property definitions
                for (int i = 0; i < s_properties.Count; i++)
                {
                    BuildProperty(type, s_properties[i], s_propertyValues[i]);
                }
            
                //handle panel control definitions
                if (s_controls.Count > 0)
                {
                    BuildControls();
                }

                //handle range definitions
                if (!string.IsNullOrEmpty(s_ranges))
                {
                    string p = "Range = new[,] {" + s_ranges + "}";
                    s_formattedProperties.Add(p);
                }

                s_formattedProperties.Sort();
                foreach (string s in s_formattedProperties)
                {
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
            s_formattedProperties.Clear();
            s_controls.Clear();
            s_properties.Clear();
            s_ranges = string.Empty;
            s_type = string.Empty;
            foreach (List<string> values in s_propertyValues)
            {
                values.Clear();
            }
            s_propertyValues.Clear();

            return BuildObject(type, name, o);
        }

        private static void BuildControls()
        {
            string c = "Controls = new UIControl[]" + s_nl + s_indent + "\t{ " + s_nl;
            for (int i = 0; i < s_controls.Count; i++)
            {
                c += s_indent + "\t\t" + s_controls[i];
                if (i < s_controls.Count - 1)
                    c += ",";
                c += s_nl;
            }
            c += s_indent + "\t}";
            s_formattedProperties.Add(c);
        }

        private static void BuildProperty(string obj, string property, List<string> values)
        {
            string p;
            p = property + " = ";

            //Todo add List length checks for explicite array access
            switch (property)
            {
                case "Type":
                    p += Formatter.FormatSkillType(values[0]);
                    break;

                case "Skill1":
                case "Skill2":
                case "Skill3":
                case "Skill4":
                case "Skill5":
                case "Skill6":
                case "Skill7":
                case "Skill8":
                    p += " new Skill (" + values[0] + ")";
                    s_formattedProperties.Add(p);
                    break;

                //collect all panel control definitions for generation of multi-dimensional array
                case "Digits":
                case "Hbar":
                case "Vbar":
                    s_controls.Add("new " + property + "(" + values[0] + ", " + values[1] + ", " + values[2] + ", " + values[3] + ", " + values[4] + ", () => { return " + values[5] + "; } )");
                    break;

                case "Hslider":
                case "Vslider":
                    s_controls.Add("new " + property + "(" + values[0] + ", " + values[1] + ", " + values[2] + ", " + values[3] + ", () => { return " + values[4] + "; } )");
                    break;

                case "Picture":
                    s_controls.Add("new " + property + "(" + values[0] + ", " + values[1] + ", " + values[2] + ", () => { return " + values[3] + "; } )");
                    break;

                case "Window":
                    s_controls.Add("new " + property + "(" + values[0] + ", " + values[1] + ", " + values[2] + ", " + values[3] + ", " + values[4] + ", () => { return " + values[5] + "; }, () => { return " + values[6] + "; } )");
                    break;

                case "Button":
                    s_controls.Add("new " + property + "(" + values[0] + ", " + values[1] + ", " + values[2] + ", " + values[3] + ", " + values[4] + ", " + values[5] + ")");
                    break;

                case "Flags":
                    p += GetFlags(values);
                    s_formattedProperties.Add(p);
                    break;

                case "Range":
                    //collect all range definitions for generation of multi-dimensional array
                    if (!string.IsNullOrEmpty(s_ranges))
                        s_ranges += ", ";
                    s_ranges += "{" + GetValues(values, ", ") + "}";
                    break;

                case "Scale_xy":
                    p = "Scale_x = " + values[0];
                    s_formattedProperties.Add(p);
                    p = "Scale_y = " + values[1];
                    s_formattedProperties.Add(p);
                    break;

                case "Bmap":
                case "Bmaps":
                    p = "Bmaps = new [] {" + GetValues(values, ", ") + "}";
                    s_formattedProperties.Add(p);
                    break;

                case "Ovlys":
                    p = "Ovlys = new [] {" + GetValues(values, ", ") + "}";
                    s_formattedProperties.Add(p);
                    break;

                case "Offset_x":
                case "Offset_y":
                    //for Wall only these properties are one-dimensional
                    if (string.Compare(obj, "Wall", true) != 0)
                    {
                        p += "new [] {" + GetValues(values, ", ") + "}";
                        s_formattedProperties.Add(p);
                    }
                    else
                    {
                        goto default;
                    }
                    break;

                case "String":
                    //fix name ambiguity for Text objects
                    if (string.Compare(obj, "Text", true) == 0)
                    {
                        p = "String_array = new [] {" + GetValues(values, ", ") + "}";
                        s_formattedProperties.Add(p);
                    }
                    else
                    {
                        goto default;
                    }
                    break;

                //these properties always require array instantiation
                case "Mirror":
                case "Delay":
                case "Scycles":
                    p += "new [] {" + GetValues(values, ", ") + "}";
                    s_formattedProperties.Add(p);
                    break;
/*
                case "Each_tick":
                case "Each_cycle":
                case "If_near":
                case "If_far":
                case "If_hit":
                case "If_touch":
                case "If_release":
                case "If_klick":
                case "If_arrived":
                case "If_enter":
                case "If_leave":
                case "If_dive":
                case "If_arise":
                    p += values[0];
                    s_formattedProperties.Add(p);
                    break;
*/
                default:
                    if (values.Count > 1)
                        p += "new [] {" + GetValues(values, ", ") + "}";
                    else
                        p += GetValues(values, ", ");
                    s_formattedProperties.Add(p);
                    break;
            }

            s_values.Clear();
        }

        public static void AddProperty(string property)
        {
            string p = string.Empty;
            bool allowMerge = false;
            bool allowMultiple = false;

            //Apply patches for undocumented WDL syntax
            switch (property)
            {
                case "Bmap":
                    p = "Bmaps";
                    break;

                case "Flags":
                    allowMerge = true;
                    p = property;
                    break;

                case "Digits":
                case "Hbar":
                case "Vbar":
                case "Hslider":
                case "Vslider":
                case "Picture":
                case "Window":
                case "Button":
                case "Range":
                    allowMultiple = true;
                    p = property;
                    break;

                default:
                    p = property;
                    break;
            }

            if (s_properties.Contains(p))
            {
                //Eliminate double definitions of properties only where their values can be merged
                if (allowMerge)
                {
                    int i = s_properties.IndexOf(p);
                    s_propertyValues[i].AddRange(s_values);
                }
                else if (allowMultiple)
                {
                    s_properties.Add(p);
                    s_propertyValues.Add(s_values);
                }
                else
                {
                    Console.WriteLine("(W) OBJECTS ignore double definition of property: " + property);
                }
            }
            else
            {
                s_properties.Add(p);
                s_propertyValues.Add(s_values);
            }

            //prepare valueList for next property
            s_values = new List<string>();
        }

        public static void AddPropertyValue(string value)
        {
            s_values.Insert(0, value);
        }

        private static string GetValues(List<string> values, string seperator)
        {
            string v = string.Empty;
            for (int i = 0; i < values.Count; i++)
            {
                v += values[i];
                if (i < values.Count - 1)
                    v += seperator;
            }
            return v;
        }

        private static string GetFlags(List<string> values)
        {
            string v = string.Empty;
            for (int i = 0; i < values.Count; i++)
            {
                v += Formatter.FormatFlag(values[i]);
                if (i < values.Count - 1)
                    v += " | ";
            }
            return v;
        }
    }
}
