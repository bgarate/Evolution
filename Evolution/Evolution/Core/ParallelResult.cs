using System;
using System.Collections.Generic;
using System.Threading;

namespace Singular.Evolution.Core
{
    /// <summary>
    /// Used as a way of doing a mapping over a list of inputs, for .NET 3.5, due to lack of .AsParallel for LINQ
    /// </summary>
    /// <typeparam name="I"></typeparam>
    /// <typeparam name="O"></typeparam>
    public class ParallelResult<I, O>
    {
        private readonly object lockObject = new object();
        private readonly List<O> output = new List<O>();

        private readonly ManualResetEvent waitHandle = new ManualResetEvent(false);
        private int counter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParallelResult{I, O}"/> class.
        /// </summary>
        public ParallelResult()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParallelResult{I, O}"/> class.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="task">The task.</param>
        public ParallelResult(IEnumerable<I> input, Func<I, O> task)
        {
            Input = input;
            Task = task;
        }
        /// <summary>
        /// Gets or sets the task list.
        /// </summary>
        /// <value>
        /// The task.
        /// </value>
        /// <remarks>This method is not thread safe</remarks>
        public Func<I, O> Task { get; set; }

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        /// <value>
        /// The input.
        /// </value>
        /// <remarks>This method is not thread safe</remarks>
        public IEnumerable<I> Input { get; set; }

        /// <summary>
        /// Calls the action over the inputs. It uses the threadpool.
        /// </summary>
        /// <returns></returns>
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