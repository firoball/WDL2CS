using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Objects
    {
        private static List<Node> s_values = new List<Node>();
        private static List<Property> s_properties = new List<Property>();

        public static Node AddStringObject(Node type, Node name, Node text)
        {
            string stype = type.Data;
            string sname = name.Data;
            Registry.Register(stype, sname);
            Node o = new Object(type, name, false, text);

            return o;
        }

        public static Node AddObject(Node type, Node name)
        {
            string stype = type.Data;
            string sname = name.Data;
            bool initialize = false;
            //Exclude predefined skills
            if (!(stype.Equals("skill", StringComparison.OrdinalIgnoreCase) && Identifier.IsSkill(ref sname)))
                Registry.Register(stype, sname);
            else
                initialize = true; //make sure predefined skills are moved to init section 

            Node o = new Object(type, name, initialize, s_properties);
            //Clean up
            s_properties.Clear();

            return o;
        }

        public static Node CreateProperty(Node property)
        {
            string sproperty = property.Data;
            property.NodeType = NodeType.Reserved;

            if (Identifier.IsProperty(ref sproperty) || Identifier.IsFlag(ref sproperty))
            {
                Property prop = new Property(property, s_values);
                s_properties.Add(prop);
            }
            else
            {
                Console.WriteLine("(W) OBJECTS discarded invalid property: " + sproperty);
            }
            //Clean up
            s_values.Clear();

            return null;
        }

        public static Node AddPropertyValue(Node value)
        {
            s_values.Insert(0, value);
            return null;
        }

    }
}
