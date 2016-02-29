using System;
using System.Collections.Generic;
using System.Threading;

namespace Singular.Evolution.Core
{
    public class ParallelResult<I, O>
    {
        private readonly object lockObject = new object();
        private readonly List<O> output = new List<O>();

        private readonly ManualResetEvent waitHandle = new ManualResetEvent(false);
        private int counter;

        public ParallelResult()
        {
        }

        public ParallelResult(IEnumerable<I> input, Func<I, O> task)
        {
            Input = input;
            Task = task;
        }

        public Func<I, O> Task { get; set; }
        public IEnumerable<I> Input { get; set; }

        public List<O> Run()
        {
            lock (lockObject)
            {
                counter = 0;
                foreach (I i in Input)
                {
                    counter++;
                    ThreadPool.QueueUserWorkItem(WorkUnit, i);
                }
            }
            waitHandle.WaitOne();
            waitHandle.Close();
            return output;
        }

        private void WorkUnit(object input)
        {
            O item = Task((I) input);
            lock (lockObject)
            {
                output.Add(item);
                counter--;
                if (counter == 0)
                {
                    waitHandle.Set();
                }
            }
        }
    }
}