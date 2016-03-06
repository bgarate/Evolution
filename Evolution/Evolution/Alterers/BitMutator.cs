using System;
using Singular.Evolution.Core;
using Singular.Evolution.Genes;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Alterers
{
    /// <summary>
    /// A <see cref="BaseMutator{G,R,F}"/> which flips the state of the bits in a <see cref="IListGenotype{G,R}"/>
    /// </summary>
    /// <typeparam name="G">Genotype</typeparam>
    /// <typeparam name="F">Fitness</typeparam>
    /// <seealso cref="BaseMutator{G,R,F}" />
    public class BitBaseMutator<G, F> : BaseMutator<G, BitGene, F> where F : IComparable<F> where G : IListGenotype<G, BitGene>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BitBaseMutator{G, F}"/> class.
        /// </summary>
        /// <param name="probability">The probability.</param>
        public BitBaseMutator(double probability) : base(probability)
        {
        }

        /// <summary>
        /// Mutates the specified Gene g.
        /// </summary>
        /// <param name="g">Original gene</param>
        /// <returns>
        /// Mutated gene
        /// </returns>
        protected override BitGene Mutate(BitGene g)
        {
            return new BitGene(!g.Value);
        }
    }
}