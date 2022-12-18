using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
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
            s = s.ToLower();
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return Defines.CheckTransform(new string(a));
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
                s += "__";
            return Defines.CheckTransform(s);
        }

        public static string FormatGlobal(string s)
        {
            return "Globals." + FormatProperty(s);
        }

        public static string FormatFlag(string s)
        {
            return "A3Flags." + FormatProperty(s);
        }

        public static string FormatSkillType(string s)
        {
            return "SkillType." + FormatProperty(s);
        }
    }
}
