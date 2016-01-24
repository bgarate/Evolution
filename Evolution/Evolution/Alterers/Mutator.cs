using System;
using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Genes;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Alterers
{
    public abstract class Mutator<G> where G : IGene, new()
    {
        public Mutator(double probability)
        {
            if (probability < 0 || probability > 1)
                throw new ArgumentException("The mutation probability must be in the range [0,1]");

            Probability = probability;
        }

        public double Probability { get; }

        public IEnumerable<IListGenotype<G>> Apply(IList<IListGenotype<G>> genotypes)
        {
            IList<IListGenotype<G>> offspring = new List<IListGenotype<G>>();

            int numberOfMutations = (int) Math.Round(genotypes.Sum(g => g.Count)*Probability);

            List<IListGenotype<G>> targets = genotypes.RandomTake(numberOfMutations).ToList();

            foreach (IListGenotype<G> genotype in targets)
            {
                int i = RandomGenerator.GetInstance().NextInt(genotype.Count);

                G orignalGene = genotype[i];
                G mutatedGene = Mutate(orignalGene);

                IListGenotype<G> mutatedGenotype = genotype.Replace(i, mutatedGene);
                offspring.Add(mutatedGenotype);
            }

            return offspring;
        }

        public abstract G Mutate(G g);
    }

    internal class GaussianMutator : Mutator<FloatGene>
    {
        private readonly RandomGenerator rnd = RandomGenerator.GetInstance();

        public GaussianMutator(double probability) : base(probability)
        {
        }

        public override FloatGene Mutate(FloatGene g)
        {
            return g.IsBounded
                ? new FloatGene(rnd.NextBoundedGaussian(g.Value, g.MaxValue.Value, g.MinValue.Value), g.MinValue,
                    g.MaxValue)
                : new FloatGene(rnd.NextGaussian(g.Value));
        }
    }
}