using System;
using System.Globalization;
using System.Linq;

namespace WDL2CS.Transformers.CS
{
    class CSNodeTransformer : NodeTransformer
    {
        override protected string TransformDefault(string input)
        {
            return input.ToLower();
        }

        override protected string TransformFile(string input)
        {
            input = input.ToLower();
            input = input.Replace('<', '"');
            input = input.Replace('>', '"');
            //Workaround for some very old wdl scripts where quotation marks for file names were omitted
            if (!input.StartsWith("\"")) input = "\"" + input;
            if (!input.EndsWith("\"")) input += "\"";

            return input;
        }

        override protected string TransformList(string input)
        {
            string[] parts = input.Split('.');
            if (parts.Length == 2)
            {
                return "Globals." + FormatReserved(parts[0]) + "[" + (Convert.ToInt32(parts[1]) - 1) + "]";
            }
            else
            {
                return string.Empty;
            }
        }

        override protected string TransformSkill(string input)
        {
            return "Skills." + FormatReserved(input);
        }

        override protected string TransformSkillType(string input)
        {
            return "SkillType." + FormatReserved(input);
        }

        override protected string TransformGlobal(string input)
        {
            return "Globals." + FormatReserved(input);
        }

        override protected string TransformEvent(string input)
        {
            return "Events." + FormatReserved(input);
        }

        override protected string TransformLocalSynonym(string input)
        {
            return FormatReserved(input);
        }

        override protected string TransformGlobalSynonym(string input)
        {
            return "Globals." + FormatReserved(input);
        }

        override protected string TransformMath(string input)
        {
            return "MathV." + FormatReserved(input);
        }

        override protected string TransformNull(string input)
        {
            return "null";
        }

        override protected string TransformNumber(string input)
        {
            if (float.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out float f))
                input = f.ToString(CultureInfo.InvariantCulture);
            else
                input = "0";
            return input;
        }

        override protected string TransformOperator(string input)
        {
            //WDL and CS have same operators, just copy over
            return input;
        }

        override protected string TransformString(string input)
        {
            //convert escaping of quotation marks
            input = input.Replace("\\\"", "\"\"");
            return "@" + input;
        }

        protected override string TransformSimpleString(string input)
        {
            //always lower case simple strings
            input = input.ToLower();
            if (!input.StartsWith("\"")) input = "\"" + input;
            if (!input.EndsWith("\"")) input += "\"";

            return input;
        }

        override protected string TransformProperty(string input)
        {
            return "." + FormatReserved(input);
        }

        override protected string TransformFlag(string input)
        {
            return "A3Flags." + FormatReserved(input);
        }

        override protected string TransformIdentifier(string input)
        {
            return FormatIdentifier(input);
        }

        override protected string TransformReserved(string input)
        {
            return FormatReserved(input);
        }

        override protected string TransformActorTarget(string input)
        {
                return "ActorTarget." + FormatReserved(input);
        }

        override protected string TransformGotoLabel(string input)
        {
            return FormatIdentifier(input) + ":";
        }


        //TODO: these are all temp
        private string FormatReserved(string input)
        {
            input = input.ToLower();
            char[] a = input.ToCharArray();
            a[0] = char.ToUpper(a[0]);

            return new string(a);
        }

        private string FormatIdentifier(string input)
        {
            //remove unknown and non-allowed characters
            input = input.Replace("-", "");
            input = input.Replace("?", "");
            input = input.Replace(".", "");
            input = input.ToLower();
            //patch all identifiers conflicting with C# language (except for null)
            if (!input.IsValidIdentifier() /*&& !input.Equals("null")*/)
            {
                Console.WriteLine("(I) CSNODETRANSFORMER patched identifier " + input);
                input = "__" + input;
            }
            return input;
        }


    }
}
