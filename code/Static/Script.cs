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
            sb.Append(@"
using System.Collections;
using Acknex3.Api;

namespace Acknex3.Script
{
	class " + Formatter.FormatReserved(className) + @"
	{
		public void Initialize()
		{
");
            Sections.Format(sb, true); //initialized data
            sb.Append(@"
		}
");
            Sections.Format(sb, false); //static data
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
