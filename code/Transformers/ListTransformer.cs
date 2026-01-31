using System;
using System.Collections.Generic;
using System.Text;
using WDL2CS.Transformers.List;

namespace WDL2CS
{
    public class ListTransformer : Transformer
    {
        PropertyList m_list;
        bool m_sort;

        public Dictionary<string, Dictionary<string, Dictionary<string, List<List<Tuple<string, string>>>>>> List { get => m_list.List; }

        public ListTransformer() : this(false) { }
        public ListTransformer(bool sort)
        {
            m_list = new PropertyList();
            m_sort = sort;
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
            if (!m_sort)
            {
                TransformSections(Sections.List);
            }
            else
            {
                List<ISection>[] sections = Sections.GetSortedSections();
                foreach (List<ISection> sectionList in sections)
                {
                    TransformSections(sectionList);
                }
            }
        }

        private void TransformSections(List<ISection> sections)
        {
            foreach (ISection section in sections)
            {
                section.Transform(m_list);
            }
        }
    }
}
