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
                    m_layer = m_layer.Add(condition);
                    break;

                case "#else":
                    m_layer.Switch();
                    break;

                case "#endif":
                    m_layer = m_layer.Close();
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

            public Layer() : this(string.Empty, null) { }
            public Layer(string condition) : this(condition, null) { }

            public Layer(string condition, T contentParent)
            {
                m_condition = condition;
                m_isElse = false;
                m_contentIf = new T() { Parent = contentParent };
                m_contentElse = new T() { Parent = contentParent };
                m_nextIf = new List<Layer>();
                m_nextElse = new List<Layer>();
            }

            public Layer Add(string condition)
            {
                //check if child layer with same condition already exists and use that one
                //otherwise create and add fresh child layer
                //reusing layers allows removal of duplicate definitions easily
                if (m_isElse)
                {
                    Layer next = m_nextElse.Where(x => x.m_condition == condition).FirstOrDefault();
                    if (next == null)
                    {
                        next = new Layer(condition, m_contentElse)
                        {
                            m_prev = this
                        };
                        m_nextElse.Add(next);
                    }
                    return next;
                }
                else
                {
                    Layer next = m_nextIf.Where(x => x.m_condition == condition).FirstOrDefault();
                    if (next == null)
                    {
                        next = new Layer(condition, m_contentIf)
                        {
                            m_prev = this
                        };
                        m_nextIf.Add(next);
                    }
                    return next;
                }
            }

            public Layer Close()
            {
                m_isElse = false;
                return m_prev;
            }

            public void Switch()
            {
                m_isElse = true;
            }

            public T Merge()
            {
                //Step 1: format layer contents and merge sub layer contents
                T next;
                m_contentIf.Format();
                foreach (Layer layer in m_nextIf)
                {
                    next = layer.Merge();
                    m_contentIf.Add(next);
                }
                m_contentElse.Format();
                foreach (Layer layer in m_nextElse)
                {
                    next = layer.Merge();
                    m_contentElse.Add(next);
                }

                //Step 2: merge all layer contents and preprocessor conditions
                T content = new T();
                content.Merge(m_condition, m_contentIf, m_contentElse);
                return content;
            }

        }

    }

}
