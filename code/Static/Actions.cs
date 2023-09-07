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
            Registry.Register("Action", name);
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
            //ridiculous patch: A3 accepts RULE statements without assignment
            //TODO: find out real behaviour in A3, currently first identifier is treated as assignee
            //patch is derived from the behaviour of A3 for statements like "+ =" - seems like "=" is optional for WDL parser
            if (!expr.Contains("="))
            {
                Console.WriteLine("(W) ACTIONS patched invalid rule: " + expr);
                string[] fragments = expr.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                fragments[1] += "="; //first operator is changed to assignment operator
                expr = string.Join(" ", fragments);
            }
            Instruction inst = new Instruction("Rule", expr);

            return inst.Serialize();
        }

        public static string CreateExpression(string assignee, string op, string expr)
        {
            expr = assignee + op + expr; 
            Instruction inst = new Instruction("Rule", expr);
            return inst.Serialize();
        }

        public static string CreateInstruction(string command)
        {
            if (Identifier.IsCommand(ref command))
            {
                Instruction inst = new Instruction(command, s_parameters);

                //Clean up
                s_parameters.Clear();
                return inst.Serialize();
            }
            else if (s_parameters.Count == 0) //take care of wrongly defined goto marker (ends with ; instead of :)
            {
                Console.WriteLine("(W) ACTIONS crrect malformed goto marker: " + command);
                Instruction inst = new Instruction(Formatter.FormatGotoMarker(command), false);
                return inst.Serialize();
            }
            else
            {
                //Clean up
                s_parameters.Clear();

                Console.WriteLine("(W) ACTIONS ignore invalid command: " + command);
                return string.Empty;
            }
        }

        public static void AddInstructionParameter(string param)
        {
            s_parameters.Insert(0, param);
        }

    }
}
