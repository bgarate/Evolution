using System;
using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Alterers;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Core
{
    /// <summary>
    /// Based on another alterer, the recombinator represents a new alterer which applies multiple times the alterer over a number of randomly chosen
    /// parents from the input individuals
    /// </summary>
    /// <typeparam name="G"></typeparam>
    /// <typeparam name="F"></typeparam>
    /// <seealso cref="Singular.Evolution.Core.IAlterer{G, F}" />
    public class Recombinator<G, F> : IAlterer<G, F> where F : IComparable<F> where G : IGenotype

    {
        /// <summary>
        /// What the NumberOfRecombinations number represents
        /// </summary>
        public enum RecombinatioNumberType
        {
            /// <summary>
            /// The absolute number of recombinations
            /// </summary>
            Absolute,
            /// <summary>
            /// A probability of a parent to be chosen for recombination
            /// In this case the number of recombinations to be made is given by (int)(NumberOfRecombinations*individuals.Count / NumberOfParents);
            /// </summary>
            Probability
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Recombinator{G, F}"/> class.
        /// </summary>
        /// <param name="alterer">The alterer.</param>
        /// <param name="numberOfParents">The number of parents.</param>
        /// <param name="numberOfRecombinations">The number of recombinations.</param>
        /// <param name="recombinationType">Type of the recombination.</param>
        /// <exception cref="System.ArgumentException">
        /// </exception>
        public Recombinator(IAlterer<G, F> alterer, int numberOfParents, double numberOfRecombinations,
            RecombinatioNumberType recombinationType)
        {
            Alterer = alterer;
            NumberOfRecombinations = numberOfRecombinations;
            NumberOfParents = numberOfParents;
            RecombinationType = recombinationType;

            if ((recombinationType == RecombinatioNumberType.Probability) &&
                !MathHelper.IsProbabilty(numberOfRecombinations))
                throw new ArgumentException($"The value of {numberOfRecombinations} must in the range [0,1]");

            if ((recombinationType == RecombinatioNumberType.Absolute) && !MathHelper.IsInteger(numberOfRecombinations))
                throw new ArgumentException($"The value of {numberOfRecombinations} must be an integer");
        }

        /// <summary>
        /// Gets the alterer to be applied.
        /// </summary>
        /// <value>
        /// The alterer.
        /// </value>
        public IAlterer<G, F> Alterer { get; }

        /// <summary>
        /// Gets the number of recombinations. What this value represents is determined by <see cref="RecombinationType"/>
        /// </summary>
        /// <value>
        /// The number of recombinations.
        /// </value>
        public double NumberOfRecombinations { get; }

        /// <summary>
        /// Gets the number of parents to be passed to the <see cref="Alterer"/>.
        /// </summary>
        /// <value>
        /// The number of parents.
        /// </value>
        public int NumberOfParents { get; }

        /// <summary>
        /// Gets or sets what <see cref="NumberOfRecombinations"/> determines.
        /// </summary>
        /// <value>
        /// The type of the recombination.
        /// </value>
        public RecombinatioNumberType RecombinationType { get; set; }


        /// <summary>
        /// Returns the result of applying the alterer over the individuals.
        /// </summary>
        /// <param name="individuals">Input individuals</param>
        /// <returns>
        /// Output individuals
        /// </returns>
        public IList<Individual<G, F>> Apply(IList<Individual<G, F>> individuals)
        {
            List<Individual<G, F>> offspring = new List<Individual<G, F>>();

            int recombinations =
                (int)
                    (NumberOfRecombinations*
                     (RecombinationType == RecombinatioNumberType.Probability ? (double)individuals.Count / NumberOfParents : 1.0));

            for (int i = 0; i < recombinations; i++)
            {
                IList<Individual<G, F>> parents = individuals.RandomTake(NumberOfParents).ToList();
                offspring.AddRange(Alterer.Apply(parents));
            }

            return offspring;
        }

        /// <summary>
        /// Returns a recombinator made from a crossover
        /// </summary>
        /// <typeparam name="G2">The type of the 2.</typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="crossover">The crossover.</param>
        /// <param name="numberOfRecombinations">The number of recombinations.</param>
        /// <param name="recombinationType">Type of the recombinatio.</param>
        /// <returns></returns>
        public static Recombinator<G2, F> FromCrossover<G2, R>(CrossoverBase<G2, R, F> crossover,
            double numberOfRecombinations, Recombinator<G2, F>.RecombinatioNumberType recombinationType)
            where R : IGene, new()
            where G2 : IListGenotype<G2, R>
        {
            return new Recombinator<G2, F>(crossover, crossover.NumberOfParentsNeeded, numberOfRecombinations,
                recombinationType);
        }
    }
}