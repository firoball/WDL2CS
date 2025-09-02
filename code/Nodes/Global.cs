using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace WDL2CS
{
    class Global : Node, ISection
    {
        private static GlobalTransformer s_transformer = null;
        private Node m_name;
        private List<Node> m_parameters;

        public static new GlobalTransformer Transformer { get => s_transformer; set => s_transformer = value; }
        public string Name { get => m_name.Data; }// set => m_name = value; }
        public string Type { get => "GLOBAL"; }

        //public List<Node> Parameters { get => m_parameters; set => m_parameters = value; }

        public Global()
        {
            m_name = new Node();
            m_parameters = new List<Node>();
        }

        public Global(Node name, Node parameter) : this(name, new Node[] { parameter }.ToList()) { }
        public Global(Node name, List<Node> parameters) : this()
        {
            m_name = name;
            if (parameters != null)
                m_parameters.AddRange(parameters);
            Setup();
        }

        public bool IsInitialized()
        {
            return true;
        }

        public void Transform(object obj)
        {
            s_transformer?.Transform(obj, m_name, m_parameters);
        }


        private static string[] s_multi = new[] { "each_tick", "each_sec", "panels", "layers", "messages" };

        private void Setup()
        {
            if (m_name.Data.ToLower().Equals("video")) //Patch for video mode definition
                m_parameters[0].NodeType = NodeType.SimpleString;

            //identify data type for array definition
            bool forceMulti = s_multi.Contains(m_name.Data.ToLower());

            if (m_parameters.Count > 1 || forceMulti)
            {
                //make sure parameter list is extended to 16
                int count = m_parameters.Count;
                for (int i = count; i < 16; i++)
                {
                    m_parameters.Add(new Node("null", NodeType.Null));
                }

            }

        }
    }
}
