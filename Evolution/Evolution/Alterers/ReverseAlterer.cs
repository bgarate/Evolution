using System;
using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Core;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Alterers
{
    /// <summary>
    /// This alterer takes a slice of the <see cref="IListGenotype{G,R}"/> and reverses it
    /// </summary>
    /// <typeparam name="G">Genotype</typeparam>
    /// <typeparam name="R">Genes</typeparam>
    /// <typeparam name="F">Fitness</typeparam>
    /// <seealso cref="Core.IAlterer{G, F}" />
    public class ReverseAlterer<G, R, F> : IAlterer<G, F> where G : IListGenotype<G, R>
        where F : IComparable<F>
        where R : class, IGene, new()
    {
        public ReverseAlterer(double probability)
        {
            Probability = probability;
        }

        /// <summary>
        /// Gets the probability of the alterer to be applied to a given genotype of the parents
        /// Is important to note of genotypes on which the reverse is applied is given by (int)(parents.Count*Probability) and that
        /// the parents may repeat
        /// </summary>
        /// <value>
        /// The probability.
        /// </value>
        public double Probability { get; }

        /// <summary>
        /// Applies the alterer on the parents and returns the offspring
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

            int point1 = rnd.NextInt(1, count);
            int point2 = rnd.NextInt(point1 + 1, count + 1);
            int toCenter = (point2 - point1)/2;

            List<R> genes = new List<R>(parent);

            for (int i = point1; i < toCenter; i++)
            {
                R gene = genes[i];
                genes[i] = genes[toCenter + i];
                genes[toCenter + i] = gene;
            }

            IListGenotypeFactory<G, R> factory = Factory.GetInstance().BuildFactory<G, IListGenotypeFactory<G, R>>();

            return factory.Create(genes);
        }
    }
}