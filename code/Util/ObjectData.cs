using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class ObjectData : PreProcessorData
    {
        private List<Property> m_properties;
        private Object m_parent;

        public ObjectData() : base(3)
        {
            m_properties = new List<Property>();
        }

        public List<Property> Properties { get => m_properties; }
        public StringBuilder RangeStream { get => m_streams[0]; }
        public StringBuilder ControlStream { get => m_streams[1]; }
        public StringBuilder PropertyStream { get => m_streams[2]; }
        public Object Parent { set => m_parent = value; }

        public override bool Contains(string name)
        {
            return m_properties.Where(x => x.Name.Equals(name)).FirstOrDefault() != null;
        }

        public override void Format()
        {
            //if/else branch may not have been populated and therefore m_parent may be null
            if (m_parent != null)
            {
                //Formatting is done in Object class itself due to dependency on internal parameters
                m_parent.ProcessObjectData(this);
                m_parent = null; //decouple for GC
            }
        }
    }
}
