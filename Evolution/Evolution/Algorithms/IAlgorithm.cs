using System;
using System.Collections.Generic;
using Singular.Evolution.Core;

namespace Singular.Evolution.Algorithms
{
    public delegate F FitnessFunctionDelegate<F>(IGenotype genotype) where F : IComparable<F>;

    public interface IAlgorithm<G, F> where F : IComparable<F> where G : IGenotype
    {
        FitnessFunctionDelegate<F> FitnessFunction { get; }

        IList<Individual<G, F>> Execute(World<G, F> original);
        IList<Individual<G, F>> Initialize();
        bool ShouldStop(World<G, F> world);
    }
}