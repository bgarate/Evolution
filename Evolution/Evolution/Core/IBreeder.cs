using System.Collections.Generic;

namespace Singular.Evolution.Core
{
    /// <summary>
    /// Inteface for Breeders.
    /// Breeders are used to generate the initial population for algorithms
    /// </summary>
    /// <typeparam name="G">Genotype</typeparam>
    public interface IBreeder<G> where G : IGenotype
    {
        /// <summary>
        /// Breeds the initial population
        /// </summary>
        /// <returns></returns>
        IList<G> Breed();
    }
}