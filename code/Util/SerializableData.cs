using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class SerializableData : PreProcessorData
    {
        private List<ISerializable> m_sections;
        //private SectionData m_sectionData;

        public SerializableData() : base(2)
        {
            m_sections = new List<ISerializable>();
            //m_sectionData = new SectionData();
        }

        public List<ISerializable> Sections { get => m_sections; set => m_sections = value; }
        //public SectionData SectionData { get => m_sectionData; set => m_sectionData = value; }
        public StringBuilder SectionStream { get => m_streams[0]; }
        public StringBuilder InitSectionStream { get => m_streams[1]; }

        /*public override void Add(PreProcessorData data)
        {
            if ((data != null) && data is SerializableData)
            {
                SectionData sectionData = ((SerializableData)data).SectionData;

                //append nested data if available
                AddStream(m_sectionData.Sections, sectionData.Sections);
                AddStream(m_sectionData.InitSections, sectionData.InitSections);
            }
        }

        public override void Merge(string condition, PreProcessorData ifData, PreProcessorData elseData)
        {
            if ((ifData != null) && ifData is SerializableData)
            {
                SectionData sectionIfData = ((SerializableData)ifData).SectionData;

                SectionData sectionElseData;
                if ((elseData != null) && elseData is SerializableData)
                {
                    sectionElseData = ((SerializableData)elseData).SectionData;
                }
                else
                {
                    //no #else branch available; make it empty
                    sectionElseData = new SectionData();
                }

                MergeStream(m_sectionData.Sections, condition, sectionIfData.Sections, sectionElseData.Sections);
                MergeStream(m_sectionData.InitSections, condition, sectionIfData.InitSections, sectionElseData.InitSections);
            }
        }*/
    }
}
