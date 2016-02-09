using System;
using Singular.Evolution.Core;
using Singular.Evolution.Genes;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Alterers
{
    public class BitMutator<G,F> : Mutator<G, BitGene, F> where F : IComparable<F> where G: IListGenotype<G,BitGene>
    {
        private readonly RandomGenerator rnd = RandomGenerator.GetInstance();

        public BitMutator(double probability) : base(probability)
        {
        }

        protected override BitGene Mutate(BitGene g)
        {
            return new BitGene(!g.Value);
        }
    }
}