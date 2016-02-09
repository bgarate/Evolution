using System;
using Singular.Evolution.Core;
using Singular.Evolution.Genes;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Alterers
{
    public class BitMutator<F> : Mutator<BitGene, F> where F : IComparable<F>
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