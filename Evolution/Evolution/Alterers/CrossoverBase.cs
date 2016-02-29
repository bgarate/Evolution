using System;
using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Core;

namespace Singular.Evolution.Alterers
{
    /// <summary>
    /// Base class for crossovers. Does the basic validations on input parents
    /// Child classes call it's constructor passing their input requirements
    /// </summary>
    /// <typeparam name="G">Genotype</typeparam>
    /// <typeparam name="R">Gene</typeparam>
    /// <typeparam name="F">Fitness</typeparam>
    /// <seealso cref="Singular.Evolution.Core.IAlterer{G, F}" />
    public abstract class CrossoverBase<G, R, F> : IAlterer<G, F> where G : IListGenotype<G, R>
        where F : IComparable<F>
        where R : IGene, new()
    {
        public CrossoverBase(int numberOfParentsNeeded, int numberOfChildsGenerated, int minimumGenesRequired,
            bool sameLengthParentsNeeded)
        {
            NumberOfParentsNeeded = numberOfParentsNeeded;
            NumberOfChildsGenerated = numberOfChildsGenerated;
            SameLengthParentsNeeded = sameLengthParentsNeeded;
            MinimumGenesRequired = minimumGenesRequired;
        }

        public int NumberOfParentsNeeded { get; }
        public bool SameLengthParentsNeeded { get; }
        public int NumberOfChildsGenerated { get; }
        public int MinimumGenesRequired { get; }

        public IList<Individual<G, F>> Apply(IList<Individual<G, F>> parents)
        {
            if (parents.Count() != NumberOfParentsNeeded)
                throw new ArgumentException($"Input expected {NumberOfParentsNeeded} parents");


            int count = parents[0].Genotype.Count;

            foreach (Individual<G, F> individual in parents)
            {
                if (SameLengthParentsNeeded && parents.Any(p => p.Genotype.Count != count))
                    throw new ArgumentException("Parents should have same length");

                if (MinimumGenesRequired > individual.Genotype.Count)
                    throw new ArgumentException($"Parents should have at least {MinimumGenesRequired} genes");
            }

            return Individual<G, F>.FromGenotypes(GetOffspring(parents.Select(g => g.Genotype)));
        }

        /// <summary>
        /// Gets the offspring.
        /// This method is called by CrossoverBase's Apply after a successful validation
        /// </summary>
        /// <param name="parents">The parents.</param>
        /// <returns></returns>
        protected abstract IList<G> GetOffspring(IEnumerable<G> parents);
    }
}