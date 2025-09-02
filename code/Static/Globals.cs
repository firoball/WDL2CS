using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Globals
    {
        static List<Node> s_eventPars = new List<Node>();

        public static Node AddGlobal(Node name)
        {
            Node g = new Global(name, s_eventPars);

            //Clean up
            s_eventPars.Clear();

            return g;
        }

        public static Node AddGlobal(Node name, Node parameter)
        {
            string sname = name.ToString();
            string sparameter = parameter.ToString();
            Node g;
            //ignore Bind and Path statements
            switch (name.Data.ToLower())
            {
                case "bind":
                    g = new Node();
                    Console.WriteLine("(I) GLOBALS ignore BIND definition: " + parameter.Data);
                    break;

                case "path":
                    g = new Node();
                    Console.WriteLine("(I) GLOBALS ignore PATH definition: " + parameter.Data);
                    break;

                default:
                    g = new Global(name, parameter);
                    break;
            }
            return g;
        }

        public static Node AddParameter(Node parameter)
        {
            //due to grammar shift/reduce issues single parameters of Events are treated as single-item list
            //NodeType for Event parameters is always Identifier, therefore set it as default here
            if (parameter.NodeType != NodeType.Null)
                parameter.NodeType = NodeType.Identifier;
            s_eventPars.Insert(0, parameter);
            return null;
        }
    }
}
