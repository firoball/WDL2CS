using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Script
    {
        public static string Format(string stream)
        {
            string s = string.Empty;

            s += Defines.FormatDefines();
            s += @"
using System.Collections;
using Acknex3.Api;

namespace Acknex3.Script
{
	class Script
	{
";
            s += stream;
            s += @"
	}
}
";

            return s;
        }
    }
}
