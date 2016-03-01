using System.Collections.Generic;

namespace Singular.Evolution.Core
{
    /// <summary>
    /// Interface for <see cref="IListGenotype{G,R}"/>
    /// </summary>
    /// <typeparam name="G"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <seealso cref="Singular.Evolution.Core.IGenotypeFactory{Singular.Evolution.Core.IListGenotype{G, R}}" />
    public interface IListGenotypeFactory<G, R> : IGenotypeFactory<IListGenotype<G, R>> where R : IGene, new()
        where G : IListGenotype<G, R>
    {
        G Create(IEnumerable<R> genes);
    }
}