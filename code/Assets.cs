using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Assets
    {
        private static readonly string s_indent = "\t\t";
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
            string scope = "public ";

            //generate declarations
            foreach (KeyValuePair<string, Dictionary<string, string>> assets in s_assets)
            {
                string type = assets.Key;
                foreach (KeyValuePair<string, string> asset in assets.Value)
                {
                    o += s_indent + scope + type + " " + asset.Key + ";" + s_nl;
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

        private static string BuildAsset(string type, string name, string pars)
        {
            string o = string.Empty;
            string scope = "public static ";

            o += s_indent + scope + type + " " + name + " = new " + type + "(" + pars + ");";

            return o;
        }

        public static string AddAsset(string type, string name, string file, string parameters)
        {
            string p = file;
            if (!string.IsNullOrEmpty(parameters))
                p += ", " + parameters;

            //move asset into asset lists
            if (s_assets.TryGetValue(type, out Dictionary<string, string> asset))
            {
                if (asset.ContainsKey(name))
                    Console.WriteLine("(W) ASSETS ignore double definition: " + name);
                else
                    asset.Add(name, p);//TODO: change to List with names only
            }

            return BuildAsset(type, name, p);
        }

    }
}
