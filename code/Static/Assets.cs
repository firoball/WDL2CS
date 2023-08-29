using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Assets
    {
        private static List<string> s_parameters = new List<string>();
        private static Dictionary<string, List<string>> s_assets = new Dictionary<string, List<string>>();

        static Assets()
        {
            s_assets.Add("Model", new List<string>());
            s_assets.Add("Sound", new List<string>());
            s_assets.Add("Music", new List<string>());
            s_assets.Add("Flic", new List<string>());
            s_assets.Add("Bmap", new List<string>());
            s_assets.Add("Ovly", new List<string>());
            s_assets.Add("Font", new List<string>());
        }


        public static bool Identify(out string asset, string name)
        {
            foreach (KeyValuePair<string, List<string>> kvp in s_assets)
            {
                if (kvp.Value.Contains(name))
                {
                    asset = kvp.Key;
                    return true;
                }
            }
            asset = string.Empty;
            return false;
        }

        private static void Register(string type, string name)
        {
            /* Assets are registered for later identification as required by Defines
             * The asset registry does not consider preprocessor directives, this leads to following limits:
             * 
             * - Assets created with certain preprocessor directives only are always identified
             * 
             * - Ambiguous object name identification (same name could be used for different objects in 
             *   different preprocessor directives) will be resolved with first come, first serve.
             *   This can lead to wrong identification results, but this case should be very rare to non-existent
             *   out in the wild.
             */
            if (s_assets.TryGetValue(type, out List<string> obj))
            {
                if (!obj.Contains(name))
                    obj.Add(name);
                //else
                //Console.WriteLine("(W) ASSETS ignore double definition: " + name);
            }
        }

        public static string AddAsset(string type, string name, string file)
        {
            type = Formatter.FormatObject(type);
            name = Formatter.FormatAssetId(name);
            Register(type, name);
            string a = new Asset(type, name, file, s_parameters).Serialize();

            //Clean up
            s_parameters.Clear();

            return a;
        }

        public static void AddParameter(string value)
        {
            s_parameters.Insert(0, value);
        }

    }
}
