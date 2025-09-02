using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Action : Node, ISection
    {
        private static ActionTransformer s_transformer = null;

        private Node m_name;
        private List<Instruction> m_instructions;

        public static new ActionTransformer Transformer { get => s_transformer; set => s_transformer = value; }
        public string Name { get => m_name.Data; }
        public string Type { get => "ACTION"; }

        public Action(Node name, Node instructions)
        {
            m_name = name;
            if (instructions != null)
            {
                List<Node> nodes = instructions.GetAll();
                m_instructions = nodes.Where(x => x is Instruction).Select(x => x as Instruction).ToList();
            }
            ProcessInstructions();
        }

        public Action() : this(new Node(), null) { }

        public bool IsInitialized()
        {
            return false;
        }

        private void ProcessInstructions()
        {
            //any instructions in action
            if (m_instructions != null)
            {
                Dictionary<int, Instruction> inserts = new Dictionary<int, Instruction>();

                //iterate through instruction list from end, so that new instructions can be inserted directly
                for (int i = m_instructions.Count - 1; i >= 0; i--)
                {
                    //C# does not allow jumpin at end of { } block - move label after block
                    //this check must take place before the later checks as these might get triggered by error due to goto marker naming
                    if (m_instructions[i].CommandType == NodeType.GotoLabel)
                    {
                        int m = i;
                        while ((m < m_instructions.Count - 1) && (m_instructions[m + 1].Command[0] == '}'))
                        {
                            m++;
                        }
                        if (m > i)
                        {
                            Console.WriteLine("(I) ACTION jump marker moved after closing bracket");
                            m_instructions.Insert(m + 1, m_instructions[i]);
                            m_instructions.RemoveAt(i);
                        }
                    }

                    //Transform "Skip" to "goto" and jump marker
                    else if (m_instructions[i].Command.StartsWith("Skip", StringComparison.OrdinalIgnoreCase))
                    {
                        //standard case: Skip comes with number of lines to skip as parameter
                        if (Int32.TryParse(m_instructions[i].Parameters[0].Data, out int num)) //TEMP
                        {
                            //Console.WriteLine(name);
                            int index = FindIndex(i, num);
                            if (index > -1)
                            {
                                //Add additional parameter carrying the jump label
                                m_instructions[i].Parameters.Add(new Node("skip" + i + "to" + index, NodeType.Identifier));
                                //create new instruction for jump marker which is to be inserted
                                Instruction marker = new Instruction(new Node(m_instructions[i].Parameters[1].Data, NodeType.GotoLabel), false);
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
                    else if (
                        (
                            m_instructions[i].Command.StartsWith("If_", StringComparison.OrdinalIgnoreCase) &&
                            //don't do this in case of a third parameter is supplied. In this special case an goto instruction got inserted already - no brackets needed
                            (m_instructions[i].Parameters.Count < 3) && (m_instructions[i].Parameters.Count > 0)
                        ) ||
                        (
                            m_instructions[i].Command.StartsWith("Else", StringComparison.OrdinalIgnoreCase) && 
                            (i < m_instructions.Count - 1) && (m_instructions[i + 1].Command[0] != '{')
                        )
                    )
                    {
                        if (i < m_instructions.Count - 1)
                        {
                            //check for nested "if" blocks and advance instruction counter for closing bracket accordingly
                            int j = CountInstructionGroup(i + 1);
                            m_instructions.Insert(j, new Instruction(new Node("}"), false));
                            m_instructions.Insert(i + 1, new Instruction(new Node("{"), false));
                        }
                        else
                        {
                            //in case no instruction follows after if_*, remove instruction
                            Console.WriteLine("(W) ACTION remove incomplete statement in " + m_name + ": " + m_instructions[i].Format(m_name.Data.ToLower()));//TEMP
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
                Instruction last = m_instructions.Last();
                if (!(last.CommandType == NodeType.Reserved &&
                    (
                        last.Command.Equals("Branch", StringComparison.OrdinalIgnoreCase) ||
                        last.Command.Equals("End", StringComparison.OrdinalIgnoreCase) ||
                        last.Command.Equals("Goto", StringComparison.OrdinalIgnoreCase) ||
                        last.Command.Equals("Exit", StringComparison.OrdinalIgnoreCase)
                    )
                ))
                    m_instructions.Add(new Instruction(new Node("END", NodeType.Reserved), false));
            }
            else //function is empty - at least yield.
            {
                m_instructions = new List<Instruction>
                {
                    new Instruction(new Node("END", NodeType.Reserved), false)
                };
            }
        }

        //identify nested "IF_" instructions and test for opening brackets
        //brackets are guaranteed to have been added already due to traversing instruction list backwards
        //this will NOT capture nested "IF_" with actual "if" instructions.
        //a wild mix of these instruction types so far has not been observed in the WDL wilderness
        private int CountInstructionGroup(int pos)
        {
            int endpos = pos + 1;
            //nested "if" with brackets found
            if (m_instructions[pos].Command.StartsWith("If_", StringComparison.OrdinalIgnoreCase) && (pos < m_instructions.Count - 1) && (m_instructions[pos + 1].Command[0] == '{'))
            {
                endpos = CountInstructionGroup(pos + 2);
            }
            return endpos;
        }

        public void Transform(object obj)
        {
            s_transformer?.Transform(obj, m_name, m_instructions);
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
                if (!m_instructions[i].Command.StartsWith("If", StringComparison.OrdinalIgnoreCase))
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
