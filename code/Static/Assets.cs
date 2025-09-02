using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Assets
    {
        private static List<Node> s_parameters = new List<Node>();
        public static Node AddAsset(Node type, Node name, Node file)
        {
            string stype = type.Data;
            string sname = name.Data;
            //Console.WriteLine("(I) ASSETS registered: " + sname + " of type " + stype);
            Registry.Register(stype, sname);

            type.NodeType = NodeType.Reserved;
            name.NodeType = NodeType.Identifier;
            file.NodeType = NodeType.File;
            Node a = new Asset(type, name, file, s_parameters);

            //Clean up
            s_parameters.Clear();

            return a;
        }

        public static Node AddParameter(Node value)
        {
            value.NodeType = NodeType.Number;
            s_parameters.Insert(0, value);
            return null;
        }

    }
}
