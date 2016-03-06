using System;
using Singular.Evolution.Core;
using Singular.Evolution.Genes;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Alterers
{
    /// <summary>
    /// A mutator which applies on <see cref="IListGenotype{G,FloatGene}"/>. The result of this mutator applied over a A <see cref="FloatGene"/> is a new B
    /// <see cref="FloatGene"/> (with same boundaries) and a new value with a Gaussian distribution for unbounded genes, or <see cref="RandomGenerator.NextBoundedGaussian">BoundedGaussian</see>
    /// for bounded and mean A.Value with one standard deviation for unbounded genes <see cref="RandomGenerator.NextBoundedGaussian">BoundedGaussian</see> for bounded genes)
    /// </summary>
    /// <typeparam name="G">Genotype</typeparam>
    /// <typeparam name="F">Fitness</typeparam>
    /// <seealso cref="BaseMutator{G,R,F}" />
    public class GaussianBaseMutator<G, F> : BaseMutator<G, FloatGene, F> where F : IComparable<F>
        where G : IListGenotype<G, FloatGene>
    {
        private readonly RandomGenerator rnd = RandomGenerator.GetInstance();

        /// <summary>
        /// Initializes a new instance of the <see cref="GaussianBaseMutator{G, F}"/> class.
        /// </summary>
        /// <param name="probability">The probability.</param>
        public GaussianBaseMutator(double probability) : base(probability)
        {
        }

        /// <summary>
        /// Mutates the specified Gene g.
        /// </summary>
        /// <param name="g">Original gene</param>
        /// <returns>
        /// Mutated gene
        /// </returns>
        protected override FloatGene Mutate(FloatGene g)
        {
            return g.IsBounded
                ? new FloatGene(rnd.NextBoundedGaussian(g.Value, g.MaxValue.Value, g.MinValue.Value), g.MinValue,
                    g.MaxValue)
                : new FloatGene(rnd.NextGaussian(g.Value));
        }
    }
}