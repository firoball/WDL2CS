using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS.Transformers.WDL
{
    class WDLGlobalTransformer : GlobalTransformer
    {

        public override void Transform(object obj, Node name, List<Node> parameters)
        {
            StringBuilder sb = (StringBuilder)obj;

            if (parameters.Count > 1)
            {
                string pars = string.Join(", ", parameters);
                sb.Append($"{name} {pars};");
            }
            else
            {
                name = Util.UpdateSkill(name); //Global skills need their property - TODO: needed for WDL?
                sb.Append($"{name} {parameters[0]};");
            }

        }
    }
}
