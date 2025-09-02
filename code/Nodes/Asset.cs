using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Asset : Node, ISection
    {
        private static AssetTransformer s_transformer = null;

        private readonly Node m_type;
        private Node m_name;
        private Node m_file;
        private List<Node> m_parameters;

        public static new AssetTransformer Transformer { get => s_transformer; set => s_transformer = value; }
        public string Type { get => m_type.Data; }
        public string Name { get => m_name.Data; }

        public Asset()
        {
            m_type = new Node();
            m_name = new Node();
            m_file = new Node();
            m_parameters = new List<Node>();
        }

        public Asset(Node type, Node name, Node file) : this(type, name, file, null) { }

        public Asset(Node type, Node name, Node file, List<Node> parameters) : this()
        {
            m_type = type;
            m_name = name;
            m_file = file;
            if (parameters != null)
                m_parameters.AddRange(parameters);
        }

        public bool IsInitialized()
        {
            return false;
        }

        public void Transform(object obj)
        {
            s_transformer?.Transform(obj, m_name, m_type, m_file, m_parameters);
        }

    }
}
