using System;
using System.Collections.Generic;

namespace Singular.Evolution.Core
{
    public interface IExecutor<G, F> where G : IGenotype where F : IComparable<F>
    {
        void AddToQueue(Action<object> action, object obj);
        void AddToQueueAndWait<I>(Action<I> action, IEnumerable<I> input);
        List<O> AddToQueueAndWait<I, O>(Func<I, O> action, IEnumerable<I> input);
        List<Individual<G, F>> UpdateFitness(List<Individual<G, F>> original);
    }
}