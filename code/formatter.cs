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
            s = s.ToLower();
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return Defines.CheckTransform(new string(a));
        }

        public static string FormatObject(string s)
        {
            s = s.ToLower();
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return Defines.CheckTransform(new string(a));
        }

        public static string FormatEvent(string s)
        {
            s = s.ToLower();
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return Defines.CheckTransform(new string(a));
        }

        public static string FormatProperty(string s)
        {
            //if (string.IsNullOrEmpty(s))
                //return "ThisIsADebugText"; //string.Empty;
            s = s.ToLower();
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return Defines.CheckTransform(new string(a));
        }

        public static string FormatCommand(string s)
        {
            s = s.ToLower();
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return Defines.CheckTransform(new string(a)); //transform required here?
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
            string a =  "Globals." + FormatProperty(s);
            return Defines.CheckTransform(a);

        }

        public static string FormatFlag(string s)
        {
            return "A3Flags." + FormatProperty(s);
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
            if ((string.Compare("Globals.", 0, target, 0, 8, true) == 0) || Objects.Is("Skill", target))
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
                return FormatProperty(parts[0]) + "[" + (Convert.ToInt32(parts[1]) - 1) + "]";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
