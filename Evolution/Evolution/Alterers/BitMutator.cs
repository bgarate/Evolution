using System;
using Singular.Evolution.Core;
using Singular.Evolution.Genes;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Alterers
{
    /// <summary>
    /// A <see cref="Mutator{G,R,F}"/> which flips the state of the bits in a <see cref="IListGenotype{G,R}"/>
    /// </summary>
    /// <typeparam name="G">Genotype</typeparam>
    /// <typeparam name="F">Fitness</typeparam>
    /// <seealso cref="Mutator{G, BitGene, F}" />
    public class BitMutator<G, F> : Mutator<G, BitGene, F> where F : IComparable<F> where G : IListGenotype<G, BitGene>
    {
        public BitMutator(double probability) : base(probability)
        {
        }

        protected override BitGene Mutate(BitGene g)
        {
            return new BitGene(!g.Value);
        }
    }
}