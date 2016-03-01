using System;
using System.Collections.Generic;

namespace Singular.Evolution.Core
{
    /// <summary>
    /// Interface for executors.
    /// Executors takes care of the execution of certain tasks between the framework. For example, it can provide a multithreaded
    /// environment for long running tasks of the algorithm, in example, the fitness calculations. Also, it can provide caching,
    /// pausing, stopping and resuming of those tasks.
    /// </summary>
    /// <typeparam name="G"></typeparam>
    /// <typeparam name="F"></typeparam>
    public interface IExecutor<G, F> where G : IGenotype where F : IComparable<F>
    {
        /// <summary>
        /// Adds a task to queue.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="obj">The object.</param>
        void AddToQueue(Action<object> action, object obj);

        /// <summary>
        /// Adds a list of tasks to queue and waits until completion
        /// </summary>
        /// <typeparam name="I">Input v</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="input">The input.</param>
        void AddToQueueAndWait<I>(Action<I> action, IEnumerable<I> input);

        /// <summary>
        /// Adds a list of tasks to queue and returns its results when finished
        /// </summary>
        /// <typeparam name="I"></typeparam>
        /// <typeparam name="O"></typeparam>
        /// <param name="action">The action.</param>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        List<O> AddToQueueAndWait<I, O>(Func<I, O> action, IEnumerable<I> input);

        /// <summary>
        /// Calculates the fitness of given individuals
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns></returns>
        List<Individual<G, F>> UpdateFitness(List<Individual<G, F>> original);
    }
}