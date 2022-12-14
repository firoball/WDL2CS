using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Formatter
    {
        public static string FormatString(string s)
        {
            //convert escaping of quotation marks
            s = s.Replace("\\\"", "\"\"");
            return "@" + s;
        }

        public static string FormatFile(string s)
        {
            s = s.ToLower();
            s = s.Replace('<', '"');
            s = s.Replace('>', '"');
            return s;
        }

        public static string FormatNull()
        {
            return "null";
        }

        
        public static string FormatFunction(string s)
        {
            s = Ucfirst(s);
            return Defines.CheckTransform(s);
        }
        
        public static string FormatObject(string s)
        {
            s = Ucfirst(s);
            return Defines.CheckTransform(s);
        }

        public static string FormatEvent(string s)
        {
            if (!string.IsNullOrEmpty(s))
                s = FormatProperty(s);
            /*
            s = s.ToLower();
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return Defines.CheckTransform(new string(a));*/
            return "Events." + s;
        }

        public static string FormatProperty(string s)
        {
            s = Ucfirst(s);
            return Defines.CheckTransform(s);
        }

        public static string FormatCommand(string s)
        {
            s = Ucfirst(s);
            return Defines.CheckTransform(s);//transform required here?
        }

        public static string FormatNumber(string s)
        {
            if (float.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out float f))
                s = f.ToString(CultureInfo.InvariantCulture);
            else
                s = "0";
            return s;
        }

        public static string FormatIdentifier(string s)
        {
            s = s.ToLower();
            //patch all identifiers conflicting with C# language
            CodeDomProvider provider = CodeDomProvider.CreateProvider("C#");
            if (!provider.IsValidIdentifier(s))
                s = "__" + s;
            return Defines.CheckTransform(s);
        }

        public static string FormatGlobal(string s)
        {
            return "Globals." + FormatProperty(s);
            //return Defines.CheckTransform(a);

        }

        public static string FormatSkill(string s)
        {
            return "Skills." + FormatProperty(s);
            //return Defines.CheckTransform(a);

        }

        public static string FormatFlag(string s)
        {
            //somewhat dirty workaround due to"base" identifier being patched to avoid clash with C' language
            //remove leading underscores and capitalize first char
            while (s[0] == '_')
                s = s.Substring(1);

            return "A3Flags." + FormatProperty(s);
        }

        //Targets are not detected specifically in the parser due to complexity limit
        //Therefore, hard code the different targets here
        private static string[] s_targets = new[] { "move", "bullet", "stick", "follow", "repel", "vertex", "node1", "node2", "hold" };
        public static string FormatActorTarget(string s)
        {
            if (s_targets.Contains(s))
                return "ActorTarget." + FormatProperty(s);
            else return s;
        }

        public static string FormatSkillType(string s)
        {
            return "SkillType." + FormatProperty(s);
        }

        public static string FormatMath(string s)
        {
            return "MathV." + FormatProperty(s);
        }

        public static string FormatMarker(string s)
        {
            return s.ToLower() + ":";
        }

        public static string FormatTargetSkill(string target)
        {
            //C# does not allow overloading of = operator, therefore auto-assignment to Skill.Val is not possible
            //Work around by generating explicit assignment after identifying target as Skill
            if (
                (
                    (target.StartsWith("Skills.") || target.Contains(".Skill")) &&
                    !(target.EndsWith(".Min") || target.EndsWith(".Max"))
                ) ||
                    Objects.Is("Skill", target)
                )
                target += ".Val";

            return target;
        }

        public static string FormatExpression(string target, string op, string expression)
        {
            if (string.Compare(op, " = ") == 0)
                target = FormatTargetSkill(target);
            return target + op + expression;
        }

        public static string FormatList(string list)
        {
            string[] parts = list.Split('.');
            if (parts.Length == 2)
            {
                return "Globals." + FormatProperty(parts[0]) + "[" + (Convert.ToInt32(parts[1]) - 1) + "]";
            }
            else
            {
                return string.Empty;
            }
        }

        public static string FormatObjectId(string obj)
        {
            //parser grammar needs to avoid shift/reduce conflicts
            //due to this some specific patches for ambiguous keywords are required to be applied
            if (string.Compare(obj, "random", true) == 0)
                return FormatSkill(obj);
            else
                return obj;
        }

        private static string Ucfirst(string s)
        {
            s = s.ToLower();
            char[] a = s.ToCharArray();
            int i = 0;
            //skip leading underscores
            while (i < a.Length && (a[i] == '_' || (a[i] >= 48 && a[i] <= 57)))
                i++;
            a[i] = char.ToUpper(a[i]);
            return new string(a);
        }
    }
}
