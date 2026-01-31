using System;
using System.Text;
using WDL2CS.Transformers.WDL;

namespace WDL2CS
{
    public class WDLTransformer : Transformer
    {

        protected override void Activate()
        {
            Node.Transformer = new WDLNodeTransformer();
            Global.Transformer = new WDLGlobalTransformer();
            Instruction.Transformer = new WDLInstructionTransformer();
            Property.Transformer = new WDLPropertyTransformer();
            Action.Transformer = new WDLActionTransformer();
            Asset.Transformer = new WDLAssetTransformer();
            Object.Transformer = new WDLObjectTransformer();
        }

        protected override void Transform()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ISection section in Sections.List)
            {
                section.Transform(sb);
                sb.AppendLine();
            }

            m_result = sb.ToString();
        }

    }
}
