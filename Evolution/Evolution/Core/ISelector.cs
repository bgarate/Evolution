using System;
using System.Collections.Generic;

namespace Singular.Evolution.Core
{
    public interface ISelector<G, F> where G : IGenotype where F : IComparable<F>
    {
        IList<Individual<G, F>> Select(IList<Individual<G, F>> individuals);
    }
}