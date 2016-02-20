using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Core;
using Singular.Evolution.Genes;
using Singular.Evolution.Genotypes;

namespace Singular.Evolution.Breeders
{
    public class BitBreeder : IBreeder<ListGenotype<BitGene>>
    {
        public BitBreeder(int populationNumber, int listSize)
        {
            PopulationNumber = populationNumber;
            ListSize = listSize;
        }

        public int PopulationNumber { get; }
        public int ListSize { get; }

        public IList<ListGenotype<BitGene>> Breed()
        {
            ListGenotype<BitGene> genotype = new ListGenotype<BitGene>(ListSize);
            List<ListGenotype<BitGene>> population = Enumerable.Repeat(genotype, PopulationNumber).ToList();
            return population;
        }
    }
}