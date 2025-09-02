using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS.Transformers.CS
{
    class CSActionTransformer : ActionTransformer
    {
        private static string s_indent = string.Empty;
        private static int s_indents = 2;
        private static readonly string s_nl = Environment.NewLine;

        private Dictionary<int, Node> m_conditions;

        public override void Transform(object obj, Node name, List<Instruction> instructions)
        {
            StringBuilder sb = (StringBuilder)obj;
            bool interruptable = false;
            string instName = name.ToString();
            string className = "Action__" + instName;

            //Update instruction list in order to make it compatible to C#
            interruptable = IsInterruptable(instructions);
            m_conditions = new Dictionary<int, Node>();

            sb.Append(UpdateIndent("private class " + className + " : Function<" + className + ">"));
            sb.Append(UpdateIndent("{"));
            sb.Append(UpdateIndent("public " + className + " () : base ()"));
            sb.Append(UpdateIndent("{"));
            if (interruptable)
            {
                sb.Append(UpdateIndent("Interruptable = true;"));
            }
            sb.Append(UpdateIndent("Name = \"" + instName + "\";"));
            sb.Append(UpdateIndent("}"));
            sb.Append(UpdateIndent("public override IEnumerator Logic()"));
            sb.Append(UpdateIndent("{"));

            Instruction last = null;

            if (instructions != null)
            {
                foreach (Instruction inst in instructions)
                {
                    try
                    {
                        //WDL allows isolated "else" (bug of scripting language)
                        //keep track of the last if, so the isolated "else" can be fixed with a negated condition
                        if (inst.Command.StartsWith("if", StringComparison.OrdinalIgnoreCase) && inst.Parameters.Count > 0)
                        {
                            PushCondition(inst.Parameters[0]);
                        }
                        //special case: "else" was found without previous closing bracket
                        //take last stored "if"-condiftion, negate it and replace "else" with an "if"
                        if ((last != null) && !last.Command.StartsWith("}") && inst.Command.StartsWith("else", StringComparison.OrdinalIgnoreCase))
                        {
                            Instruction patch;
                            Node condNode = PopCondition();
                            if (string.IsNullOrEmpty(condNode.ToString()))//TODO: find edge case and check how Node behaves. Adjust check condition
                            {
                                Console.WriteLine("(W) ACTION ELSE without IF detected - disabling");
                                patch = new Instruction(new Node("if"), new Node("(false) //disabled by transpiler"));
                            }
                            else
                            {
                                Console.WriteLine("(W) ACTION patched isolated ELSE");
                                patch = new Instruction(new Node("if"), new Node(new Node("(!"), condNode, new Node(")")));
                            }
                            sb.Append(UpdateIndent(patch.Command, patch.Format(instName)));
                        }
                        //regular path
                        else
                        {
                            Instruction temp = inst;
                            if (inst.Command.StartsWith("else", StringComparison.OrdinalIgnoreCase) && (PopCondition() == null))
                            {
                                Console.WriteLine("(W) ACTION ELSE without IF detected - disabling");
                                temp = new Instruction(new Node("if"), new Node("(false) //disabled by transpiler"));
                            }
                            string f = temp.Format(instName);
                            if (!string.IsNullOrEmpty(f))
                                sb.Append(UpdateIndent(temp.Command, f));
                        }

                        last = inst;
                    }
                    catch
                    {
                        Console.WriteLine("(E) ACTION unexpected number of parameters: " + instName + " " + inst.Command);
                    }
                }
            }
            sb.Append(UpdateIndent("}"));
            sb.Append(UpdateIndent("}"));
            string c = "public static Function " + instName + " = new " + className + "()";

            {
                sb.Append(UpdateIndent(c + ";"));
            }
        }
        private bool IsInterruptable(List<Instruction> instructions)
        {
            bool interruptable = false;
            if (instructions != null)
            {
                for (int i = instructions.Count - 1; i >= 0; i--)
                {
                    if (instructions[i].Command.StartsWith("Wait", StringComparison.OrdinalIgnoreCase) || instructions[i].Command.StartsWith("Inkey", StringComparison.OrdinalIgnoreCase))
                    {
                        interruptable = true;
                        break;
                    }
                }
            }

            return interruptable;
        }

        private string UpdateIndent(string s)
        {
            return UpdateIndent(s, s);
        }

        private string UpdateIndent(string s, string t)
        {
            if (s[0] == '{')
            {
                BuildIndent();
                s_indents++;
            }
            else if (s[0] == '}')
            {
                s_indents--;
                BuildIndent();
            }
            else
            {
                BuildIndent();
            }
            return s_indent + t + s_nl;
        }

        private void BuildIndent()
        {
            s_indent = new string('\t', s_indents);
        }

        private void PushCondition(Node cond)
        {
            m_conditions[s_indents] = cond;
        }

        private Node PopCondition()
        {
            if (m_conditions.TryGetValue(s_indents, out Node cond))
            {
                m_conditions.Remove(s_indents);
                return cond;
            }
            else
            {
                return null;
            }
        }

    }
}
