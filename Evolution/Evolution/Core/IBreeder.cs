using System;
using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Genes;
using Singular.Evolution.Genotypes;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Core
{
    public interface IBreeder<G> where G: IGenotype
    {
        IList<G> Breed();
    }

    public class RandomBitBreeder : IBreeder<ListGenotype<BitGene>>
    {
        public RandomBitBreeder(int populationNumber, int listSize)
        {
            PopulationNumber = populationNumber;
            ListSize = listSize;
        }

        public int PopulationNumber { get; }
        public int ListSize { get; }

        public IList<ListGenotype<BitGene>> Breed()
        {
            List<ListGenotype<BitGene>> population= new List<ListGenotype<BitGene>>();
            for (int i = 0; i < PopulationNumber; i++)
            {
                List<BitGene> genes = new List<BitGene>(ListSize);
                for (int j = 0; j < ListSize; j++)
                {
                    genes.Add(new BitGene(RandomGenerator.GetInstance().NextInt()%2 == 0));
                }
                population.Add(new ListGenotype<BitGene>(genes));
            }
            return population;
        }
    }
}