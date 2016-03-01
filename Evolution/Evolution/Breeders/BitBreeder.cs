using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Core;
using Singular.Evolution.Genes;
using Singular.Evolution.Genotypes;

namespace Singular.Evolution.Breeders
{
    /// <summary>
    /// Breeds a number of <see cref="IListGenotype{G,BitGene}"/>
    /// </summary>
    /// <seealso cref="Core.IBreeder{ListGenotype{BitGene}}" />
    public class BitBreeder : IBreeder<ListGenotype<BitGene>>
    {
        public BitBreeder(int populationSize, int genotypeSize)
        {
            PopulationSize = populationSize;
            GenotypeSize = genotypeSize;
        }

        /// <summary>
        /// Population size
        /// </summary>
        /// <value>
        /// The population size.
        /// </value>
        public int PopulationSize { get; }

        /// <summary>
        /// Genotype size
        /// </summary>
        /// <value>
        /// The size of the genotype.
        /// </value>
        public int GenotypeSize { get; }

        /// <summary>
        /// Breeds the initial population
        /// </summary>
        /// <returns></returns>
        public IList<ListGenotype<BitGene>> Breed()
        {
            ListGenotype<BitGene> genotype = new ListGenotype<BitGene>(GenotypeSize);
            List<ListGenotype<BitGene>> population = Enumerable.Repeat(genotype, PopulationSize).ToList();
            return population;
        }
    }
}