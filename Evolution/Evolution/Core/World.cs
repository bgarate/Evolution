using System;
using System.Collections.Generic;

namespace Singular.Evolution.Core
{
    public class World<G, F> where G : IGenotype where F : IComparable<F>
    {
        public World(IList<Individual<G, F>> population)
        {
            Population = population;
        }

        public World(World<G, F> world, IList<Individual<G, F>> population)
        {
            Population = population;
            Generation = world.Generation + 1;
        }

        public IList<Individual<G, F>> Population { get; private set; }

        public int Generation { get; }
    }
}