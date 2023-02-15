using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Asset
    {
        private static readonly string s_sepAss = "#[A]#";
        private static readonly string s_sepPar = "#[P]#";
        private static readonly string s_indent = "\t\t";
        private readonly string m_type;
        private string m_name;
        private string m_file;
        private List<string> m_parameters;

        public string Type { get => m_type; }
        public string Name { get => m_name; set => m_name = value; }
        public string File { get => m_file; set => m_file = value; }
        public List<string> Parameters { get => m_parameters; }

        public Asset()
        {
            m_type = string.Empty;
            m_name = string.Empty;
            m_file = string.Empty;
            m_parameters = new List<string>();
        }

        public Asset(string type, string name, string file) : this(type, name, file, null) { }

        public Asset(string type, string name, string file, List<string> parameters) : this()
        {
            m_type = type;
            m_name = name;
            m_file = file;
            if (parameters != null)
                m_parameters.AddRange(parameters);
        }

        public string Serialize()
        {
            string s = m_type + s_sepAss + m_name + s_sepAss + m_file;
            s += s_sepAss + string.Join(s_sepPar, m_parameters);
            return s;
        }

        public static Asset Deserialize(string stream)
        {            
            string[] fragments = stream.Split(new[] { s_sepAss }, StringSplitOptions.None);
            string type = fragments[0];
            string name = fragments[1];
            string file = fragments[2];
            List<string> parameters = null;
            if (!string.IsNullOrEmpty(fragments[3]))
            {
                parameters = fragments[3].Split(new[] { s_sepPar }, StringSplitOptions.None).ToList();
            }
            return new Asset(type, name, file, parameters);
        }

        public string Format()
        {
            string o = string.Empty;
            string scope = "public static ";

            string pars = string.Empty;
            if (m_parameters != null && m_parameters.Count > 0)
               pars = ", " + string.Join(", ", m_parameters);
            o += s_indent + scope + m_type + " " + m_name + " = new " + m_type + "(" + m_file + pars + ");";

            return o;
        }

    }
}
