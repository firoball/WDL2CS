using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class PropertyData
    {
        private string m_ranges;
        private string m_controls;
        private string m_properties;

        public PropertyData()
        {
            m_ranges = string.Empty;
            m_controls = string.Empty;
            m_properties = string.Empty;
        }

        public string Ranges { get => m_ranges; set => m_ranges = value; }
        public string Controls { get => m_controls; set => m_controls = value; }
        public string Properties { get => m_properties; set => m_properties = value; }
    }
}
