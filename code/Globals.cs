using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Globals
    {
        private static readonly string s_indent = "\t\t";
        static Dictionary<string, string> s_globals = new Dictionary<string, string>();

        private static readonly string s_nl = Environment.NewLine;

        public static string BuildGlobals()
        {
            string o = string.Empty;

            //generate declarations
            foreach (KeyValuePair<string, string> global in s_globals)
            {
                o += s_indent + global.Key + " = " + global.Value + ";" + s_nl;
            }
            o += s_nl;

            return o;
        }

        public static void AddGlobal(string name, string value)
        {
            //Patch for video mode definition
            if (name.Contains("Video"))
                value = "\"" + value + "\"";

            //ignore Bind and Path statements
            switch (name)
            {
                case "Bind":
                    Console.WriteLine("(I) GLOBALS ignore BIND definition: " + value);
                    break;

                case "Path":
                    Console.WriteLine("(I) GLOBALS ignore PATH definition: " + value);
                    break;

                default:
                    if (s_globals.ContainsKey(name))
                        Console.WriteLine("(W) GLOBALS ignore double definition: " + name);
                    else
                        s_globals.Add(name, value);
                    break;
            }
        }
    }
}
