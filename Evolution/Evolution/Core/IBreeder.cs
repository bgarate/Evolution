using System;
using System.Collections.Generic;
using Singular.Evolution.Genes;
using Singular.Evolution.Genotypes;

namespace Singular.Evolution.Core
{
    public interface IBreeder<G> where G: IGenotype
    {
        IList<G> Breed();
    }

    class RandomBitBreeder : IBreeder<ListGenotype<BitGene>>
    {
        public IList<ListGenotype<BitGene>> Breed()
        {
            throw new NotImplementedException();
        }
    }
}