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
        private static bool s_transform = false;
        private static string s_const = string.Empty;
        private static string s_original = string.Empty;

        public static string AddTransform(string redefine)
        {
            string s = string.Empty;

            if (s_transform)
            {
                if (!s_redefines.ContainsKey(redefine))
                {
                    s_redefines.Add(redefine, s_original);
                    Console.WriteLine("(I) DEFINES add transformation: " + redefine + ", " + s_original);
                }
                else
                {
                    Console.WriteLine("(W) DEFINES ignore double definition: " + redefine + ", " + s_original);
                }
            }
            else
            {
                Console.WriteLine("(I) DEFINES add const: " + redefine + ", " + s_original);
                s = $"{s_indent}public static const {s_const} {redefine} = {s_original};{s_nl}";
            }

            //Reset settings
            s_const = string.Empty;
            s_transform = false;

            return s;
        }

        public static void AddStringDefine(string s)
        {
            s_const = "string";
            s_original = s;
        }

        public static void AddFileDefine(string s)
        {
            s_const = "string";
            s_original = s;
        }

        public static void AddNumberDefine(string s)
        {
            s_const = "Var";
            s_original = s;
        }

        public static void AddKeywordDefine(string s)
        {
            if (Objects.Is(out string obj, s))
            {
                s_const = obj;
            }
            else
            {
                s_transform = true;
            }
            s_original = s;
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
