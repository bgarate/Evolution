using System;
using Singular.Evolution.Core;
using Singular.Evolution.Genes;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Alterers
{
    public class GaussianMutator<G,F> : Mutator<G,FloatGene, F> where F : IComparable<F> where G: IListGenotype<G,FloatGene>
    {
        private readonly RandomGenerator rnd = RandomGenerator.GetInstance();

        public GaussianMutator(double probability) : base(probability)
        {
        }

        protected override FloatGene Mutate(FloatGene g)
        {
            return g.IsBounded
                ? new FloatGene(rnd.NextBoundedGaussian(g.Value, g.MaxValue.Value, g.MinValue.Value), g.MinValue,
                    g.MaxValue)
                : new FloatGene(rnd.NextGaussian(g.Value));
        }
    }
}