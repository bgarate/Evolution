using System;
using System.Collections.Generic;
using Singular.Evolution.Core;

namespace Singular.Evolution.Algorithms
{
    public interface IFitnessExecutor<G, F>
        where G : IGenotype
        where F : IComparable<F>
    {
        MultithreadedCachedExecutor<G,F> ParentMultithreadedCachedExecutor { get; }
        FitnessFunctionDelegate<G, F> FitnessFunction { get; }

        List<Individual<G, F>> UpdateFitness(List<Individual<G, F>> original);
    }
}