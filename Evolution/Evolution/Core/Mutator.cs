using System;
using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Genotypes;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Core
{
    /// <summary>
    /// Base class to define a mutator on a <c>IListGenotype</c>.
    /// </summary>
    /// <typeparam name="R">Type of genes</typeparam>
    /// <typeparam name="F">Return type of the fitness function</typeparam>
    public abstract class Mutator<G, R, F> : IAlterer<G, F> where F : IComparable<F> where R : IGene, new() where G : IListGenotype<G,R>

    {

        public Mutator(double probability)
        {
            if (probability < 0 || probability > 1)
                throw new ArgumentException("The mutation probability must be in the range [0,1]");

            Probability = probability;
        }

        public double Probability { get; }

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

            R orignalGene = genotype[genePlace];
            R mutatedGene = Mutate(orignalGene);

            G mutatedGenotype = genotype.Replace(genePlace, mutatedGene);
            return mutatedGenotype;
        }

        protected abstract R Mutate(R g);

        public object Apply(object individuals)
        {
            return Apply((IList<Individual<ListGenotype<R>, F>>) individuals);
        }
    }
}