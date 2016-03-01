using System;
using System.Collections.Generic;
using System.Linq;

namespace Singular.Evolution.Core
{
    /// <summary>
    /// Represents an Individual given by a Genotype and it's corresponding Fitness
    /// </summary>
    /// <typeparam name="G"></typeparam>
    /// <typeparam name="F"></typeparam>
    /// <seealso cref="System.IComparable{Singular.Evolution.Core.Individual{G, F}}" />
    public class Individual<G, F> : IComparable<Individual<G, F>> where F : IComparable<F> where G : IGenotype
    {
        private F fitness;

        /// <summary>
        /// Initializes a new instance of the <see cref="Individual{G, F}"/> class.
        /// </summary>
        /// <param name="genotype">The genotype.</param>
        public Individual(G genotype)
        {
            Genotype = genotype;
            HasFitnessAssigned = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Individual{G, F}"/> class.
        /// </summary>
        /// <param name="genotype">The genotype.</param>
        /// <param name="fitness">The fitness.</param>
        public Individual(G genotype, F fitness)
        {
            Genotype = genotype;
            Fitness = fitness;
        }

        /// <summary>
        /// Gets a value indicating whether this instance has fitness assigned.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has fitness assigned; otherwise, <c>false</c>.
        /// </value>
        public bool HasFitnessAssigned { get; private set; }

        /// <summary>
        /// Gets the genotype.
        /// </summary>
        /// <value>
        /// The genotype.
        /// </value>
        public G Genotype { get; }

        /// <summary>
        /// Gets the fitness.
        /// </summary>
        /// <value>
        /// The fitness.
        /// </value>
        /// <exception cref="System.InvalidOperationException">This individual has not fitness assigned</exception>
        public F Fitness
        {
            get
            {
                if (!HasFitnessAssigned)
                    throw new InvalidOperationException("This individual has not fitness assigned");

                return fitness;
            }
            private set
            {
                fitness = value;
                HasFitnessAssigned = true;
            }
        }

        /// <summary>
        /// Gets the fitness or its default value.
        /// </summary>
        /// <value>
        /// The fitness or default.
        /// </value>
        public F FitnessOrDefault => HasFitnessAssigned ? fitness : default(F);

        /// <summary>
        /// Compares to another Individual based on their Fitness
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public int CompareTo(Individual<G, F> other)
        {
            return Fitness.CompareTo(other.Fitness);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public Individual<G, F> Clone()
        {
            return new Individual<G, F>(Genotype);
        }

        /// <summary>
        /// Returns individuals from a list of genotypes. Fitness gets its default value.
        /// </summary>
        /// <param name="genotypes">The genotypes.</param>
        /// <returns></returns>
        public static IList<Individual<G, F>> FromGenotypes(IList<G> genotypes)
        {
            return genotypes.Select(g => new Individual<G, F>(g)).ToList();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{{ Fitness: {(HasFitnessAssigned ? Fitness.ToString() : "-")} Genotype: {Genotype} }}";
        }
    }

    /// <summary>
    /// Extension methods for individuals
    /// </summary>
    public static class IndividualExtensions
    {
        /// <summary>
        /// Gets the genotypes of a list of individuals
        /// </summary>
        /// <typeparam name="G"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <param name="individuals">The individuals.</param>
        /// <returns></returns>
        public static IList<G> GetGenotypes<G, F>(this IList<Individual<G, F>> individuals) where F : IComparable<F>
            where G : IGenotype
        {
            return individuals.Select(i => i.Genotype).ToList();
        }
    }
}