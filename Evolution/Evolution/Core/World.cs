using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Singular.Evolution.Core
{
    public class World<G, F> where G : IGenotype where F : IComparable<F>
    {
        private IList<Individual<G, F>> population;

        public IList<Individual<G, F>> Population
        {
            get { return population; }
            private set { population = value; }
        }

        public World(IList<Individual<G, F>> population)
        {
            Population = population;
        }

        public World(World<G, F> world, IList<Individual<G, F>> population)
        {
            Population = population;
            Generation = world.Generation + 1;
        }

        public int Generation { get; }

    }

}
