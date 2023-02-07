using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    abstract class PreProcessorData
    {

        protected static string s_if = "#if";
        protected static string s_else = "#else";
        protected static string s_end = "#end";

        public PreProcessorData()
        {
        }

        public virtual void Add(PreProcessorData data)
        {
            Console.WriteLine("(W) PREPROCESSORDATA: Add function requires appropriate override");
        }

        public virtual void Merge(string condition, PreProcessorData ifData, PreProcessorData elseData)
        {
            Console.WriteLine("(W) PREPROCESSORDATA: Merge function requires appropriate override");
        }
    }
}
