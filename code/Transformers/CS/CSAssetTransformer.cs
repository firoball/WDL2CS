using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS.Transformers.CS
{
    class CSAssetTransformer : AssetTransformer
    {
        private static readonly string s_indent = "\t\t";
        private static readonly string s_scope = "public static";

        public CSAssetTransformer(bool excludeProperties) : base(excludeProperties) { }

        public override void Transform(object obj, Node name, Node type, Node file, List<Node> parameters)
        {
            StringBuilder sb = (StringBuilder)obj;
            string stype = type.ToString();
            string sname = name.ToString();

            sb.Append($"{s_indent}{s_scope} {stype} {sname} = new {stype}(\"{sname}\"");
            if (!m_excludeProperties)
            {
                string pars = string.Empty;
                if (parameters != null && parameters.Count > 0)
                {
                    pars = ", " + string.Join(", ", parameters);
                }
                sb.Append($", {file}{pars}");
            }
            sb.Append(");");
        }
    }
}
