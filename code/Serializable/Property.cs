using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Property
    {
        private static readonly string s_sepProp = "#[P]#";
        private static readonly string s_sepCont = "#[C]#";
        private static readonly string s_sepVal = "#[V]#";

        private string m_name;
        private bool m_allowMerge; //do not serialize, can easily be recreated
        private bool m_allowMultiple; //do not serialize, can easily be recreated
        private List<string> m_values;

        public string Name { get => m_name; set { m_name = value; SetFlags(); } }
        public List<string> Values { get => m_values; set => m_values = value; }
        public bool AllowMerge { get => m_allowMerge; }
        public bool AllowMultiple { get => m_allowMultiple; }

        public Property()
        {
            m_allowMerge = false;
            m_allowMultiple = false;
            Name = string.Empty;
            m_values = new List<string>();
        }

        public Property(string property) : this(property, new List<string>()) { }
        public Property(string property, string value) : this(property, new[] { value }.ToList()) { }

        public Property(string property, List<string> values) : this()
        {
            m_name = property;
            if (values != null)
                m_values.AddRange(values);
        }

        public string Serialize()
        {
            string s = s_sepProp + m_name;
            s += s_sepCont + string.Join(s_sepVal, m_values);
            return s;
        }

        public static Property Deserialize(string stream)
        {
            //kill any leading object seperator - it is used for serializing multiple instructions only
            string[] fragments = stream.Split(new[] { s_sepProp }, StringSplitOptions.RemoveEmptyEntries);

            fragments = fragments[0].Split(new[] { s_sepCont }, StringSplitOptions.None);
            Property prop = new Property
            {
                Name = fragments[0]
            };
            if ((fragments.Length > 1) && !string.IsNullOrEmpty(fragments[1]))
            {
                prop.Values = fragments[1].Split(new[] { s_sepVal }, StringSplitOptions.None).ToList();
            }

            return prop;
        }

        public string Format(string obj)
        {
            string p = string.Empty;

            try
            {
                //TODO: add List length checks for explicite array access
                switch (m_name)
                {
                    case "Type":
                        p = m_name + " = " + Formatter.FormatSkillType(m_values[0]);
                        break;

                    case "Skill1":
                    case "Skill2":
                    case "Skill3":
                    case "Skill4":
                    case "Skill5":
                    case "Skill6":
                    case "Skill7":
                    case "Skill8":
                        p = m_name + " = new Skill(" + m_values[0] + ")";
                        break;

                    //collect all panel control definitions for generation of multi-dimensional array
                    case "Digits":
                    case "Hbar":
                    case "Vbar":
                        p = "new " + m_name + "(" + m_values[0] + ", " + m_values[1] + ", " + m_values[2] + ", " + m_values[3] + ", " + m_values[4] + ", () => { return " + m_values[5] + "; } )";
                        break;

                    case "Hslider":
                    case "Vslider":
                        p = "new " + m_name + "(" + m_values[0] + ", " + m_values[1] + ", " + m_values[2] + ", " + m_values[3] + ", () => { return " + m_values[4] + "; } )";
                        break;

                    case "Picture":
                        p = "new " + m_name + "(" + m_values[0] + ", " + m_values[1] + ", " + m_values[2] + ", () => { return " + m_values[3] + "; } )";
                        break;

                    case "Window":
                        p = "new " + m_name + "(" + m_values[0] + ", " + m_values[1] + ", " + m_values[2] + ", " + m_values[3] + ", " + m_values[4] + ", () => { return " + m_values[5] + "; }, () => { return " + m_values[6] + "; } )";
                        break;

                    case "Button":
                        p = "new " + m_name + "(" + m_values[0] + ", " + m_values[1] + ", " + m_values[2] + ", " + m_values[3] + ", " + m_values[4] + ", " + m_values[5] + ")";
                        break;

                    case "Flags":
                        p = m_name + " = " + string.Join(" | ", m_values.Select(x => Formatter.FormatFlag(x)));
                        break;

                    case "Range":
                        //collect all range definitions for generation of multi-dimensional array
                        p = "{" + string.Join(", ", m_values) + "}";
                        break;

                    case "Bmap":
                    case "Bmaps":
                        p = "Bmaps = new Bmap[] {" + string.Join(", ", m_values) + "}";
                        break;

                    case "Ovlys":
                        p = "Ovlys = new Ovly[] {" + string.Join(", ", m_values) + "}";
                        break;

                    case "Offset_x":
                    case "Offset_y":
                        //for Wall only these properties are one-dimensional
                        if (!string.Equals(obj, "Wall"))
                        {
                            p = m_name + " = " + "new Var[] {" + string.Join(", ", m_values) + "}";
                        }
                        else
                        {
                            goto default;
                        }
                        break;

                    case "String":
                        //fix name ambiguity for Text objects
                        if (string.Equals(obj, "Text"))
                        {
                            p = "String_array = new string[] {" + string.Join(", ", m_values) + "}";
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
                        p = m_name + " = " + "new Var[] {" + string.Join(", ", m_values) + "}";
                        break;

                    case "Scale_xy":
                        p = m_name + " = " + "new Var[] {" + string.Join(", ", m_values) + "}";
                        break;

                    case "Target":
                        {
                            p = m_name + " = " + Formatter.FormatActorTarget(m_values[0]);
                        }
                        break;

                    default:
                        if (m_values.Count > 1)
                            p = m_name + " = " + "new [] {" + string.Join(", ", m_values) + "}";
                        else
                            p = m_name + " = " + m_values[0];
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("(E) PROPERTY: " + e);
            }
            return p;
        }

        public static List<Property> DeserializeList(string stream)
        {
            string[] fragments = stream.Split(new[] { s_sepProp }, StringSplitOptions.RemoveEmptyEntries);
            List<Property> properties = new List<Property>();
            foreach (string fragment in fragments)
            {
                properties.Add(Deserialize(fragment));
            }
            return properties;
        }

        private void SetFlags()
        {
            switch (m_name)
            {
                //Apply patch for undocumented WDL syntax
                case "Bmap":
                    m_name = "Bmaps";
                    break;

                //All definitions must be merged into single one
                case "Flags":
                    m_allowMerge = true;
                    break;

                //Multiple definitions per object allowed
                case "Digits":
                case "Hbar":
                case "Vbar":
                case "Hslider":
                case "Vslider":
                case "Picture":
                case "Window":
                case "Button":
                case "Range":
                    m_allowMultiple = true;
                    break;

                default:
                    break;
            }

        }
    }
}
