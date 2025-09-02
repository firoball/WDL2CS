using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace WDL2CS
{
    abstract class GlobalTransformer
    {
        protected bool m_excludeProperties;

        public GlobalTransformer() : this(false) { }

        public GlobalTransformer(bool excludeProperties)
        {
            m_excludeProperties = excludeProperties;
        }

        public virtual void Transform(object obj, Node name, List<Node> parameters)
        {
        }
    }
}
