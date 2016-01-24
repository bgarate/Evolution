using System;

namespace Singular.Evolution
{
    public interface IIndividual<G, F> where F : IComparable<F>
    {
        IGenotype Genotype { get; }
        F Fitness { get; }
    }
}