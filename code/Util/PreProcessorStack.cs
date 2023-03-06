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
            m_layer = new Layer();
            m_head = m_layer;
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
            private List<Layer> m_nextIf;
            private List<Layer> m_nextElse;
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
                m_nextIf = new List<Layer>();
                m_nextElse = new List<Layer>();
            }

            public Layer Add(string preprocessor, string condition)
            {
                //TODO: append to layer with identical condition instead of creating new one
                Layer next = new Layer(preprocessor, condition)
                {
                    m_prev = this
                };

                if (m_isElse)
                {
                    m_nextElse.Add(next);
                }
                else
                {
                    m_nextIf.Add(next);
                }

                return next;
            }

            public Layer Close(string preprocessor)
            {
                m_isElse = false;
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
                foreach (Layer layer in m_nextIf)
                {
                    next = layer.Merge();
                    m_contentIf.Add(next);
                }
                foreach (Layer layer in m_nextElse)
                {
                    next = layer.Merge();
                    m_contentElse.Add(next);
                }
                T content = new T();
                content.Merge(m_condition, m_contentIf, m_contentElse);
                return content;
            }

        }

    }

}
