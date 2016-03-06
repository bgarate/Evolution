using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Singular.Evolution.Algorithms;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Core
{
    /// <summary>
    /// An <see cref="IExecutor{G,F}"/> multithreaded and with caching for genotype's fitness values
    /// </summary>
    /// <typeparam name="G"></typeparam>
    /// <typeparam name="F"></typeparam>
    /// <seealso cref="Singular.Evolution.Core.IExecutor{G, F}" />
    public class MultithreadedCachedExecutor<G, F> : IExecutor<G, F>
        where G : IGenotype
        where F : IComparable<F>
    {
        private readonly SimpleLRUCache<G, F> fitnessCache = new SimpleLRUCache<G, F>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MultithreadedCachedExecutor{G, F}"/> class.
        /// </summary>
        /// <param name="fitnessFunction">The fitness function.</param>
        public MultithreadedCachedExecutor(FitnessFunctionDelegate<G, F> fitnessFunction)
        {
            FitnessFunction = fitnessFunction;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the executre should use multithreading.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the executor uses multithreading; otherwise, <c>false</c>.
        /// </value>
        public bool UseMultithreading { get; set; } = true;

        /// <summary>
        /// Gets the fitness function used to calculate the fitnesses.
        /// </summary>
        /// <value>
        /// The fitness function.
        /// </value>
        public FitnessFunctionDelegate<G, F> FitnessFunction { get; }

        /// <summary>
        /// Adds a task to queue.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="obj">The object.</param>
        public void AddToQueue(Action<object> action, object obj)
        {
            if (UseMultithreading)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(action), obj);
            }
            else
            {
                action(obj);
            }
        }

        /// <summary>
        /// Adds a list of tasks to queue and waits until completion
        /// </summary>
        /// <typeparam name="I">Input value</typeparam>
        /// <typeparam name="O">Return value</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public List<O> AddToQueueAndWait<I, O>(Func<I, O> action, IEnumerable<I> input)
        {
            if (UseMultithreading)
            {
                ParallelResult<I, O> parallelResult = new ParallelResult<I, O>(input, action);
                return parallelResult.Run();
            }
            else
            {
                return input.Select(action).ToList();
            }
        }


        /// <summary>
        /// Calculates the fitness of given individuals
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns></returns>
        public List<Individual<G, F>> UpdateFitness(List<Individual<G, F>> original)
        {
            Dictionary<G, int> waitUntilCalculation = new Dictionary<G, int>();
            List<Individual<G, F>> result = new List<Individual<G, F>>();

            foreach (Individual<G, F> individual in original)
            {
                F cachedFitness;
                if (fitnessCache.TryGet(individual.Genotype, out cachedFitness))
                {
                    result.Add(new Individual<G, F>(individual.Genotype, cachedFitness));
                }
                else
                {
                    int numberToCreate;

                    if (waitUntilCalculation.TryGetValue(individual.Genotype, out numberToCreate))
                        waitUntilCalculation.Remove(individual.Genotype);

                    numberToCreate++;
                    waitUntilCalculation.Add(individual.Genotype, numberToCreate);
                }
            }

            List<Individual<G, F>> newCalculatedFitneses =
                AddToQueueAndWait(i => new Individual<G, F>(i, FitnessFunction(i)),
                    waitUntilCalculation.Keys);

            Debug.Assert(newCalculatedFitneses.Count == waitUntilCalculation.Count);

            foreach (Individual<G, F> calculatedIndividual in newCalculatedFitneses)
            {
                fitnessCache.Add(calculatedIndividual.Genotype, calculatedIndividual.Fitness);
                int numberToCreate = waitUntilCalculation[calculatedIndividual.Genotype];
                result.AddRange(Enumerable.Repeat(calculatedIndividual, numberToCreate));
            }

            return result;
        }

        /// <summary>
        /// Adds a list of tasks to queue and waits until completion
        /// </summary>
        /// <typeparam name="I">Input v</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="input">The input.</param>
        public void AddToQueueAndWait<I>(Action<I> action, IEnumerable<I> input)
        {
            if (UseMultithreading)
            {
                ParallelResult<I, bool> parallelResult = new ParallelResult<I, bool>(input, i =>
                {
                    action(i);
                    return true;
                });
                parallelResult.Run();
            }
            else
            {
                foreach (I i in input)
                {
                    action(i);
                }
            }
        }
    }
}