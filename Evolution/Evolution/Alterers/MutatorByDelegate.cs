using System;
using Singular.Evolution.Core;

namespace Singular.Evolution.Alterers
{
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