using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Assets
    {
        private static string s_indent = "\t\t";
        private static Dictionary<string, Dictionary<string, string>> s_assets = new Dictionary<string, Dictionary<string, string>>();

        private static readonly string s_nl = Environment.NewLine;

        static Assets()
        {
            s_assets.Add("Bmap", new Dictionary<string, string>());
            s_assets.Add("Flic", new Dictionary<string, string>());
            s_assets.Add("Font", new Dictionary<string, string>());
            s_assets.Add("Model", new Dictionary<string, string>());
            s_assets.Add("Music", new Dictionary<string, string>());
            s_assets.Add("Ovly", new Dictionary<string, string>());
            s_assets.Add("Sound", new Dictionary<string, string>());
        }


        public static string BuildAssets()
        {
            string o = string.Empty;

            //generate declarations
            foreach (KeyValuePair<string, Dictionary<string, string>> assets in s_assets)
            {
                string type = assets.Key;
                foreach (KeyValuePair<string, string> asset in assets.Value)
                {
                    o += s_indent + "public " + type + " " + asset.Key + ";" + s_nl;
                }
            }
            o += s_nl;

            //generate definitions
            foreach (KeyValuePair<string, Dictionary<string, string>> assets in s_assets)
            {
                string type = assets.Key;
                foreach (KeyValuePair<string, string> asset in assets.Value)
                {
                    o += s_indent + asset.Key + " = new " + type + "(" + asset.Value + ");" + s_nl;
                }
            }
            o += s_nl;

            return o;
        }

        public static void AddAsset(string type, string name, string file, string parameters)
        {
            string p = file;
            if (!string.IsNullOrEmpty(parameters))
                p += ", " + parameters;

            //move asset into asset lists
            Dictionary<string, string> asset;
            if (s_assets.TryGetValue(type, out asset))
            {
                if (asset.ContainsKey(name))
                    Console.WriteLine("ASSETS ignore double definition: " + name);
                else
                    asset.Add(name, p);
            }
        }

    }
}
