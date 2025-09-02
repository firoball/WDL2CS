using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    abstract class PropertyTransformer
    {
        protected bool m_excludeProperties;

        public PropertyTransformer() : this(false) { }

        public PropertyTransformer(bool excludeProperties)
        {
            m_excludeProperties = excludeProperties;
        }

        public virtual void Transform(object obj, Node name, List<Node> values, Node owner)
        {
        }
    }
}
