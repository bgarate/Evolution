using System;
using System.Collections.Generic;
using Singular.Evolution.Core;

namespace Singular.Evolution.Algorithms
{
    public delegate F FitnessFunctionDelegate<in G, out F>(G genotype) where F : IComparable<F> where G : IGenotype;

    public interface IAlgorithm<G, F> where F : IComparable<F> where G : IGenotype
    {
        FitnessFunctionDelegate<G, F> FitnessFunction { get; }
        Engine<G, F> Engine { get; set; }

        IList<Individual<G, F>> Execute(World<G, F> original);
        IList<Individual<G, F>> Initialize();
        bool ShouldStop(World<G, F> world);
    }
}