using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Define : ISerializable
    {
        private static readonly string s_sepDef = "#[D]#";
        private static readonly string s_indent = "\t\t";
        private static readonly string s_scope = "private";

        private readonly string m_type;
        private readonly string m_redefine;
        private readonly string m_original;

        public string Name => m_redefine; //serve ISerializable (required by section double definition check)
        public string Type => m_type;

        public Define(string type, string redefine, string original)
        {
            m_type = type;
            m_redefine = redefine;
            m_original = original;
        }

        public bool IsInitialized()
        {
            return false;
        }

        public string Serialize()
        {
            return m_type + s_sepDef + m_redefine + s_sepDef + m_original;
        }

        public static Define Deserialize(string stream)
        {
            string[] fragments = stream.Split(new[] { s_sepDef }, StringSplitOptions.None);
            string type = fragments[0];
            string redefine = fragments[1];
            string original = fragments[2];

            return new Define(type, redefine, original);
        }

        public string Format()
        {
            string o;
            o = $"{s_indent}{s_scope} static readonly {m_type} {m_redefine} = {m_original};";

            return o;
        }
    }
}
