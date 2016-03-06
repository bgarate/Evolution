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
        /// <summary>
        /// Applies a scaling function to the list of fitneses
        /// The i-th element of the returned list matches with the i-th element of the input list
        /// </summary>
        /// <param name="originalFitneses"></param>
        /// <returns></returns>
        List<F> Scale(List<F> originalFitneses);
    }
}