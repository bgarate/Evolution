using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Singular.Evolution.Algorithms;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Core
{
    public class MultithreadedCachedExecutor<G, F> : IExecutor<G, F>
        where G : IGenotype
        where F : IComparable<F>
    {
        private readonly SimpleLRUCache<G, F> fitnessCache = new SimpleLRUCache<G, F>();

        public MultithreadedCachedExecutor(FitnessFunctionDelegate<G, F> fitnessFunction)
        {
            FitnessFunction = fitnessFunction;
        }

        public bool UseMultithreading { get; set; } = true;

        public FitnessFunctionDelegate<G, F> FitnessFunction { get; }

        public void AddToQueue(Action<object> action, object obj)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(action), obj);
        }

        public List<O> AddToQueueAndWait<I, O>(Func<I, O> action, IEnumerable<I> input)
        {
            ParallelResult<I, O> parallelResult = new ParallelResult<I, O>(input, action);
            return parallelResult.Run();
        }

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


        public void AddToQueueAndWait<I>(Action<I> action, IEnumerable<I> input)
        {
            ParallelResult<I, bool> parallelResult = new ParallelResult<I, bool>(input, i =>
            {
                action(i);
                return true;
            });
            parallelResult.Run();
        }
    }
}