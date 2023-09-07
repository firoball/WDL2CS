using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Identifiers
    {
        private static Dictionary<string, List<string>> s_identifiers = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        static Identifiers()
        {
            s_identifiers.Add("Model", new List<string>());
            s_identifiers.Add("Sound", new List<string>());
            s_identifiers.Add("Music", new List<string>());
            s_identifiers.Add("Flic", new List<string>());
            s_identifiers.Add("Bmap", new List<string>());
            s_identifiers.Add("Ovly", new List<string>());
            s_identifiers.Add("Font", new List<string>());
            s_identifiers.Add("Synonym", new List<string>());
            s_identifiers.Add("String", new List<string>());
            s_identifiers.Add("Skill", new List<string>());
            s_identifiers.Add("Palette", new List<string>());
            s_identifiers.Add("Texture", new List<string>());
            s_identifiers.Add("Wall", new List<string>());
            s_identifiers.Add("Region", new List<string>());
            s_identifiers.Add("Thing", new List<string>());
            s_identifiers.Add("Actor", new List<string>());
            s_identifiers.Add("Way", new List<string>());
            s_identifiers.Add("Text", new List<string>());
            s_identifiers.Add("Overlay", new List<string>());
            s_identifiers.Add("Panel", new List<string>());
            s_identifiers.Add("View", new List<string>());
            s_identifiers.Add("Action", new List<string>());
        }

        public static bool Is(string obj, string name)
        {
            if (s_identifiers.TryGetValue(obj, out List<string> skills))
            {

                return skills.Contains(name, StringComparer.OrdinalIgnoreCase);
            }
            return false;
        }

        public static bool Identify(out string obj, string name)
        {
            foreach (KeyValuePair<string, List<string>> kvp in s_identifiers)
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

        public static void Register(string type, string name)
        {
            /* Identifiers are registered for later identification as required by Defines and some Instructions
             * The identifier registry does not consider preprocessor directives, this leads to following limits:
             * 
             * - Identifiers created with certain preprocessor directives only are always identified
             * 
             * - Ambiguous identifier name identification (same name could be used for different identifiers in 
             *   different preprocessor directives) will be resolved with first come, first serve.
             *   This can lead to wrong identification results, but this case should be very rare to non-existent
             *   out in the wild.
             */
            if (s_identifiers.TryGetValue(type, out List<string> id))
            {
                if (!id.Contains(name))
                    id.Add(name);
                //else
                //Console.WriteLine("(W) IDENTIFIERS ignore double definition: " + name);
            }
        }

    }
}
