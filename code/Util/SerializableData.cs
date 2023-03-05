using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class SerializableData : PreProcessorData
    {
        private static readonly string s_nl = Environment.NewLine;

        private List<ISerializable> m_sections;
        private SectionData m_sectionData;

        public SerializableData() : base()
        {
            m_sections = new List<ISerializable>();
            m_sectionData = new SectionData();
        }

        public List<ISerializable> Sections { get => m_sections; set => m_sections = value; }
        public SectionData SectionData { get => m_sectionData; set => m_sectionData = value; }

        public override void Add(PreProcessorData data)
        {
            if ((data != null) && data is SerializableData)
            {
                SectionData sectionData = ((SerializableData)data).SectionData;

                //append nested data if available
                if (!string.IsNullOrEmpty(m_sectionData.Sections) && !string.IsNullOrEmpty(sectionData.Sections))
                    m_sectionData.Sections += s_nl + sectionData.Sections;
                else
                    m_sectionData.Sections += sectionData.Sections;

                if (!string.IsNullOrEmpty(m_sectionData.InitSections) && !string.IsNullOrEmpty(sectionData.InitSections))
                    m_sectionData.InitSections += s_nl + sectionData.InitSections;
                else
                    m_sectionData.InitSections += sectionData.InitSections;
            }
        }

        public override void Merge(string condition, PreProcessorData ifData, PreProcessorData elseData)
        {
            //Append data from if-branch
            Add(ifData);

            if (!string.IsNullOrEmpty(condition))
            {
                //insert preprocessor activation condition if required
                if (!string.IsNullOrEmpty(m_sectionData.Sections))
                    m_sectionData.Sections = "#if " + condition + s_nl + m_sectionData.Sections;
                if (!string.IsNullOrEmpty(m_sectionData.InitSections))
                    m_sectionData.InitSections = "#if " + condition + s_nl + m_sectionData.InitSections;

                //take care of else branch if available
                if ((elseData != null) && elseData is SerializableData)
                {
                    SerializableData data = (SerializableData)elseData;
                    //insert preprocessor activation condition if required
                    if (!string.IsNullOrEmpty(data.SectionData.Sections))
                        m_sectionData.Sections += s_nl + s_else;
                    if (!string.IsNullOrEmpty(data.SectionData.InitSections))
                        m_sectionData.InitSections += s_nl + s_else;
                }
            }

            //Append data from else-branch
            Add(elseData);

            if (!string.IsNullOrEmpty(condition))
            {
                //append preprocessor end definition if present and required
                if (!string.IsNullOrEmpty(m_sectionData.Sections))
                    m_sectionData.Sections += s_nl + s_end;
                if (!string.IsNullOrEmpty(m_sectionData.InitSections))
                    m_sectionData.InitSections += s_nl + s_end;
            }
        }

    }
}
