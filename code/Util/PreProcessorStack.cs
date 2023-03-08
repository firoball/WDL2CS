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

        public bool Contains(string name)
        {
            return m_layer.Contains(name);
        }

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

            public Layer() : this(string.Empty) { }

            public Layer(string condition)
            {
                m_condition = condition;
                m_isElse = false;
                m_contentIf = new T() { };
                m_nextIf = new List<Layer>();
                m_nextElse = new List<Layer>();

                m_contentElse = new T() { };

            }

            public bool Contains(string name) //TODO: concept does not work... refactor this for detecting of shadowing parent layer defs
            {
                //reached stack head - there won't be any other duplicates
                if (m_prev == null)
                    return false;

                //check the child lists of parent layer
                //there might be children with identical conditions carrying identical section identifiers
                //detect same condition of active layer, but include active layer from self-comparison
                IEnumerable<Layer> candidates;
                if (m_prev.m_isElse)
                {
                    candidates = m_prev.m_nextElse.Where(x => (x != this) && (x.m_condition == m_condition));
                }
                else
                {
                    candidates = m_prev.m_nextIf.Where(x => (x != this) && (x.m_condition == m_condition));
                }
                return candidates.Where(x => x.Content.Contains(name)).FirstOrDefault() != null;
            }

            public Layer Add(string condition)
            {
                Layer next = new Layer(condition)
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
                /*
                //                return next;
                if (m_isElse)
                {
                    Layer next = m_nextElse.Where(x => x.m_condition == condition).FirstOrDefault();
                    if (next == null)
                    {
                        Console.WriteLine("STACK new else-layer for: " + m_condition + " next " + condition);
                        next = new Layer(condition)
                        {
                            m_prev = this
                        };
                        m_nextElse.Add(next);
                    }
                    else
                        Console.WriteLine("STACK found else-layer for: " + condition);
                    return next;
                }
                else
                {
                    Layer next = m_nextIf.Where(x => x.m_condition == condition).FirstOrDefault();
                    if (next == null)
                    {
                        Console.WriteLine("STACK new if-layer for: " + m_condition + " next " + condition);
                        next = new Layer(condition)
                        {
                            m_prev = this
                        };
                        m_nextIf.Add(next);
                    }
                    else
                        Console.WriteLine("STACK found if-layer for: " + condition);

                    return next;
 //                   m_nextIf.Add(next);
                }
                */
                return next;
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
                T content = new T();
                content.Merge(m_condition, m_contentIf, m_contentElse);
                return content;
            }

        }

    }

}
