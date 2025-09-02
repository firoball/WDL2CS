using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS.Transformers.CS
{
    class CSGlobalTransformer : GlobalTransformer
    {
        private static readonly string s_indent = "\t\t\t";

        private static Dictionary<string, string> s_types = new Dictionary<string, string>
        {
            { "each_tick", "Function" },
            { "each_sec", "Function" },
            { "panels", "Panel" },
            { "layers", "Overlay" },
            { "messages", "Text" }
        };

        public override void Transform(object obj, Node name, List<Node> parameters)
        {
            StringBuilder sb = (StringBuilder)obj;

            if (parameters.Count > 1)
            {
                if (s_types.TryGetValue(name.Data.ToLower(), out string type))
                {
                    string pars = string.Join(", ", parameters);
                    sb.Append($"{s_indent}{name} = new {type}[] {{{pars}}};");
                }
                else
                {
                    Console.WriteLine("(W) GLOBAL discarded malformed type: " + name.Data);
                }
            }
            else
            {
                name = Util.UpdateSkill(name); //Global skills need their property
                sb.Append($"{s_indent}{name} = {parameters[0]};");
            }

        }
    }
}
