using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Assets
    {
        //TODO: add asset list (similar to Objects) for identification whether identifier is an asset
        private static List<string> s_parameters = new List<string>();

        public static string AddAsset(string type, string name, string file)
        {
            string a = new Asset(type, name, file, s_parameters).Serialize();

            //Clean up
            s_parameters.Clear();

            return a;
        }

        public static void AddParameter(string value)
        {
            s_parameters.Insert(0, Formatter.FormatNumber(value));
        }

    }
}
