using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Script
    {
        public static string Format(string className, string stream)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Sections.Deserialize(ref stream);

            StringBuilder sb = new StringBuilder();
            sb.Append(Defines.Format());
            sb.Append(@"
using System.Collections;
using Acknex3.Api;

namespace Acknex3.Script
{
	class " + Formatter.FormatClass(className) + @"
	{
		public void Initialize()
		{
");
            sb.Append(Sections.FormatInit());
            sb.Append(@"
		}
");
            sb.Append(Sections.Format());
            sb.Append(@"
	}
}
");
            Console.WriteLine("(I) SCRIPT formatting finished in " + watch.Elapsed);
            watch.Stop();

            return sb.ToString();
        }

    }
}
