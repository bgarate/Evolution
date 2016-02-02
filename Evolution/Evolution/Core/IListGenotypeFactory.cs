using System.Collections.Generic;

namespace Singular.Evolution.Core
{
    public interface IListGenotypeFactory<G, R> : IGenotypeFactory<IListGenotype<R>> where R : IGene, new()
    {
        G Create(IEnumerable<R> genes);
    }
}