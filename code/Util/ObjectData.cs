using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class ObjectData : PreProcessorData
    {
        private List<Property> m_properties;
        //private PropertyData m_propertyData;

        public ObjectData() : base(3)
        {
            m_properties = new List<Property>();
            //m_propertyData = new PropertyData();
        }

        public List<Property> Properties { get => m_properties; set => m_properties = value; }
        public StringBuilder RangeStream { get => m_streams[0]; }
        public StringBuilder ControlStream { get => m_streams[1]; }
        public StringBuilder PropertyStream { get => m_streams[2]; }
        //public PropertyData PropertyData { get => m_propertyData; set => m_propertyData = value; }

        /*public override void Add(PreProcessorData data)
        {
            if ((data != null) && data is ObjectData)
            {
                PropertyData propertyData = ((ObjectData)data).PropertyData;

                //append nested data if available
                if (!string.IsNullOrEmpty(m_propertyData.Ranges) && !string.IsNullOrEmpty(propertyData.Ranges))
                    m_propertyData.Ranges += s_nl + propertyData.Ranges;
                else
                    m_propertyData.Ranges += propertyData.Ranges;

                if (!string.IsNullOrEmpty(m_propertyData.Controls) && !string.IsNullOrEmpty(propertyData.Controls))
                    m_propertyData.Controls += s_nl + propertyData.Controls;
                else
                    m_propertyData.Controls += propertyData.Controls;

                if (!string.IsNullOrEmpty(m_propertyData.Properties) && !string.IsNullOrEmpty(propertyData.Properties))
                    m_propertyData.Properties += s_nl + propertyData.Properties;
                else
                    m_propertyData.Properties += propertyData.Properties;
            }
        }

        public override void Merge(string condition, PreProcessorData ifData, PreProcessorData elseData)
        {
            //Append data from if-branch
            Add(ifData);

            if (!string.IsNullOrEmpty(condition))
            {
                //TODO: support empty #if in case #else is not empty
                //insert preprocessor activation condition if required
                if (!string.IsNullOrEmpty(m_propertyData.Ranges))
                    m_propertyData.Ranges = "#if " + condition + s_nl + m_propertyData.Ranges;
                if (!string.IsNullOrEmpty(m_propertyData.Controls))
                    m_propertyData.Controls = "#if " + condition + s_nl + m_propertyData.Controls;
                if (!string.IsNullOrEmpty(m_propertyData.Properties))
                    m_propertyData.Properties = "#if " + condition + s_nl + m_propertyData.Properties;

                //take care of else branch if available
                if ((elseData != null) && elseData is ObjectData)
                {
                    ObjectData data = (ObjectData)elseData;
                    //insert preprocessor activation condition if required
                    if (!string.IsNullOrEmpty(data.PropertyData.Ranges))
                        m_propertyData.Ranges += s_nl + s_else;
                    if (!string.IsNullOrEmpty(data.PropertyData.Controls))
                        m_propertyData.Controls += s_nl + s_else;
                    if (!string.IsNullOrEmpty(data.PropertyData.Properties))
                        m_propertyData.Properties += s_nl + s_else;
                }
            }

            //Append data from else-branch
            Add(elseData);

            if (!string.IsNullOrEmpty(condition))
            {
                //append preprocessor end definition if present and required
                if (!string.IsNullOrEmpty(m_propertyData.Ranges))
                    m_propertyData.Ranges += s_nl + s_end;
                if (!string.IsNullOrEmpty(m_propertyData.Controls))
                    m_propertyData.Controls += s_nl + s_end;
                if (!string.IsNullOrEmpty(m_propertyData.Properties))
                    m_propertyData.Properties += s_nl + s_end;
            }
        }*/

    }
}
