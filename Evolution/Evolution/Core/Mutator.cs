using System;
using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Core
{
    public abstract class Mutator<R, F> : IAlterer<IListGenotype<R>, F> where F : IComparable<F> where R : IGene, new()

    {
        public Mutator(double probability)
        {
            if (probability < 0 || probability > 1)
                throw new ArgumentException("The mutation probability must be in the range [0,1]");

            Probability = probability;
        }

        public double Probability { get; }

        public IList<Individual<IListGenotype<R>, F>> Apply(IList<Individual<IListGenotype<R>, F>> individuals)
        {
            IList<IListGenotype<R>> genotypes = individuals.Select(i => i.Genotype).ToList();

            int numberOfMutations = (int) Math.Round(genotypes.Sum(g => g.Count)*Probability);

            List<int> targets =
                RandomGenerator.GetInstance().IntSequence(0, genotypes.Count).Take(numberOfMutations).ToList();

            foreach (int genotypeNumber in targets)
            {
                IListGenotype<R> genotype = genotypes[genotypeNumber];
                IListGenotype<R> mutatedGenotype = MutateGene(genotype);
                genotypes[genotypeNumber] = mutatedGenotype;
            }

            return Individual<IListGenotype<R>, F>.FromGenotypes(genotypes);
        }

        private IListGenotype<R> MutateGene(IListGenotype<R> genotype)
        {
            int genePlace = RandomGenerator.GetInstance().NextInt(genotype.Count);

            R orignalGene = genotype[genePlace];
            R mutatedGene = Mutate(orignalGene);

            IListGenotype<R> mutatedGenotype = genotype.Replace(genePlace, mutatedGene);
            return mutatedGenotype;
        }

        protected abstract R Mutate(R g);

        public object Apply(object individuals)
        {
            return Apply((IList<Individual<IListGenotype<R>, F>>) individuals);
        }
    }
}