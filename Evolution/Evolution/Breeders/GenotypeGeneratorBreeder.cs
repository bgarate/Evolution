using System.Collections.Generic;
using Singular.Evolution.Core;

namespace Singular.Evolution.Breeders
{
    /// <summary>
    /// Breeds a population by applying delegate
    /// </summary>
    /// <typeparam name="G"></typeparam>
    /// <seealso cref="Singular.Evolution.Core.IBreeder{G}" />
    public class GenotypeGeneratorBreeder<G> : IBreeder<G> where G : IGenotype
    {
        /// <summary>
        /// A delegate which returns a Genotype given an index
        /// </summary>
        /// <param name="i">Index</param>
        /// <returns></returns>
        public delegate G BreederDelegateWithIndex(int i);

        /// <summary>
        /// A delegate which returns a Genotype
        /// </summary>
        /// <returns></returns>
        public delegate G BreederDelegateWithoutIndex();
        //
        private GenotypeGeneratorBreeder(int populationSize)
        {
            PopulationSize = populationSize;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GenotypeGeneratorBreeder{G}"/> class.
        /// </summary>
        /// <param name="breederDelegate">The breeder delegate.</param>
        /// <param name="populationSize">Size of the population.</param>
        public GenotypeGeneratorBreeder(BreederDelegateWithIndex breederDelegate, int populationSize)
            : this(populationSize)
        {
            DelegateWithIndex = breederDelegate;
            DelegateWithoutIndex = null;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GenotypeGeneratorBreeder{G}"/> class.
        /// </summary>
        /// <param name="breederDelegate">The breeder delegate.</param>
        /// <param name="populationSize">Size of the population.</param>
        public GenotypeGeneratorBreeder(BreederDelegateWithoutIndex breederDelegate, int populationSize)
            : this(populationSize)
        {
            DelegateWithoutIndex = breederDelegate;
            DelegateWithIndex = null;
        }
        /// <summary>
        /// Gets the <see cref="BreederDelegateWithIndex"/> if it was given
        /// </summary>
        /// <value>
        /// <see cref="BreederDelegateWithIndex"/> to be used
        /// </value>
        public BreederDelegateWithIndex DelegateWithIndex { get; }

        /// <summary>
        /// Gets the <see cref="BreederDelegateWithoutIndex"/> if it was given
        /// </summary>
        /// <value>
        /// <see cref="BreederDelegateWithoutIndex"/> to be used
        /// </value>
        public BreederDelegateWithoutIndex DelegateWithoutIndex { get; }

        /// <summary>
        /// Gets the size of the population to generate.
        /// </summary>
        /// <value>
        /// The size of the population.
        /// </value>
        public int PopulationSize { get; }

        /// <summary>
        /// Breeds the initial population
        /// </summary>
        /// <returns></returns>
        public IList<G> Breed()
        {
            List<G> population = new List<G>();
            for (int i = 0; i < PopulationSize; i++)
            {
                G genotype = DelegateWithIndex != null ? DelegateWithIndex(i) : DelegateWithoutIndex();
                population.Add(genotype);
            }
            return population;
        }
    }
}