using System;
using System.Collections.Generic;
using System.Linq;

namespace Singular.Evolution.Core
{
    public class World<G, F> where G : IGenotype where F : IComparable<F>
    {
        private readonly IList<Individual<G, F>> population;

        public World(IList<Individual<G, F>> population)
        {
            this.population = population;
            BestIndividual = Population.OrderByDescending(i => i.Fitness).First();
        }

        public World(World<G, F> world, IList<Individual<G, F>> population) : this(population)
        {
            Generation = world.Generation + 1;
        }

        public IList<Individual<G, F>> Population
        {
            get { return population.ToList(); }
        }

        public F BestFitness => BestIndividual.Fitness;
        public G BestGenotype => BestIndividual.Genotype;
        public Individual<G, F> BestIndividual { get; }

        public int Generation { get; }
    }
}