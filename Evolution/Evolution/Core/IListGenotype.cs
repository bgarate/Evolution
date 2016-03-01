using System.Collections.Generic;

namespace Singular.Evolution.Core
{
    /// <summary>
    /// Interface for list-like genotypes
    /// </summary>
    /// <typeparam name="G">Genotype</typeparam>
    /// <typeparam name="R">Gene of which the genotype is made of</typeparam>
    /// <seealso cref="Singular.Evolution.Core.IGenotype" />
    /// <seealso cref="System.Collections.Generic.IEnumerable{R}" />
    public interface IListGenotype<G, R> : IGenotype, IEnumerable<R> where G : IListGenotype<G, R>
        where R : IGene, new()
    {
        /// <summary>
        /// Gets thenumber of genes
        /// </summary>
        /// <value>
        /// Number of genes
        /// </value>
        int Count { get; }

        /// <summary>
        /// Gets the <see cref="R"/> at the specified index.
        /// </summary>
        /// <value>
        /// The gene.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        R this[int index] { get; }

        /// <summary>
        /// Gets a copy of the genotype with genes at positions i and j swaped
        /// </summary>
        /// <param name="i">One gene</param>
        /// <param name="j">The other gene</param>
        /// <returns></returns>
        G Swap(int i, int j);

        /// <summary>
        /// Gets a copy of the genotype with gene gene at position i replaced with g
        /// </summary>
        /// <param name="i">The index</param>
        /// <param name="g">The gene</param>
        /// <returns></returns>
        G Replace(int i, R g);
    }
}