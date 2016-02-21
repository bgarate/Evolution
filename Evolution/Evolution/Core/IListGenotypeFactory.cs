using System.Collections.Generic;

namespace Singular.Evolution.Core
{
    public interface IListGenotypeFactory<G, R> : IGenotypeFactory<IListGenotype<G, R>> where R : IGene, new()
        where G : IListGenotype<G, R>
    {
        G Create(IEnumerable<R> genes);
    }
}