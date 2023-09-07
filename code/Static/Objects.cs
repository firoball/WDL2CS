using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Objects
    {
        private static List<string> s_values = new List<string>();

        public static string AddStringObject(string type, string name, string text)
        {
            Identifiers.Register(type, name);
            string o = new Object(type, name, false, text, true).Serialize();

            return o;
        }

        public static string AddObject(string type, string name)
        {
            return AddObject(type, name, string.Empty);
        }

        public static string AddObject(string type, string name, string stream)
        {
            bool initialize = false;
            //Exclude predefined skills
            if (!(type.Equals("skill", StringComparison.OrdinalIgnoreCase) && Identifier.IsSkill(ref name)))
                Identifiers.Register(type, name);
            else
                initialize = true; //make sure predefined skills are moved to init section 

            string o = new Object(type, name, initialize, stream).Serialize();

            return o;
        }

        public static string CreateProperty(string property)
        {
            if (Identifier.IsProperty(ref property) || Identifier.IsFlag(ref property))
            {
                Property prop = new Property(property, s_values);

                //Clean up
                s_values.Clear();
                return prop.Serialize();
            }
            else
            {
                //Clean up
                s_values.Clear();
                Console.WriteLine("(W) OBJECTS discarded invalid property: " + property);
                return string.Empty;
            }
        }

        public static void AddPropertyValue(string value)
        {
            s_values.Insert(0, value);
        }

    }
}
