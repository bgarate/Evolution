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
    /// <seealso cref="Singular.Evolution.Core.Mutator{G, R, F}" />
    public class MutatorByDelegate<G, R, F> : Mutator<G, R, F> where F : IComparable<F>
        where G : IListGenotype<G, R>
        where R : IGene, new()
    {
        public delegate R MutateGeneDelegate(R g);

        public MutatorByDelegate(double probability, MutateGeneDelegate mutateDelegate) : base(probability)
        {
            MutateDelegate = mutateDelegate;
        }

        public MutateGeneDelegate MutateDelegate { get; }

        protected override R Mutate(R g)
        {
            return MutateDelegate(g);
        }
    }
}