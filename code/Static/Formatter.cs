﻿using System;
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
            //somewhat dirty workaround due to Keywords which aer identical to Object Defs.
            //These have been prefixed with leading underscores in a previous formatting step.
            //remove leading underscores again so proper identifier can be derived
            while (s[0] == '_')
                s = s.Substring(1);

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
        }

        public static string FormatSkill(string s)
        {
            return "Skills." + FormatProperty(s);
        }

        public static string FormatPropertyValue(string s)
        {
            //prioritize usage of const definitions (defines in WDL) over global skills/events in properties
            //this is more or less a shameless hack, but consts cannot be treated at earlier stage
            //without breaking redeclaration of built-in Acknex engine skills and events.
            return Defines.CheckConst(s);
        }

        public static string FormatFlag(string s)
        {
            //somewhat dirty workaround due to"base" identifier being patched to avoid clash with C# language
            //remove leading underscores and capitalize first char
            while (s[0] == '_')
                s = s.Substring(1);

            return "A3Flags." + FormatProperty(s);
        }

        public static string FormatVideo(string s)
        {
            //somewhat dirty workaround due to identifier starting with number being patched to avoid clash with C' language
            //remove leading underscores and capitalize first char
            while (s[0] == '_')
                s = s.Substring(1);

            s = "\"" + s + "\"";
            return s;
        }

        //Targets are not detected specifically in the parser due to complexity limit
        //Therefore, hard code the different targets here
        private static string[] s_targets = new[] { "move", "bullet", "drop", "stick", "follow", "repel", "vertex", "node0", "node1", "hold", "null" };
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
            if (obj.Equals("random"))
                return FormatSkill(obj);
            else
                return obj;
        }

        public static string FormatAssetId(string asset)
        {
            //WDL accepts numbers as asset IDs -> prefix these for valid C# identifiers
            if (int.TryParse(asset, out int n))
                return "__" + asset;
            else
                return asset;
        }

        public static string FormatPreprocessor(string preproc)
        {
            return preproc.ToUpperInvariant();
        }

        public static string FormatClass(string s)
        {
            s = Ucfirst(s);
            return s;
        }

        public static string FormatGotoLabel(string marker)
        {
            //Workaround
            //make sure any falsly added prefix is discarded (parser cannot distinguish properly for goto)
            int g = marker.LastIndexOf('.');
            string label = g < 0 ? marker : marker.Substring(g + 1);
            return label.ToLowerInvariant();
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
