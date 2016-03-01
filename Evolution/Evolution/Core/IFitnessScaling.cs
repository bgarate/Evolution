using System;
using System.Collections.Generic;

namespace Singular.Evolution.Core
{
    /// <summary>
    /// Inteface for fitness scaling
    /// </summary>
    /// <typeparam name="F"></typeparam>
    public interface IFitnessScaling<F> where F : IComparable<F>
    {
        List<F> Scale(List<F> originalFitneses);
    }
}