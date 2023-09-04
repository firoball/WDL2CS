using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Actions
    {
        private static List<string> s_parameters = new List<string>();

        public static string AddAction(string name, string stream)
        {
            name = Formatter.FormatActionId(name);
            Identifiers.Register("Action", name);
            string a = new Action(name, stream).Serialize();

            return a;
        }


        public static string CreateMarker(string name, string stream)
        {
            string s = string.Empty;

            s += new Instruction(Formatter.FormatGotoMarker(name), false).Serialize();
            s += stream;

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

        public static string CreateInvalidInstruction(string type)
        {
            //Clean up
            s_parameters.Clear();

            Console.WriteLine("(W) ACTIONS ignore invalid command: " + type);
            return string.Empty;
        }
    }
}
