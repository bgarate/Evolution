using System.Collections.Generic;

namespace Singular.Evolution.Core
{
    /// <summary>
    /// Inteface for factories of <see cref="IListGenotype{G,R}"/>
    /// </summary>
    /// <typeparam name="G"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <seealso cref="IListGenotype{G,R}" />
    public interface IListGenotypeFactory<G, R> : IGenotypeFactory<IListGenotype<G, R>> where R : IGene, new()
        where G : IListGenotype<G, R>
    {
        /// <summary>
        /// Creates a new IListGenotype from the specified genes.
        /// </summary>
        /// <param name="genes">The genes.</param>
        /// <returns></returns>
        G Create(IEnumerable<R> genes);
    }
}