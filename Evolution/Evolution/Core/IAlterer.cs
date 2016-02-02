using System;
using System.Collections.Generic;

namespace Singular.Evolution.Core
{
    public interface IAlterer
    {
        //object Apply(object individuals);
    }

    public interface IAlterer<G, F> : IAlterer where G : IGenotype where F : IComparable<F>
    {
        IList<Individual<G, F>> Apply(IList<Individual<G, F>> individuals);
    }
}