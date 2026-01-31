using System;
using System.Globalization;
using System.Linq;

namespace WDL2CS.Transformers.WDL
{
    class WDLNodeTransformer : NodeTransformer
    {
        override protected string TransformDefault(string input)
        {
            return input.ToLower();
        }

        override protected string TransformFile(string input)
        {
            input = input.ToLower();
            //Workaround for some very old wdl scripts where quotation marks for file names were omitted
            if (!input.StartsWith("<")) input = "<" + input;
            if (!input.EndsWith(">")) input += ">";

            return input;
        }

        override protected string TransformList(string input)
        {
            string[] parts = input.Split('.');
            if (parts.Length == 2)
            {
                return FormatReserved(parts[0]) + "." + Convert.ToInt32(parts[1]);
            }
            else
            {
                return string.Empty;
            }
        }

        override protected string TransformSkill(string input)
        {
            return FormatReserved(input);
        }

        override protected string TransformSkillType(string input)
        {
            return FormatReserved(input);
        }

        override protected string TransformGlobal(string input)
        {
            return FormatReserved(input);
        }

        override protected string TransformEvent(string input)
        {
            return FormatReserved(input);
        }

        override protected string TransformLocalSynonym(string input)
        {
            return FormatReserved(input);
        }

        override protected string TransformGlobalSynonym(string input)
        {
            return FormatReserved(input);
        }

        override protected string TransformMath(string input)
        {
            return FormatReserved(input);
        }

        override protected string TransformNull(string input)
        {
            return FormatReserved("null");
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
            //just copy over
            return input;
        }

        override protected string TransformString(string input)
        {
            //just copy over
            return input;
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
            if (input.Equals("val", StringComparison.OrdinalIgnoreCase))
                return "";
            else
                return "." + FormatReserved(input);
        }

        override protected string TransformFlag(string input)
        {
            return FormatReserved(input);
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
                return FormatReserved(input);
        }

        override protected string TransformGotoLabel(string input)
        {
            return FormatIdentifier(input) + ":";
        }


        private string FormatReserved(string input)
        {
            return input.ToUpper();
        }

        private string FormatIdentifier(string input)
        {
            //remove unknown and non-allowed characters
            input = input.Replace("-", "");
            input = input.Replace("?", "");
            input = input.Replace(".", "");
            input = input.ToLower();

            return input;
        }


    }
}
