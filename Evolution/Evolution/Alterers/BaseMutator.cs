using System;
using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Core;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Alterers
{
    /// <summary>
    ///     Base class to define a mutator which acts one gene at a time
    /// </summary>
    /// <typeparam name="G">Type of the genotype</typeparam>
    /// <typeparam name="R">Type of genes</typeparam>
    /// <typeparam name="F">Return type of the fitness function</typeparam>
    public abstract class BaseMutator<G, R, F> : IAlterer<G, F> where F : IComparable<F>
        where R : IGene, new()
        where G : IListGenotype<G, R>

    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseMutator{G, R, F}"/> class.
        /// </summary>
        /// <param name="probability">The probability.</param>
        /// <exception cref="System.ArgumentException">The mutation probability must be in the range [0,1]</exception>
        public BaseMutator(double probability)
        {
            if (probability < 0 || probability > 1)
                throw new ArgumentException("The mutation probability must be in the range [0,1]");

            Probability = probability;
        }

        /// <summary>
        /// Gets the probability of the mutator to act in a given gene between all genes on all genotypes
        /// The number of affected genes is give by <code>Math.Round(genotypes.Sum(g=>g.Count))*Probability</code>
        /// </summary>
        /// <value>
        /// The probability.
        /// </value>
        public double Probability { get; }

        /// <summary>
        /// Returns the result of applying the alterer over the individuals.
        /// </summary>
        /// <param name="individuals">Input individuals</param>
        /// <returns>
        /// Output individuals
        /// </returns>
        public IList<Individual<G, F>> Apply(IList<Individual<G, F>> individuals)
        {
            IList<G> genotypes = individuals.Select(i => i.Genotype).ToList();

            int numberOfMutations = (int) Math.Round(genotypes.Sum(g => g.Count)*Probability);

            List<int> targets =
                RandomGenerator.GetInstance().IntSequence(0, genotypes.Count).Take(numberOfMutations).ToList();

            foreach (int genotypeNumber in targets)
            {
                G genotype = genotypes[genotypeNumber];
                G mutatedGenotype = MutateGene(genotype);
                genotypes[genotypeNumber] = mutatedGenotype;
            }

            return Individual<G, F>.FromGenotypes(genotypes);
        }

        private G MutateGene(G genotype)
        {
            int genePlace = RandomGenerator.GetInstance().NextInt(genotype.Count);

            R originalGene = genotype[genePlace];
            R mutatedGene = Mutate(originalGene);

            G mutatedGenotype = genotype.Replace(genePlace, mutatedGene);
            return mutatedGenotype;
        }

        /// <summary>
        /// Mutates the specified Gene g.
        /// </summary>
        /// <param name="g">Original gene</param>
        /// <returns>Mutated gene</returns>
        protected abstract R Mutate(R g);
    }
}