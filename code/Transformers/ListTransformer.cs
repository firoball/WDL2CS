using System;
using System.Collections.Generic;
using System.Text;
using WDL2CS.Transformers.List;

namespace WDL2CS
{
    public class ListTransformer : Transformer
    {
        PropertyList m_list;

        public Dictionary<string, Dictionary<string, Dictionary<string, List<List<string>>>>> List { get => m_list.List; }

        public ListTransformer()
        {
            m_list = new PropertyList();
        }

        protected override void Activate()
        {
            Node.Transformer = new ListNodeTransformer();
            Global.Transformer = null; //not supported/required
            Instruction.Transformer = null; //not supported/required
            Property.Transformer =  new ListPropertyTransformer();
            Action.Transformer = null; //not supported/required
            Asset.Transformer = new ListAssetTransformer();
            Object.Transformer = new ListObjectTransformer();
        }

        protected override void Transform()
        {
            foreach (ISection section in Sections.List)
            {
                section.Transform(m_list);
            }
        }

    }
}
