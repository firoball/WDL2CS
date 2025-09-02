using System;
using System.Text;
using WDL2CS.Transformers.CS;

namespace WDL2CS
{
    public class CSTransformer : Transformer
    {
        bool m_excludeProperties = false;
        string m_scriptName;

        public CSTransformer() : this("Script", false) { }

        public CSTransformer(string scriptName, bool excludeProperties)
        {
            m_excludeProperties = excludeProperties;
            m_scriptName = scriptName;
        }

        protected override void Activate()
        {
            Node.Transformer = new CSNodeTransformer();
            Global.Transformer = new CSGlobalTransformer();
            Instruction.Transformer = new CSInstructionTransformer();
            Property.Transformer = new CSPropertyTransformer();
            Action.Transformer = new CSActionTransformer();
            Asset.Transformer = new CSAssetTransformer(m_excludeProperties);
            Object.Transformer = new CSObjectTransformer(m_excludeProperties);
        }

        protected override void Transform()
        {
            StringBuilder sb = new StringBuilder();
            Node className = new Node(m_scriptName, NodeType.Reserved);

            sb.Append(@"
using System.Collections;
using Acknex3.Api;

namespace Acknex3.Script
{
	class " + className + @"
	{
		public void Initialize()
		{
");
            ProcessSections(sb, true); //initialized data
            sb.Append(@"
		}
");
            ProcessSections(sb, false); //static data
            sb.Append(@"
	}
}
");
            m_result = sb.ToString();
        }

        private void ProcessSections(StringBuilder sb, bool isInitialized)
        {
            foreach (ISection section in Sections.List)
            {
                if (section.IsInitialized() == isInitialized)
                {
                    section.Transform(sb);
                    sb.AppendLine();
                }
            }
        }


    }
}
