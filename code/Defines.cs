using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Defines
    {
        private static readonly string s_indent = "";
        private static readonly string s_nl = Environment.NewLine;

        public static List<string> s_defines = new List<string>();

        private static Dictionary<string, string> s_redefines = new Dictionary<string, string>();
        //private static Stack<string> s_ifdefs = new Stack<string>();

        public static void AddTransform(string redefine, string original)
        {
            if (!s_redefines.ContainsKey(redefine))
            {
                s_redefines.Add(redefine, original);
                Console.WriteLine("(I) DEFINES add transformation: " + redefine + ", " + original);
            }
            else
            {
                Console.WriteLine("(W) DEFINES ignore double definition: " + redefine + ", " + original);
            }
        }

        public static string AddDefine(string define)
        {
            return s_indent + "#define " + define + s_nl;
            /*
            if (!s_defines.Contains(define))
                s_defines.Add(define);
            else
                Console.WriteLine("DEFINES ignore double definition: " + define);
                */
        }

        public static string RemoveDefine(string define)
        {
            if (s_redefines.ContainsKey(define))
                s_redefines.Remove(define);

            return s_indent + "#undef " + define + s_nl;

            /*
            if (s_defines.Contains(define))
                s_defines.Remove(define);
            */
        }

        public static string CheckTransform(string identifier)
        {
            if (s_redefines.TryGetValue(identifier, out string original))
                return original;
            else
                return identifier;
        }

    }
}
