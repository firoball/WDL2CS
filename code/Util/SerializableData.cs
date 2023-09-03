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
            //TODO: add support for "Load" section here
            //TODO: should this code be somewhere in Sections class?
            foreach (ISerializable section in m_sections)
            {
                if (ParentContains(section.Name))
                {
                    Console.WriteLine("(I) SERIALIZABLEDATA found shadow: " + section.Name);
                    //Workaround: treat shadow (redefined) sections like initialized objects
                    //initSections.Add(section.Format()); -- TODO: add on-the-fly-switch for "init" mode
                }
                else if (section.IsInitialized())
                {
                    section.Format(InitSectionStream);
                    InitSectionStream.AppendLine();
                }
                else
                {
                    section.Format(SectionStream);
                    SectionStream.AppendLine();
                }
            }

        }
    }
}
