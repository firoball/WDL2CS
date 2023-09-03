using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Preprocessor : ISerializable
    {
        private static readonly string s_sepPre = "#[P]#";

        private readonly string m_name;
        private readonly string m_expression;

        public string Name { get => m_name; }
        public string Type { get => "Preprocessor"; }
        public string Expression { get => m_expression; }

        public Preprocessor()
        {
            m_name = string.Empty;
            m_expression = string.Empty;
        }

        public Preprocessor(string preprocessor) : this(preprocessor, string.Empty) { }
        public Preprocessor(string preprocessor, string expression) : this()
        {
            m_name = preprocessor;
            m_expression = expression;
        }

        public bool IsInitialized()
        {
            return false;
        }

        public string Serialize()
        {
            return m_name + s_sepPre + m_expression;
        }

        public static Preprocessor Deserialize(ref string stream)
        {
            string[] fragments = stream.Split(new[] { s_sepPre }, StringSplitOptions.None);
            Preprocessor pre;
            //if ((fragments.Length > 1) && !string.IsNullOrEmpty(fragments[1]))
            {
                pre = new Preprocessor(fragments[0], fragments[1]);
            }
            //else
            //{
            //    pre = new Preprocessor(fragments[0]);
            //}

            return pre;
        }

        public void Format(StringBuilder sb)
        {
            sb.Append(m_name);
            if (!string.IsNullOrEmpty(m_expression))
                sb.Append (" " + m_expression);
        }

    }
}
