using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS.Transformers.WDL
{
    class WDLAssetTransformer : AssetTransformer
    {

        public override void Transform(object obj, Node name, Node type, Node file, List<Node> parameters)
        {
            StringBuilder sb = (StringBuilder)obj;
            string stype = type.ToString();
            string sname = name.ToString();

            sb.Append($"{stype} {sname}");
            string pars = string.Empty;
            if (parameters != null && parameters.Count > 0)
            {
                pars = ", " + string.Join(", ", parameters);
            }
            sb.Append($", {file}{pars};");
        }
    }
}
