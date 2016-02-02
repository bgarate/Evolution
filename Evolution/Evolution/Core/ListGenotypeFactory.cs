using System.Collections.Generic;
using Singular.Evolution.Genotypes;

namespace Singular.Evolution.Core
{
    internal class ListGenotypeFactory<R> : IListGenotypeFactory<ListGenotype<R>, R> where R : IGene, new()
    {
        public ListGenotype<R> Create(IEnumerable<R> genes)
        {
            return new ListGenotype<R>(genes);
        }
    }
}