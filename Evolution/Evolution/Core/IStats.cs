using System;

namespace Singular.Evolution.Core
{
    public interface IStats<G, F> where F : IComparable<F> where G : IGenotype
    {
        int BestIndividualGeneration { get; }
        Individual<G,F> BestIndividual { get; }
        World<G, F> BestWorld { get; }
        F BestFitness { get; }
    }
}