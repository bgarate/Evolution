using System;
using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Core;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Alterers
{
    /// <summary>
    /// This alterer takes two genes at random at a <see cref="IListGenotype{G,R}"/> and swaps them
    /// </summary>
    /// <typeparam name="G"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <typeparam name="F"></typeparam>
    /// <seealso cref="Singular.Evolution.Core.IAlterer{G, F}" />
    public class SwapAlterer<G, R, F> : IAlterer<G, F> where G : IListGenotype<G, R>
        where F : IComparable<F>
        where R : class, IGene, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SwapAlterer{G, R, F}"/> class.
        /// </summary>
        /// <param name="probability">The probability.</param>
        public SwapAlterer(double probability)
        {
            Probability = probability;
        }

        /// <summary>
        /// Gets the probability.
        /// </summary>
        /// <value>
        /// The probability.
        /// </value>
        public double Probability { get; }

        /// <summary>
        /// Returns the result of applying the alterer over the individuals.
        /// This alterer returns al list of the parents, modified or not.
        /// </summary>
        /// <param name="parents">The parents.</param>
        /// <returns></returns>
        public IList<Individual<G, F>> Apply(IList<Individual<G, F>> parents)
        {
            List<G> offspring = new List<G>(parents.Select(g => g.Genotype));

            int numberOfMutations = (int) (parents.Count*Probability);

            foreach (
                int location in RandomGenerator.GetInstance().IntSequence(0, offspring.Count).Take(numberOfMutations))
            {
                offspring[location] = Mutate(offspring[location]);
            }

            return Individual<G, F>.FromGenotypes(offspring).ToList();
        }

        private G Mutate(G parent)
        {
            int count = parent.Count;

            RandomGenerator rnd = RandomGenerator.GetInstance();

            int point1 = rnd.NextInt(0, count);
            int point2;

            do
            {
                point2 = rnd.NextInt(0, count);
            } while (point1 == point2);


            G child = parent.Swap(point1, point2);

            return child;
        }
    }
}