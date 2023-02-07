using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class PreProcessorStack<T> where T : PreProcessorData, new()
    {
        private Layer m_layer;
        private Layer m_head;

        public PreProcessorStack()
        {
            m_head = new Layer();
            m_layer = m_head;
        }

        public T Update(string preproc)
        {
            return Update(preproc, string.Empty);
        }

        public T Update (string preprocessor, string condition)
        {
            switch (preprocessor)
            {
                case "#if":
                    m_layer = m_layer.Add(preprocessor, condition);
                    break;

                case "#else":
                    m_layer.Switch(preprocessor);
                    break;

                case "#endif":
                    m_layer = m_layer.Close(preprocessor);
                    break;

                default:
                    break;
            }
            return m_layer.Content;
        }

        public T Content { get => m_layer.Content; }
        public string Condition { get => m_layer.Condition; }

        public T Merge()
        {
            return m_head.Merge();
        }

        class Layer
        {
            private readonly string m_condition;
            private Layer m_nextIf;
            private Layer m_nextElse;
            private Layer m_prev;
            private readonly T m_contentIf = default(T);
            private T m_contentElse = default(T);
            private bool m_isElse;

            public string Condition { get => m_condition; }
            public T Content { get => m_isElse ? m_contentElse : m_contentIf; }

            public Layer() : this(string.Empty, string.Empty) { }

            public Layer(string preprocessor, string condition)
            {
                m_condition = condition;
                m_isElse = false;
                m_contentIf = new T() { };
            }

            public Layer Add(string preprocessor, string condition)
            {
                Layer next = new Layer(preprocessor, condition)
                {
                    m_prev = this
                };

                if (m_isElse)
                {
                    m_nextElse = next;
                }
                else
                {
                    m_nextIf = next;
                }

                return next;
            }

            public Layer Close(string preprocessor)
            {
                return m_prev;
            }

            public void Switch(string preprocessor)
            {
                m_isElse = true;
                m_contentElse = new T() { };
            }

            public T Merge()
            {
                T next;
                if (m_nextIf != null)
                {
                    next = m_nextIf.Merge();
                    m_contentIf.Add(next);
                }
                if (m_nextElse != null)
                {
                    next = m_nextElse.Merge();
                    m_contentElse.Add(next);
                }
                T content = new T();
                content.Merge(m_condition, m_contentIf, m_contentElse);
                return content;
            }

        }

    }

}
