using System;
using System.Collections.Generic;

namespace Singular.Evolution.Core
{
    public interface IAlterer
    {
        IList<Individual<G,F>> Apply<G,F>(IList<Individual<G,F>> individuals) where G : IGenotype
            where F : IComparable<F>;
    }
}