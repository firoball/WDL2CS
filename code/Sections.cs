using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Sections
    {
        private static readonly string s_nl = Environment.NewLine;

        public static string CreatePreProcIfNotCondition(string expr, string stream)
        {
            string s = string.Empty;

            s += "#if !(" + expr + ")" + s_nl;
            s += stream;

            return s;
        }


        public static string CreatePreProcIfCondition(string expr, string stream)
        {
            string s = string.Empty;

            s += "#if " + expr + s_nl;
            s += stream;

            return s;
        }

        public static string CreatePreProcElseCondition(string ifstream, string elsestream)
        {
            string s = string.Empty;

            s += ifstream;
            s += "#else" + s_nl;
            s += elsestream;
            s += "#endif" + s_nl;

            return s;
        }

        public static string CreatePreProcEndCondition(string stream)
        {
            string s = string.Empty;

            s += stream + s_nl;
            s += "#endif";

            return s;
        }


    }
}
