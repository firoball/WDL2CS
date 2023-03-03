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
            Sections.Deserialize(stream);
            string s = string.Empty;

            s += Defines.Format();
            s += @"
using System.Collections;
using Acknex3.Api;

namespace Acknex3.Script
{
	class Script
	{
		public void Initialize()
		{
";
            s += Sections.FormatInit();
            s += @"
		}
";
            s += Sections.Format();
            s += @"
	}
}
";

            return s;
        }
    }
}
