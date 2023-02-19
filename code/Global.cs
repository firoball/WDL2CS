﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Global
    {
        private static readonly string s_sepGlob = "#[G]#";
        private static readonly string s_sepPar = "#[P]#";
        private static readonly string s_indent = "\t\t";
        private string m_name;
        private List<string> m_parameters;

        public string Name { get => m_name; set => m_name = value; }
        public List<string> Parameters { get => m_parameters; set => m_parameters = value; }

        public Global()
        {
            m_name = string.Empty;
            m_parameters = new List<string>();
        }

        public Global(string name, string parameter) : this(name, new string[] { parameter }.ToList()) { }
        public Global(string name, List<string> parameters) : this()
        {
            m_name = name;
            if (parameters != null)
                m_parameters.AddRange(parameters);
        }

        public string Serialize()
        {
            string s = m_name + s_sepGlob;
            s += string.Join(s_sepPar, m_parameters);
            return s;
        }

        public static Global Deserialize(string stream)
        {
            string[] fragments = stream.Split(new[] { s_sepGlob }, StringSplitOptions.None);
            string name = fragments[0];
            List<string> parameterss = null;
            if (!string.IsNullOrEmpty(fragments[1]))
            {
                parameterss = fragments[1].Split(new[] { s_sepPar }, StringSplitOptions.None).ToList();
            }
            return new Global(name, parameterss);
        }

        public string Format()
        {
            string o = string.Empty;

            if (m_parameters.Count > 1)
            {
                //identify data type for array definition
                string type = string.Empty;
                if (m_name.Contains("Each_"))
                    type = "Function";
                if (m_name.Contains("Panels"))
                    type = "Panel";
                if (m_name.Contains("Layers"))
                    type = "Overlay";
                if (m_name.Contains("Messages"))
                    type = "Text";

                string parameters = string.Join(", ", m_parameters);
                o += s_indent + m_name + " = new " + type + "[] {" + parameters + "};";
            }
            else
            {
                string parameter = m_parameters[0];
                //Patch for video mode definition
                if (m_name.Contains("Video"))
                    parameter = "\"" + parameter + "\"";
                o += s_indent + m_name + " = " + parameter + ";";
            }
            return o;
        }

    }
}
