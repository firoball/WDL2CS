using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Defines
    {
        private static readonly string s_nl = Environment.NewLine;

        private static List<string> s_defines = new List<string>();

        private static Dictionary<string, string> s_redefines = new Dictionary<string, string>();
        public static Dictionary<string, string> s_consts = new Dictionary<string, string>();
        private static bool s_transformProperty = false;
        private static bool s_transformIdentifier = false;
        private static string s_const = string.Empty;
        private static string s_original = string.Empty;

        public static string Format()
        {
            string o = string.Empty;

            foreach (string s in s_defines)
            {
                o += "#define " + s + s_nl;
            }

            return o;
        }

        public static string AddTransform(string redefine)
        {
            string s = string.Empty;

            //patch away global skills - formatting gets inherited due to grammar of parser
            int i = redefine.LastIndexOf('.');
            redefine = i < 0 ? redefine : redefine.Substring(i + 1);

            if (s_transformProperty || s_transformIdentifier)
            {
                if (s_transformProperty)
                    redefine = Formatter.FormatProperty(redefine);
                if (s_transformIdentifier)
                    redefine = Formatter.FormatIdentifier(redefine);
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
                if (!s_consts.ContainsKey(redefine))
                {
                    s_consts.Add(redefine, s_original);
                }
                else
                {
                    //Defines can be defined in different preprocessor sections - do not issue warning 
                    //Console.WriteLine("(W) DEFINES ignore double definition: " + redefine);
                }
                Console.WriteLine("(I) DEFINES add const: " + redefine + ", " + s_original);
                s = new Define(s_const, redefine, s_original).Serialize();
            }

            //Reset settings
            s_const = string.Empty;
            s_transformProperty = false;
            s_transformIdentifier = false;

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

        public static void AddListDefine(string s)
        {
            //lists must be assigned directly, references (Synonyms) won't work here
            s_transformIdentifier = true;
            s_original = s;
        }

        public static void AddKeywordDefine(string s)
        {
            s = Formatter.FormatDefine(s); //due to limitations of parser, any keyword define needs to be reformatted explicitly
            if (Objects.Identify(out string obj, s))
            {
                //identified as some specific object, declare data type accordingly
                if (obj.Equals("String"))
                    s_const = "string"; //special case - C# string type is used instead of custom object
                else
                    s_const = obj;
                s_original = s;
            }
            else if (Assets.Identify(out string asset, s))
            {
                //identified as some specific asset, declare data type accordingly
                s_const = asset;
                s_original = s;
            }
            else
            {
                //must be some renamed property
                s_transformProperty = true;
                s_original = Formatter.FormatProperty(s);
            }
        }

        public static string AddDefine(string define)
        {
            //C# does not allow conditional #define, therefore just add all identified #defines at beginning of generated code file            
            if (!s_defines.Contains(define))
                s_defines.Add(define);
            else
                Console.WriteLine("DEFINES ignore double definition: " + define);

            return string.Empty; //defines without parameters are treated independently from data stream
        }

        public static string RemoveDefine(string define)
        {
            //#undef directives are not supported by C# in the same way as in WDL, transpilation may fail here
            if (s_redefines.ContainsKey(define))
                s_redefines.Remove(define);

            if (s_defines.Contains(define))
                s_defines.Remove(define);

            return string.Empty; //defines without parameters are treated independently from data stream
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

            if (s_consts.ContainsKey(constId))
            {
                return constId;
            }
            else
            {
                return identifier;
            }

        }

        public static string GetConstReference(string constId)
        {
            if (s_consts.TryGetValue(constId, out string reference))
            {
                return reference;
            }
            else
            {
                return constId;
            }

        }
    }
}
