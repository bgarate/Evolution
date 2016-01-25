using System;
using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Core
{
    public abstract class Mutator<G, F> : IAlterer where G : IGene, new() where F : IComparable<F>

    {
        public Mutator(double probability)
        {
            if (probability < 0 || probability > 1)
                throw new ArgumentException("The mutation probability must be in the range [0,1]");

            Probability = probability;
        }

        public double Probability { get; }

        public IList<Individual<IListGenotype<G>,F>> Apply(IList<Individual<IListGenotype<G>,F>> individuals)
        {

            IList<IListGenotype<G>> genotypes = individuals.Select(i => i.Genotype).ToList();

            int numberOfMutations = (int) Math.Round(genotypes.Sum(g => g.Count)*Probability);

            List<int> targets =
                RandomGenerator.GetInstance().IntSequence(0, genotypes.Count).Take(numberOfMutations).ToList();

            foreach (int genotypeNumber in targets)
            {
                IListGenotype<G> genotype = genotypes[genotypeNumber];
                IListGenotype<G> mutatedGenotype = MutateGene(genotype);
                genotypes[genotypeNumber] = mutatedGenotype;
            }

            return Individual<IListGenotype<G>,F>.FromGenotypes(genotypes);
        }

        private IListGenotype<G> MutateGene(IListGenotype<G> genotype)
        {
            int genePlace = RandomGenerator.GetInstance().NextInt(genotype.Count);

            G orignalGene = genotype[genePlace];
            G mutatedGene = Mutate(orignalGene);

            IListGenotype<G> mutatedGenotype = genotype.Replace(genePlace, mutatedGene);
            return mutatedGenotype;
        }

        protected abstract G Mutate(G g);

        IList<Individual<G1,F1>> IAlterer.Apply<G1,F1>(IList<Individual<G1,F1>> individuals)
        {
            return (IList<Individual<G1,F1>>) Apply((IList<Individual<IListGenotype<G>,F>>)individuals);
        }
    }
}