using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    abstract class ObjectTransformer
    {
        protected bool m_excludeProperties;

        public ObjectTransformer() : this(false) { }

        public ObjectTransformer(bool excludeProperties)
        {
            m_excludeProperties = excludeProperties;
        }

        public virtual void Transform(object obj, Node name, Node type, Node str, List<Property> properties, bool isInitialized)
        {
        }
    }
}
