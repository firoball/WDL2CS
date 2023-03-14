using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class ObjectData : PreProcessorData
    {
        private List<Property> m_properties;
        private Object m_parentObject;

        public ObjectData() : base(4)
        {
            m_properties = new List<Property>();
        }

        public List<Property> Properties { get => m_properties; }
        public StringBuilder RangeStream { get => m_streams[0]; }
        public StringBuilder ControlStream { get => m_streams[1]; }
        public StringBuilder PropertyStream { get => m_streams[2]; }
        public StringBuilder ShadowStream { get => m_streams[3]; }
        public Object ParentObject { set => m_parentObject = value; }

        public override bool Contains(string name)
        {
            return m_properties.Where(x => x.Name.Equals(name)).FirstOrDefault() != null;
        }

        public override void Format()
        {
            //if/else branch may not have been populated and therefore m_parent may be null
            if (m_parentObject != null)
            {
                //Formatting is done in Object class itself due to dependency on internal parameters
                m_parentObject.ProcessObjectData(this);
                m_parentObject = null; //decouple for GC
            }
        }

    }
}
