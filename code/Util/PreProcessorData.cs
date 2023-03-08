using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    abstract class PreProcessorData
    {

        protected static readonly string s_if = "#if";
        protected static readonly string s_else = "#else";
        protected static readonly string s_end = "#endif";
        protected static readonly string s_nl = Environment.NewLine;

        protected StringBuilder[] m_streams;

        public PreProcessorData(int streams)
        {
            m_streams = new StringBuilder[streams];
            for (int i = 0; i < streams; i++)
            {
                m_streams[i] = new StringBuilder();
            }
        }

        public virtual void Format()
        {
            //replace witch formatting code for sata structure
        }

        //TODO: review stack.Contains function for identification of shadow definitions
        public virtual bool Contains(string name)
        {
            //replace witch code to check data structure for existing elements
            return false;
        }

        private void AddStream(StringBuilder ownData, StringBuilder otherData)
        {
            //append nested data if available
            if ((ownData.Length > 0) && (otherData.Length > 0))
                ownData.Append(s_nl);
            ownData.Append(otherData);
        }

        private void MergeStream(StringBuilder result, string condition, StringBuilder ifData, StringBuilder elseData)
        {
            //add #if - directive (allow empty #if in case #else is required)
            if (!string.IsNullOrEmpty(condition) && ((ifData.Length > 0) || (elseData.Length > 0)))
            {
                result.Append("#if " + condition);
            }
            AddStream(result, ifData);

            //add #else directive
            if (elseData.Length > 0)
            {
                result.Append(s_nl + s_else);
            }
            AddStream(result, elseData);

            //add #end directive only in case #if and/or #else is required
            if (!string.IsNullOrEmpty(condition) && (result.Length > 0))
            {
                result.Append(s_nl + s_end);
            }
        }

        public void Add(PreProcessorData data)
        {
            if ((data != null) && (data.m_streams.Length == m_streams.Length))
            {
                for(int i = 0; i < m_streams.Length; i++)
                {
                    AddStream(m_streams[i], data.m_streams[i]);
                }
            }
        }

        public void Merge(string condition, PreProcessorData ifData, PreProcessorData elseData)
        {
            if ((ifData != null) && (elseData != null))
            {
                for (int i = 0; i < m_streams.Length; i++)
                {
                    MergeStream(m_streams[i], condition, ifData.m_streams[i], elseData.m_streams[i]);
                }
            }
        }
    }
}
