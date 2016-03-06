using System;
using Singular.Evolution.Core;

namespace Singular.Evolution.Alterers
{
    /// <summary>
    /// A mutator which acts on genes applying a delegate
    /// </summary>
    /// <typeparam name="G">Genotype</typeparam>
    /// <typeparam name="R">Gene</typeparam>
    /// <typeparam name="F">Fitness</typeparam>
    /// <seealso cref="BaseMutator{G,R,F}" />
    public class BaseMutatorByDelegate<G, R, F> : BaseMutator<G, R, F> where F : IComparable<F>
        where G : IListGenotype<G, R>
        where R : IGene, new()
    {
        /// <summary>
        /// Delegate to apply the mutation on the g parameter
        /// </summary>
        /// <param name="g">The gene to mutate</param>
        /// <returns></returns>
        public delegate R MutateGeneDelegate(R g);

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseMutatorByDelegate{G, R, F}"/> class.
        /// </summary>
        /// <param name="probability">The probability.</param>
        /// <param name="mutateDelegate">The mutate delegate.</param>
        public BaseMutatorByDelegate(double probability, MutateGeneDelegate mutateDelegate) : base(probability)
        {
            MutateDelegate = mutateDelegate;
        }

        /// <summary>
        /// Delegate to invoke the mutation over a gene
        /// </summary>
        /// <value>
        /// Mutation delegate
        /// </value>
        public MutateGeneDelegate MutateDelegate { get; }

        /// <summary>
        /// Mutates the specified Gene g.
        /// </summary>
        /// <param name="g">Original gene</param>
        /// <returns>
        /// Mutated gene
        /// </returns>
        protected override R Mutate(R g)
        {
            return MutateDelegate(g);
        }
    }
}