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
        private Dictionary<int, string> s_conditions = new Dictionary<int, string>();

        public string Name { get => m_name; set => m_name = value; }
        public string Type { get => "Action"; }

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
            string className = Formatter.FormatActionClass(m_name);
            string instName = m_name;

            //Update instruction list in order to make it compatible to C#
            interruptable = ProcessInstructions();

            s += UpdateIndent("private class " + className + " : Function<" + className + ">");
            s += UpdateIndent("{");
            s += UpdateIndent("public override IEnumerator Logic()");
            s += UpdateIndent("{");

            Instruction last = null;

            if (m_instructions != null)
            {
                foreach (Instruction inst in m_instructions)
                {
                    try
                    {
                        //WDL allows isolated "else" (bug of scripting language)
                        //keep track of the last if, so the isolated "else" can be fixed with a negated condition
                        if (inst.Command.StartsWith("if") && inst.Parameters.Count > 0)
                        {
                            PushCondition(inst.Parameters[0]);
                        }
                        //special case: "else" was found without previous closing bracket
                        //take last stored "if"-condiftion, negate it and replace "else" with an "if"
                        if ((last != null) && !last.Command.StartsWith("}") && inst.Command.StartsWith("else"))
                        {
                            Instruction patch;
                            string cond = PopCondition();
                            if (string.IsNullOrEmpty(cond))
                            {
                                Console.WriteLine("(W) ACTION ELSE without IF detected - disabling");
                                patch = new Instruction("if", "(false) //disabled by transpiler");
                            }
                            else
                            {
                                Console.WriteLine("(W) ACTION patched isolated ELSE");
                                patch = new Instruction("if", "(!" + cond + ")");
                            }
                            s += UpdateIndent(patch.Command, patch.Format(instName));
                        }
                        //regular path
                        else
                        {
                            Instruction temp = inst;
                            if (inst.Command.StartsWith("else") && string.IsNullOrEmpty(PopCondition()))
                            {
                                Console.WriteLine("(W) ACTION ELSE without IF detected - disabling");
                                temp = new Instruction("if", "(false) //disabled by transpiler");
                            }
                            string f = temp.Format(instName);
                            if (!string.IsNullOrEmpty(f))
                                s += UpdateIndent(temp.Command, f);
                        }

                        last = inst;
                    }
                    catch 
                    {
                        Console.WriteLine("(E) ACTION unexpected number of parameters: " + instName + " " + inst.Command);
                    }
                }
            }
            s += UpdateIndent("}");
            s += UpdateIndent("}");
            string c = "public static Function " + instName + " = new " + className + "()";

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

            //any instructions in action
            if (m_instructions != null)
            {
                Dictionary<int, Instruction> inserts = new Dictionary<int, Instruction>();

                //iterate through instruction list from end, so that new instructions can be inserted directly
                for (int i = m_instructions.Count - 1; i >= 0; i--)
                {
                    if (m_instructions[i].Command.StartsWith("Wait") || m_instructions[i].Command.StartsWith("Inkey"))
                    {
                        interruptable = true;
                    }

                    //Transform "Skip" to "goto" and jump marker
                    else if (m_instructions[i].Command.StartsWith("Skip"))
                    {
                        //standard case: Skip comes with number of lines to skip as parameter
                        if (Int32.TryParse(m_instructions[i].Parameters[0], out int num))
                        {
                            //Console.WriteLine(name);
                            int index = FindIndex(i, num);
                            if (index > -1)
                            {
                                //Add additional parameter carrying the jump label
                                m_instructions[i].Parameters.Add("skip" + i + "to" + index);
                                //create new instruction for jump marker which is to be inserted
                                Instruction marker = new Instruction(Formatter.FormatGotoMarker(m_instructions[i].Parameters[1]), false);
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
                            else
                            {
                                //Skip tries to jump outside instruction list - throw warning and remove instruction
                                Console.WriteLine("(W) ACTION remove invalid statement in " + m_name + ": " + m_instructions[i].Command + " " + m_instructions[i].Parameters[0]);
                                m_instructions.RemoveAt(i);
                            }
                        }
                        else
                        {
                            //special case: skip is used like goto with label instead of number - Add additional parameter carrying the copied jump label to appease the formatter
                            m_instructions[i].Parameters.Add(m_instructions[i].Parameters[0]);
                        }
                    }

                    //add brackets for "If_..." instructions, since these are transformed to bare "if"
                    //don't do this in case of a third partameter is supplied. In this special case an goto instruction got inserted already - no brackets needed
                    else if ((m_instructions[i].Command.StartsWith("If_") && (m_instructions[i].Parameters.Count < 3)) ||
                        (m_instructions[i].Command.StartsWith("Else") && (i < m_instructions.Count - 1) && (m_instructions[i + 1].Command[0] != '{')))
                    {
                        if (i < m_instructions.Count - 1)
                        {
                            //check for nested "if" blocks and advance instruction counter for closig bracket accordingly
                            int j = CountInstructionGroup(i + 1);
                            m_instructions.Insert(j, new Instruction("}", false));
                            m_instructions.Insert(i + 1, new Instruction("{", false));
                        }
                        else
                        {
                            //in case no instruction follows after if_*, remove instruction
                            Console.WriteLine("(W) ACTION remove incomplete statement in " + m_name + ": " + m_instructions[i].Format(m_name));
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
            }
            else //function is empty - at least yield.
            {
                m_instructions = new List<Instruction>();
                m_instructions.Add(new Instruction("yield break;", false));
            }
            return interruptable;
        }

        //identify nested "IF_" instructions and test for opening brackets
        //brackets are guaranteed to have been added already due to traversing instruction list backwards
        //this will NOT capture nested "IF_" with actual "if" instructions.
        //a wild mix of these instruction types so far has not been observed in the WDL wilderness
        private int CountInstructionGroup(int pos)
        {
            int endpos = pos + 1;
            //nested "if" with brackets found
            if (m_instructions[pos].Command.StartsWith("If_") && (pos < m_instructions.Count - 1) && (m_instructions[pos + 1].Command[0] == '{'))
            {
                endpos = CountInstructionGroup(pos + 2);
            }
            return endpos;
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

        private void PushCondition(string cond)
        {
            s_conditions[s_indents] = cond;
        }

        private string PopCondition()
        {
            if (s_conditions.TryGetValue(s_indents, out string cond))
            {
                s_conditions.Remove(s_indents);
                return cond;
            }
            else
            {
                return string.Empty;
            }
        }

        private int FindIndex(int startIndex, int count)
        {
            int progress = 0;
            int i = startIndex;

            while (progress != count)
            {
                i += Math.Sign(count);

                //something is very wrong here -abort
                if (i >= m_instructions.Count || i < 0)
                    return -1;

                if (m_instructions[i].Count)
                    progress += Math.Sign(count);
            }
            if (count > 0)
            {
                //correct index by one for positive skips only - make sure marker is never placed directly behind "If"
                if (!m_instructions[i].Command.StartsWith("If"))
                    i++;
                else //code tries to jump into if block - C# does not allow this; at least make file compilable by moving marker up
                    Console.WriteLine("(W) ACTION patch invalid jump marker after " + m_name + ": " + m_instructions[i].Command + " " + m_instructions[i].Parameters[0]);

                //make sure marker is not placed before closing bracket, but moved after
                while ((i < m_instructions.Count) && m_instructions[i].Command.StartsWith("}"))
                    i++;
            }

            return i;
        }

    }
}
