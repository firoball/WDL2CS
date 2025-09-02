using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS.Transformers.CS
{
    class CSPropertyTransformer : PropertyTransformer
    {
        public override void Transform(object obj, Node name, List<Node> values, Node owner)
        {
            StringBuilder sb = (StringBuilder)obj;
            string sowner = owner.Data.ToLower(); //TEMP
            string p = string.Empty;
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
                            pname = "Val";
                        goto default;

                    case "type":
                        sb.Append($"{pname} = {values[0]}");
                        break;

                    case "skill1":
                    case "skill2":
                    case "skill3":
                    case "skill4":
                    case "skill5":
                    case "skill6":
                    case "skill7":
                    case "skill8":
                        sb.Append($"{pname} = new Skill({values[0]})");
                        break;

                    //collect all panel control definitions for generation of multi-dimensional array
                    case "digits":
                    case "hbar":
                    case "vbar":
                        sb.Append($"new {pname}({values[0]}, {values[1]}, {values[2]}, {values[3]}, {values[4]}, () => {{ return {values[5]}; }} )");
                        break;

                    case "hslider":
                    case "vslider":
                        sb.Append($"new {pname}({values[0]}, {values[1]}, {values[2]}, {values[3]}, () => {{ return {values[4]}; }} )");
                        break;

                    case "picture":
                        sb.Append($"new {pname}({values[0]}, {values[1]}, {values[2]}, () => {{ return {values[3]}; }} )");
                        break;

                    case "window":
                        sb.Append($"new {pname}({values[0]}, {values[1]}, {values[2]}, {values[3]}, {values[4]}, () => {{ return {values[5]}; }}, () => {{ return {values[6]}; }} )");
                        break;

                    case "button":
                        sb.Append($"new {pname}({values[0]}, {values[1]}, {values[2]}, {values[3]}, {values[4]}, {values[5]})");
                        break;

                    case "flags":
                        values.ForEach(x => x.NodeType = NodeType.Flag);
                        sb.Append($"{pname} = {string.Join(" | ", values)}");
                        break;

                    case "range":
                        //collect all range definitions for generation of multi-dimensional array
                        sb.Append($"{{{string.Join(", ", values)}}}");
                        break;

                    case "bmaps":
                        sb.Append($"Bmaps = new Bmap[] {{ {string.Join(", ", values)} }}");
                        break;

                    case "ovlys":
                        sb.Append($"Ovlys = new Ovly[] {{ {string.Join(", ", values)} }}");
                        break;

                    case "offset_x":
                    case "offset_y":
                        //for Wall only these properties are one-dimensional
                        if (!sowner.Equals("wall"))
                            sb.Append($"{pname} = new Var[] {{ {string.Join(", ", values)} }}");
                        else
                            goto default;
                        break;

                    case "string":
                        //fix name ambiguity for Text objects
                        if (sowner.Equals("text"))
                            sb.Append($"String_array = new string[] {{ {string.Join(", ", values)} }}");
                        else
                            goto default;
                        break;

                    //these properties always require array instantiation
                    case "mirror":
                    case "delay":
                    case "scycles":
                        sb.Append($"{pname} = new Var[] {{ {string.Join(", ", values)} }}");
                        break;

                    case "scale_xy":
                        sb.Append($"{pname} = new Var[] {{ {string.Join(", ", values)} }}");
                        break;

                    case "target":
                        sb.Append($"{pname} = {values[0]}");
                        break;

                    default:
                        if (values.Count > 1)
                            sb.Append($"{pname} = new [] {{ {string.Join(", ", values)} }}");
                        else
                            sb.Append($"{pname} = {values[0]}");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("(E) CSPROPERTYTRANSFORMER: " + e);
            }

        }

    }
}
