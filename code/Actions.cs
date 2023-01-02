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
        private static readonly Random s_random = new Random();

        private static string s_indent = string.Empty;
        private static int s_indents = 2;
        private static readonly string s_nl = Environment.NewLine;

        public static string BuildActions()
        {
            return string.Empty;
        }

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
            //Console.WriteLine(stream);
            s_instructions = Instruction.DeserializeList(stream);
            //Console.WriteLine("public IEnumerator " + name+"()");
            //Console.WriteLine("{");
            s += UpdateIndent("public IEnumerator " + name + "()");
            s += UpdateIndent("{");
            foreach (Instruction inst in s_instructions)
            {
                s += UpdateIndent(inst.Command, inst.Format());
                //UpdateIndent(inst.Command);
                //Console.WriteLine(s_indent + /*"("+inst.Count.ToString()[0]+"): " + */inst.Format());
            }
            s += UpdateIndent("}");
//            Console.WriteLine("}");

            //Cleanup
            s_instructions.Clear();

            return s;
        }

        private static string RandomMarker()
        {
            int size = 10;
            var builder = new StringBuilder(size);

            char offset = 'a';
            const int lettersOffset = 26; //a..z: length = 26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)s_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return builder.ToString();
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

            //Skip instructions need to be replaced by "goto", therefore supply a randomly generated jump marker as additional paraemter
            if (type.StartsWith("Skip"))
                inst.Parameters.Add(RandomMarker());

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
