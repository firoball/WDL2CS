using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Action : ISerializable
    {
        private static readonly string s_sepAct = "#[A]#";
        private static string s_indent = string.Empty;
        private static int s_indents = 2;
        private static readonly string s_nl = Environment.NewLine;

        private string m_name;
        private readonly string m_serializedInstructions;
        private List<Instruction> m_instructions;

        public string Name { get => m_name; set => m_name = value; }

        public Action(string name, string instructions)
        {
            m_name = name;
            m_serializedInstructions = instructions;
            m_instructions = null;
        }

        public Action(string name, List<Instruction> instructions)
        {
            m_name = name;
            m_serializedInstructions = string.Empty;
            m_instructions = instructions;
        }

        public Action() : this(string.Empty, string.Empty) { }
        public Action(string name) : this(name, string.Empty) { }

        public bool IsInitialized()
        {
            return false;
        }

        public string Serialize()
        {
            string s = m_name + s_sepAct + m_serializedInstructions;
            return s;
        }

        public static Action Deserialize(string stream)
        {
            List<Instruction> instructions = null;

            string[] fragments = stream.Split(new[] { s_sepAct }, StringSplitOptions.None);
            string name = fragments[0];
            if (!string.IsNullOrEmpty(fragments[1]))
            {
                instructions = Instruction.DeserializeList(fragments[1]);
            }
            return new Action(name, instructions);
        }

        public string Format()
        {
            string s = string.Empty;
            bool interruptable = false;
            string instName = Formatter.FormatIdentifier(m_name);

            //Update instruction list in order to make it compatible to C#
            interruptable = ProcessInstructions();

            s += UpdateIndent("private class " + m_name + " : Function<" + m_name + ">");
            s += UpdateIndent("{");
            s += UpdateIndent("public override IEnumerator Logic()");
            s += UpdateIndent("{");

            Instruction lastif = null;
            Instruction last = null;

            foreach (Instruction inst in m_instructions)
            {
                //WDL allows isolated "else" (bug of scripting language)
                //keep track of the last if, so the isolated "else" can be fixed with a negated condition
                if (inst.Command.StartsWith("if"))
                {
                    lastif = inst;
                }
                //special case: "else" was found without previous closing bracket
                //take last stored "if"-condiftion, negate it and replace "else" with an "if"
                if ((last != null) && !last.Command.StartsWith("}") && inst.Command.StartsWith("else"))
                {
                    Instruction patch = new Instruction(lastif.Command, "(!" + lastif.Parameters[0] + ")");
                    s += UpdateIndent(patch.Command, patch.Format(instName));
                }
                //regular path
                else
                {
                    string f = inst.Format(instName);
                    if (!string.IsNullOrEmpty(f))
                        s += UpdateIndent(inst.Command, f);
                }
                

                last = inst;
            }
            s += UpdateIndent("}");
            s += UpdateIndent("}");
            string c = "public static Function " + instName + " = new " + m_name + "()";

            //flag any action which was identified as interruptable
            if (interruptable)
            {
                s += UpdateIndent(c);
                s += UpdateIndent("{");
                s += UpdateIndent("Interruptable = true");
                s += UpdateIndent("};");
            }
            else
            {
                s += UpdateIndent(c + ";");
            }

            return s;
        }

        private bool ProcessInstructions()
        {
            bool interruptable = false;
            Dictionary<int, Instruction> inserts = new Dictionary<int, Instruction>();

            //iterate through instruction list from end, so that new instructions can be inserted directly
            for (int i = m_instructions.Count - 1; i >= 0; i--)
            {
                if (m_instructions[i].Command.StartsWith("Wait") || m_instructions[i].Command.StartsWith("Inkey"))
                {
                    interruptable = true;
                }

                //Transform "Skip" to "goto" and jump marker
                if (m_instructions[i].Command.StartsWith("Skip"))
                {
                    //Console.WriteLine(name);
                    int index = FindIndex(i, Convert.ToInt32(m_instructions[i].Parameters[0]));
                    //Add additional parameter carrying the jump label
                    m_instructions[i].Parameters.Add("skip" + i + "to" + index);
                    //create new instruction for jump marker which is to be inserted
                    Instruction marker = new Instruction(Formatter.FormatMarker(m_instructions[i].Parameters[1]), false);
                    //instruction needs to be inserted above current one. Buffer and insert later
                    //otherwise iteration can break on direct insertion
                    if (index < i)
                    {
                        inserts.Add(index, marker);
                    }
                    else
                    {
                        m_instructions.Insert(index, marker);
                    }
                }

                //add brackets for "If_..." instructions, since these are transformed to bare "if"
                if (m_instructions[i].Command.StartsWith("If_") ||
                    (m_instructions[i].Command.StartsWith("Else") && (i < m_instructions.Count - 1) && (m_instructions[i + 1].Command[0] != '{')))
                {
                    if (i < m_instructions.Count - 1)
                    {
                        m_instructions.Insert(i + 2, new Instruction("}", false));
                        m_instructions.Insert(i + 1, new Instruction("{", false));
                    }
                    else
                    {
                        //in case no instruction follows after if_*, remove instruction
                        Console.WriteLine("(W) INSTRUCTIONS remove incomplete statement: " + m_instructions[i].Format(m_name));
                        m_instructions.RemoveAt(i);
                    }
                }

                //check if any buffered instructions need to be inserted at current position
                if (inserts.TryGetValue(i, out Instruction insert))
                {
                    m_instructions.Insert(i, insert);
                }
            }
            //always yield at end of function!
            if (!m_instructions.Last().Command.Equals("Branch") &&
                !m_instructions.Last().Command.Equals("End") &&
                !m_instructions.Last().Command.Equals("Goto") &&
                !m_instructions.Last().Command.Equals("Exit")
                )
                m_instructions.Add(new Instruction("yield break;", false));

            return interruptable;
        }

        private string UpdateIndent(string s)
        {
            return UpdateIndent(s, s);
        }

        private string UpdateIndent(string s, string t)
        {
            if (s[0] == '#')
            {
                int i = s_indents;
                s_indents = 0;
                BuildIndent();
                s_indents = i;
            }
            else if (s[0] == '{')
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

        private int FindIndex(int startIndex, int count)
        {
            int progress = 0;
            int i = startIndex;

            while (progress != count)
            {
                i += Math.Sign(count);
                if (m_instructions[i].Count)
                    progress += Math.Sign(count);
            }
            if (count > 0)
            {
                //correct index by one for positive skips only
                i++;
                //make sure marker is not placed before closing bracket, but moved after
                while ((i < m_instructions.Count) && m_instructions[i].Command.StartsWith("}"))
                    i++;
            }

            return i;
        }

    }
}
