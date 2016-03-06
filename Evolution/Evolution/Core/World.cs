using System;
using System.Collections.Generic;
using System.Linq;

namespace Singular.Evolution.Core
{
    /// <summary>
    /// Represents a population on a certain generation.
    /// </summary>
    /// <typeparam name="G"></typeparam>
    /// <typeparam name="F"></typeparam>
    public class World<G, F> where G : IGenotype where F : IComparable<F>
    {
        private readonly IList<Individual<G, F>> population;

        /// <summary>
        /// Initializes a new instance of the <see cref="World{G, F}"/> class.
        /// </summary>
        /// <param name="population">The population.</param>
        public World(IList<Individual<G, F>> population)
        {
            this.population = population;
            BestIndividual = Population.OrderByDescending(i => i.Fitness).First();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="World{G, F}"/> class.
        /// The new world has the old world's next generation and the specified population
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="population">The population.</param>
        public World(World<G, F> world, IList<Individual<G, F>> population) : this(population)
        {
            Generation = world.Generation + 1;
        }

        /// <summary>
        /// Gets the world's population.
        /// </summary>
        /// <value>
        /// The population.
        /// </value>
        public IList<Individual<G, F>> Population => population.ToList();

        /// <summary>
        /// Gets the best world's fitness.
        /// </summary>
        /// <value>
        /// The best fitness.
        /// </value>
        public F BestFitness => BestIndividual.Fitness;

        /// <summary>
        /// Gets the genotype of the individual with the best fitness.
        /// </summary>
        /// <value>
        /// The best genotype.
        /// </value>
        public G BestGenotype => BestIndividual.Genotype;

        /// <summary>
        /// Gets the individual with the best fitness.
        /// </summary>
        /// <value>
        /// The best individual.
        /// </value>
        public Individual<G, F> BestIndividual { get; }

        /// <summary>
        /// Gets the world's generation.
        /// </summary>
        /// <value>
        /// The generation.
        /// </value>
        public int Generation { get; }
    }
}