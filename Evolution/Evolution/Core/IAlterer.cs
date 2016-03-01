using System;
using System.Collections.Generic;

namespace Singular.Evolution.Core
{

    public interface IAlterer
    {
        //object Apply(object individuals);
    }

    /// <summary>
    ///     Interface of alterers.
    /// </summary>
    /// <typeparam name="G">Type of the genotypes it acts upon</typeparam>
    /// <typeparam name="F">Return type of the fitness function</typeparam>
    public interface IAlterer<G, F> : IAlterer where G : IGenotype where F : IComparable<F>
    {
        /// <summary>
        /// Returns the result of applying the alterer over the individuals.
        /// </summary>
        /// <param name="individuals">Input individuals</param>
        /// <returns>Output individuals</returns>
        IList<Individual<G, F>> Apply(IList<Individual<G, F>> individuals);
    }
}