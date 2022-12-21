using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Actions
    {
        private static List<string> s_instructions  = new List<string>();
        private static Dictionary<string, string> s_actions = new Dictionary<string, string>();
        private static List<string> s_parameters = new List<string>();

        private static readonly string s_indent = "\t\t";
        private static readonly string s_nl = Environment.NewLine;

        public static string BuildActions()
        {
            return string.Empty;
        }

        public static void AddAction(string name)
        {
            Console.WriteLine("ACTION " + name);
            Console.WriteLine("{");
            foreach (string inst in s_instructions)
                Console.WriteLine(inst);
            Console.WriteLine("}");

            //Cleanup
            s_instructions.Clear();
        }

        public static void AddExpression(string expr)
        {
            //bypass instruction logic for RULEs
//            s_instructions.Insert(0, expr);
            s_instructions.Add(expr);
        }

        public static void AddInstruction(string type)
        {
            string inst = string.Empty;

            switch (type)
            {
                default:
                    inst = "instr: " + type + " params:";
                    foreach (string param in s_parameters)
                        inst += " " + param;
                    //Console.WriteLine(inst);
                    break;

            }
            s_instructions.Add(inst);

            //Clean up
            s_parameters.Clear();
        }

        public static void AddInstructionParameter(string param)
        {
            s_parameters.Insert(0, param);
        }

        private void BuildInstruction(string command, List<string> instructions)
        {

        }
    }
}
