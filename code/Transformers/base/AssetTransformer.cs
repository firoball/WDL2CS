using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    abstract class AssetTransformer
    {
        protected bool m_excludeProperties;

        public AssetTransformer() : this(false) { }

        public AssetTransformer(bool excludeProperties)
        {
            m_excludeProperties = excludeProperties;
        }

        public virtual void Transform(object obj, Node name, Node type, Node file, List<Node> parameters)
        {
        }
    }
}
