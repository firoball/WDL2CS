using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    abstract class ActionTransformer
    {
        protected bool m_excludeProperties;

        public ActionTransformer() : this(false) { }

        public ActionTransformer(bool excludeProperties)
        {
            m_excludeProperties = excludeProperties;
        }

        public virtual void Transform(object obj, Node name, List<Instruction> instructions)
        {
        }
    }
}
