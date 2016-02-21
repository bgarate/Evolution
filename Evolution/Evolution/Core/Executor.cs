using System;
using System.Collections.Generic;
using System.Threading;

namespace Singular.Evolution.Core
{
    public class Executor
    {
        public bool UseMultithreading { get; set; } = true;

        public void AddToQueue(Action<object> action, object obj)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(action), obj);
        }

        public List<O> AddToQueueAndWait<I, O>(Func<I, O> action, List<I> input)
        {
            ParallelResult<I, O> parallelResult = new ParallelResult<I, O>(input, action);
            return parallelResult.Run();
        }
    }

    public class ParallelResult<I, O>
    {
        private readonly object lockObject = new object();
        private readonly List<O> output = new List<O>();

        private readonly AutoResetEvent waitHandle = new AutoResetEvent(false);
        private int counter;

        public ParallelResult()
        {
        }

        public ParallelResult(List<I> input, Func<I, O> task)
        {
            Input = input;
            Task = task;
        }

        public Func<I, O> Task { get; set; }
        public List<I> Input { get; set; }

        public List<O> Run()
        {
            foreach (I i in Input)
            {
                Interlocked.Increment(ref counter);
                ThreadPool.QueueUserWorkItem(WorkUnit, i);
            }
            waitHandle.WaitOne();
            return output;
        }

        private void WorkUnit(object input)
        {
            O item = Task((I) input);
            lock (lockObject)
            {
                output.Add(item);
            }
            Interlocked.Decrement(ref counter);
            if (counter == 0)
            {
                waitHandle.Set();
            }
        }
    }
}