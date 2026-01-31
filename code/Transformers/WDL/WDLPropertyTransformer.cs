using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS.Transformers.WDL
{
    class WDLPropertyTransformer : PropertyTransformer
    {
        public override void Transform(object obj, Node name, List<Node> values, Node owner)
        {
            StringBuilder sb = (StringBuilder)obj;
            string sowner = owner.Data.ToLower(); //TEMP
            string sname = name.Data.ToLower(); //TEMP
            string pname = name.ToString();

            try
            {
                //TODO: add List length checks for explicite array access
                switch (sname)
                {
                    case "default":
                        //Skill only: rename "Default" property to "Val" (undocumented)
                        if (sowner.Equals("skill"))
                            pname = "VAL";
                        goto default;

                    case "flags":
                        values.ForEach(x => x.NodeType = NodeType.Flag);
                        goto default;

                    default:
                        if (values.Count > 1)
                            sb.Append($"{pname} = {string.Join(", ", values)}");
                        else
                            sb.Append($"{pname} = {values[0]}");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("(E) WDLPROPERTYTRANSFORMER: " + e);
            }

        }

    }
}
