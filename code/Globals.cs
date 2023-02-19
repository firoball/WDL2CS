using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Globals
    {
        static List<string> s_eventPars = new List<string>();

        public static string AddEvent(string name)
        {
            string g = new Global(name, s_eventPars).Serialize();

            //Clean up
            s_eventPars.Clear();

            //TODO: move up to Section level - until Section code is updated for serialization, just deserialize and format
            return "//PATCHED: " + Global.Deserialize(g).Format(); //PATCHED
        }

        public static string AddGlobal(string name, string parameter)
        {
            string g = string.Empty;

            //ignore Bind and Path statements
            switch (name)
            {
                case "Globals.Bind":
                    Console.WriteLine("(I) GLOBALS ignore BIND definition: " + parameter);
                    break;

                case "Globals.Path":
                    Console.WriteLine("(I) GLOBALS ignore PATH definition: " + parameter);
                    break;

                default:
                    g = new Global(name, parameter).Serialize();
                    //TODO: move up to Section level - until Section code is updated for serialization, just deserialize and format
                    g = "//PATCHED: " + Global.Deserialize(g).Format(); //PATCHED
                    break;
            }
            return g;
        }

        public static void AddParameter(string parameter)
        {
            //parser does not explicitly take care of null parameters, therefore patch these manually
            if (parameter.Equals(Formatter.FormatIdentifier("null")))
                parameter = Formatter.FormatNull();

            s_eventPars.Insert(0, parameter);
        }
    }
}
