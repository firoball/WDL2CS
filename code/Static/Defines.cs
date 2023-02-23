using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Defines
    {
        private static readonly string s_indent = "\t\t";
        private static readonly string s_nl = Environment.NewLine;

        public static List<string> s_defines = new List<string>();

        private static Dictionary<string, string> s_redefines = new Dictionary<string, string>();
        public static List<string> s_consts = new List<string>();
        private static bool s_transform = false;
        private static string s_const = string.Empty;
        private static string s_original = string.Empty;

        public static string AddTransform(string redefine)
        {
            string s = string.Empty;

            //patch away global skills - formatting gets inherited due to grammar of parsing process
            int i = redefine.LastIndexOf('.');
            redefine = i < 0 ? redefine : redefine.Substring(i+1);

            if (s_transform)
            {
                redefine = Formatter.FormatProperty(redefine); //not clean: might also be an Identifier?
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
                //format constants as identifiers instead of preprocessor definition
                redefine = Formatter.FormatIdentifier(redefine);
                if (!s_consts.Contains(redefine))
                {
                    s_consts.Add(redefine);
                    Console.WriteLine("(I) DEFINES add const: " + redefine + ", " + s_original);
                    s = new Define(s_const, redefine, s_original).Serialize();

                    //TODO: move up to Section level - until Section code is updated for serialization, just deserialize and format
                    Define d = Define.Deserialize(s);
                    s = d.Format();
                }
                else
                {
                    Console.WriteLine("(W) DEFINES ignore double definition: " + redefine);
                }
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
            if (Objects.Identify(out string obj, s))
            {
                //identified as some specific object, declare data type accordingly
                s_const = obj;
                s_original = s;
            }
            else
            {
                //must be some renamed property
                s_transform = true;
                s_original = Formatter.FormatProperty(s);
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

        public static string CheckConst(string identifier)
        {
            //patch away global skills prefix
            int i = identifier.LastIndexOf('.');
            string constId = i < 0 ? identifier : identifier.Substring(i + 1);
            //format constants as identifiers instead of preprocessor definition
            constId = Formatter.FormatIdentifier(constId);

            if (s_consts.Contains(constId))
            {
                return constId;
            }
            else
            {
                return identifier;
            }

        }
    }
}
