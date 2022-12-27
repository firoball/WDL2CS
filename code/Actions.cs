using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Actions
    {
        private static List<Instruction> s_instructions  = new List<Instruction>();

        //private static Dictionary<string, string> s_actions = new Dictionary<string, string>();
        private static List<string> s_parameters = new List<string>();

        private static string s_indent = string.Empty;
        private static int s_indents = 2;
        private static readonly string s_nl = Environment.NewLine;

        public static string BuildActions()
        {
            return string.Empty;
        }

        private static void CheckIndent(string s)
        {
            if (s[0] == '{')
            {
                UpdateIndent();
                s_indents++;
            }
            else if (s[0] == '}')
            {
                s_indents--;
                UpdateIndent();
            }
            else
            {
                UpdateIndent();
            }
        }

        private static void UpdateIndent()
        {
            s_indent = new string('\t', s_indents);
        }

        public static void AddAction(string name, string stream)
        {
            //Console.WriteLine(stream);
            s_instructions = Instruction.DeserializeList(stream);
            Console.WriteLine("public IEnumerator " + name+"()");
            Console.WriteLine("{");
            foreach (Instruction inst in s_instructions)
            {
                CheckIndent(inst.Command);
                Console.WriteLine(s_indent + /*"("+inst.Count.ToString()[0]+"): " + */inst.Format());
            }
            Console.WriteLine("}");

            //Cleanup
            s_instructions.Clear();
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

        public static string CreatePreProcElseCondition(string stream)
        {
            string s = string.Empty;

            s += new Instruction("#else", false).Serialize();
            s += stream;
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

            s += new Instruction("if", expr).Serialize();
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

            s += new Instruction("while", expr).Serialize();
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
