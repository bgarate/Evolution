using System;
using System.Collections.Generic;

namespace Singular.Evolution.Core
{
    public interface IFitnessScaling<F> where F:IComparable<F>
    {
        List<F> Scale(List<F> originalFitneses);
    }
}