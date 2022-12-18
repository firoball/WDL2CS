using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCCCompiler;

namespace WDL2CS
{
    class Include
    {
        public static string Process(string file)
        {
            file = file.Replace("\"", string.Empty);
            if (File.Exists(file))
            {
                Console.WriteLine("(I) INCLUDE found: " + file);
                return MyCompiler.SubScanner(file);
            }
            else
            {
                Console.WriteLine("(W) INCLUDE missing: " + file);
                return string.Empty;
            }
        }
    }
}
