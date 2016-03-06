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
    /// <seealso>
    ///     <cref>IBreeder{ListGenotype{BitGene}}</cref>
    /// </seealso>
    public class BitBreeder : IBreeder<ListGenotype<BitGene>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BitBreeder"/> class.
        /// </summary>
        /// <param name="populationSize">Size of the population.</param>
        /// <param name="genotypeSize">Size of the genotype.</param>
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