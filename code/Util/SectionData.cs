using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class SectionData
    {
        private string m_sections;
        private string m_initSections;

        public SectionData()
        {
            m_sections = string.Empty;
            m_initSections = string.Empty;
        }

        public string Sections { get => m_sections; set => m_sections = value; }
        public string InitSections { get => m_initSections; set => m_initSections = value; }
    }
}
