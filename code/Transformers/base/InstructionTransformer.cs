using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    abstract class InstructionTransformer
    {
        protected bool m_excludeProperties;

        public InstructionTransformer() : this(false) { }

        public InstructionTransformer(bool excludeProperties)
        {
            m_excludeProperties = excludeProperties;
        }

        public virtual void Transform(object obj, Node command, List<Node> parameters)
        {
        }
    }
}
