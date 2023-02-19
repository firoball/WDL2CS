using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Objects
    {
        private static Dictionary<string, List<string>> s_objects = new Dictionary<string, List<string>>();

        private static List<string> s_values = new List<string>();

        private static readonly string s_nl = Environment.NewLine;

        static Objects()
        {
            s_objects.Add("Synonym", new List<string>());
            s_objects.Add("String", new List<string>());
            s_objects.Add("Skill", new List<string>());
            s_objects.Add("Palette", new List<string>());
            s_objects.Add("Texture", new List<string>());
            s_objects.Add("Wall", new List<string>());
            s_objects.Add("Region", new List<string>());
            s_objects.Add("Thing", new List<string>());
            s_objects.Add("Actor", new List<string>());
            s_objects.Add("Way", new List<string>());
            s_objects.Add("Text", new List<string>());
            s_objects.Add("Overlay", new List<string>());
            s_objects.Add("Panel", new List<string>());
            s_objects.Add("View", new List<string>());
        }

        public static bool Is(string obj, string name)
        {
            if (s_objects.TryGetValue(obj, out List<string> skills))
            {

                return skills.Contains(name);
            }
            return false;
        }

        public static bool Identify(out string obj, string name)
        {
            foreach (KeyValuePair<string, List<string>> kvp in s_objects)
            {
                if (kvp.Value.Contains(name))
                {
                    obj = kvp.Key;
                    return true;
                }
            }
            obj = string.Empty;
            return false;
        }

        private static void Register(string type, string name)
        {
            /* Objects are registered for later identification as required by Defines and some Instructions
             * The object registry does not consider preprocessor directives, this leads to following limits:
             * 
             * - Objects created with certain preprocessor directives only are always identified
             * 
             * - Ambiguous object name identification (same name could be used for different objects in 
             *   different preprocessor directives) will be resolved with first come, first serve.
             *   This can lead to wrong identification results, but this case should be very rare to non-existent
             *   out in the wild.
             */
            if (s_objects.TryGetValue(type, out List<string> obj))
            {
                if (!obj.Contains(name))
                    obj.Add(name);
                //else
                    //Console.WriteLine("(W) OBJECTS ignore double definition: " + name);
            }
        }

        public static string AddStringObject(string type, string name, string text)
        {
            Register(type, name);
            string o = new Object(type, name, text, true).Serialize();

            //TODO: move up to Section level - until Section code is updated for serialization, just deserialize and format
            Object obj = Object.Deserialize(o);
            return obj.Format();
        }

        public static string AddObject(string type, string name)
        {
            return AddObject(type, name, string.Empty);
        }

        public static string AddObject(string type, string name, string stream)
        {
            Register(type, name);
            string o = new Object(type, name, stream).Serialize();

            //TODO: move up to Section level - until Section code is updated for serialization, just deserialize and format
            Object obj = Object.Deserialize(o);
            return obj.Format();
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

    }
}
