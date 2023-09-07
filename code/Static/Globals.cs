using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Globals
    {
        static List<string> s_eventPars = new List<string>();

        public static string AddGlobal(string name)
        {
            string g = new Global(name, s_eventPars).Serialize();

            //Clean up
            s_eventPars.Clear();

            return g;
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
                    break;
            }
            return g;
        }

        public static void AddParameter(string parameter)
        {
            parameter = Formatter.FormatIdentifier(parameter); //Events always take action references as parameter TODO: should this be here or outside?

            s_eventPars.Insert(0, parameter);
        }
    }
}
