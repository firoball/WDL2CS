using System;
using System.Diagnostics;

namespace WDL2CS
{
    public class Transformer
    {
        protected string m_result = string.Empty;

        public string Result => m_result;

        protected virtual void Transform()
        {
            //override and start the transform process here
        }

        protected virtual void Activate()
        {
            //override and assign Transformers to Nodes here
        }

        public void Execute()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Activate();
            Transform();

            Console.WriteLine("(I) TRANSFORMER formatting finished in " + watch.Elapsed);
            watch.Stop();
        }
    }
}
