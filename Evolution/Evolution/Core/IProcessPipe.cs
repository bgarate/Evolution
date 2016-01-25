using System;
using System.Collections.Generic;

namespace Singular.Evolution.Core
{
    public interface IPipe<G,F> where F : IComparable<F>
    {
        IList<Individual<G,F>> Input(IList<Individual<G,F>> genotype);
    }
}