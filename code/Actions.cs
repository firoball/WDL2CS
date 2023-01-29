using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Actions
    {
        private static List<Instruction> s_instructions  = new List<Instruction>();
        private static Dictionary<int, Instruction> s_inserts = new Dictionary<int, Instruction>();

        private static List<string> s_parameters = new List<string>();
        private static readonly Random s_random = new Random();

        private static string s_indent = string.Empty;
        private static int s_indents = 2;
        private static readonly string s_nl = Environment.NewLine;

        private static string UpdateIndent(string s)
        {
            return UpdateIndent(s, s);
        }

        private static string UpdateIndent(string s, string t)
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

        private static void BuildIndent()
        {
            s_indent = new string('\t', s_indents);
        }

        public static string AddAction(string name, string stream)
        {
            string s = string.Empty;
            bool interruptable = false;
            string instName = Formatter.FormatIdentifier(name);
            s_instructions = Instruction.DeserializeList(stream);

            //iterate through instruction list from end, so that new instructions can be inserted directly
            for (int i = s_instructions.Count - 1; i >= 0; i--)
            {
                if (s_instructions[i].Command.StartsWith("Wait") || s_instructions[i].Command.StartsWith("Inkey"))
                {
                    interruptable = true;
                }

                //Transform "Skip" to "goto" and jump marker
                if (s_instructions[i].Command.StartsWith("Skip"))
                {
                    //Console.WriteLine(name);
                    int index = FindIndex(i, Convert.ToInt32(s_instructions[i].Parameters[0]));
                    //Add additional parameter carrying the jump label
                    s_instructions[i].Parameters.Add("skip" + i +"to" + index);
                    //create new instruction for jump marker which is to be inserted
                    Instruction marker = new Instruction(Formatter.FormatMarker(s_instructions[i].Parameters[1]), false);
                    //instruction needs to be inserted above current one. Buffer and insert later
                    //otherwise iteration can break on direct insertion
                    if (index < i)
                    {
                        s_inserts.Add(index, marker);
                    }
                    else
                    {
                        s_instructions.Insert(index, marker);
                    }
                }

                //add brackets for "If_..." instructions, since these are transformed to bare "if"
                if (s_instructions[i].Command.StartsWith("If_") || 
                    (s_instructions[i].Command.StartsWith("Else") && (i < s_instructions.Count - 1) && (s_instructions[i + 1].Command[0] != '{')))
                {
                    s_instructions.Insert(i + 2, new Instruction("}", false));
                    s_instructions.Insert(i + 1, new Instruction("{", false));
                }

                //check if any buffered instructions need to be inserted at current position
                if (s_inserts.TryGetValue(i, out Instruction insert))
                {
                    s_instructions.Insert(i, insert);
                }
            }
            //always yield at end of function!
            if (!s_instructions.Last().Command.Equals("Branch") && 
                !s_instructions.Last().Command.Equals("End") &&
                !s_instructions.Last().Command.Equals("Goto") &&
                !s_instructions.Last().Command.Equals("Exit")
                )
                s_instructions.Add(new Instruction("yield break;", false));

            s += UpdateIndent("private class " + name + " : Function<" + name + ">");
            s += UpdateIndent("{");
            s += UpdateIndent("public override IEnumerator Logic()");
            s += UpdateIndent("{");

            Instruction lastif = null;
            Instruction last = null;

            foreach (Instruction inst in s_instructions)
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
                    s += UpdateIndent(inst.Command, inst.Format(instName));
                }

                last = inst;
            }
            s += UpdateIndent("}");
            s += UpdateIndent("}");
            string c = "public static Function " + instName + " = new " + name + "()";
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

            //Cleanup
            s_instructions.Clear();
            s_inserts.Clear();

            return s;
        }

        private static int FindIndex(int startIndex, int count)
        {
            int progress = 0;
            int i = startIndex;

            while(progress != count)
            {
                i += Math.Sign(count);
                if (s_instructions[i].Count)
                    progress += Math.Sign(count);
            }
            if (count > 0)
            {
                //correct index by one for positive skips only
                i++;
                //make sure marker is not placed before closing bracket, but moved after
                while ((i < s_instructions.Count) && s_instructions[i].Command.StartsWith("}"))
                    i++;
            }

            return i;
        }

        public static string CreateMarker(string name, string stream)
        {
            string s = string.Empty;

            s += new Instruction(Formatter.FormatMarker(name), false).Serialize();
            s += stream;

            return s;

        }

        public static string CreatePreProcIfNotCondition(string expr, string stream)
        {
            string s = string.Empty;

            s += new Instruction("#if", "!(" + expr + ")", false).Serialize();
            s += stream;

            return s;
        }


        public static string CreatePreProcIfCondition(string expr, string stream)
        {
            string s = string.Empty;

            s += new Instruction("#if", expr, false).Serialize();
            s += stream;

            return s;
        }

        public static string CreatePreProcElseCondition(string ifstream, string elsestream)
        {
            string s = string.Empty;

            s += ifstream;
            s += new Instruction("#else", false).Serialize();
            s += elsestream;
            s += new Instruction("#endif", false).Serialize();

            return s;
        }

        public static string CreatePreProcEndCondition(string stream)
        {
            string s = string.Empty;

            s += stream;
            s += new Instruction("#endif", false).Serialize();

            return s;
        }

        public static string CreateIfCondition(string expr, string stream)
        {
            string s = string.Empty;

            s += new Instruction("if", "(" + expr + ")").Serialize();
            s += new Instruction("{", false).Serialize();
            s += stream;
            s += new Instruction("}", false).Serialize();

            return s;
        }

        public static string CreateElseCondition(string stream)
        {
            string s = string.Empty;

            s += new Instruction("else").Serialize();
            s += new Instruction("{", false).Serialize();
            s += stream;
            s += new Instruction("}", false).Serialize();

            return s;
        }

        public static string CreateWhileCondition(string expr, string stream)
        {
            string s = string.Empty;

            s += new Instruction("while", "(" + expr + ")").Serialize();
            s += new Instruction("{", false).Serialize();
            s += stream;
            s += new Instruction("}", false).Serialize();

            return s;
        }

        public static string CreateExpression(string expr)
        {
            Instruction inst = new Instruction("Rule", expr);
            return inst.Serialize();
        }
        
        public static string CreateInstruction(string type)
        {
            Instruction inst = new Instruction(type, s_parameters);

            //Clean up
            s_parameters.Clear();
            return inst.Serialize();
        }

        public static void AddInstructionParameter(string param)
        {
            s_parameters.Insert(0, param);
        }

    }
}
