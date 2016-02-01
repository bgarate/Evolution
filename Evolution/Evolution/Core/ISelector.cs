using System;
using System.Collections.Generic;

namespace Singular.Evolution.Core
{
    public interface ISelector<G, F> : IAlterer<G,F> where G : IGenotype where F : IComparable<F>
    {
        
    }
}