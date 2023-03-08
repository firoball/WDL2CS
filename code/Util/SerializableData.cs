using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class SerializableData : PreProcessorData
    {
        private List<ISerializable> m_sections;

        public SerializableData() : base(2)
        {
            m_sections = new List<ISerializable>();
        }

        public List<ISerializable> Sections { get => m_sections; }
        public StringBuilder SectionStream { get => m_streams[0]; }
        public StringBuilder InitSectionStream { get => m_streams[1]; }

        public override bool Contains(string name)
        {
            return m_sections.Where(x => x.Name.Equals(name)).FirstOrDefault() != null;
        }

        public override void Format()
        {
            List<string> sections = new List<string>();
            List<string> initSections = new List<string>();

            foreach (ISerializable section in m_sections)
            {
                if (section.IsInitialized())
                    initSections.Add(section.Format());
                else
                    sections.Add(section.Format());
            }
            //sections.Sort(); //TODO: this is dangerous - introduce sort by type
            SectionStream.Append(string.Join(s_nl, sections));

            //initSections.Sort(); //TODO: this is dangerous - introduce sort by type
            InitSectionStream.Append(string.Join(s_nl, initSections));
        }
    }
}
