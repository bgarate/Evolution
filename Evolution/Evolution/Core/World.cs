using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Singular.Evolution.Core
{
    public class World<G,F> where G:IGenotype where F: IComparable<F>
    {
        private IList<Individual<G, F>> population;

        public IList<Individual<G, F>> Population
        {
            get { return population.Select(i => i.Clone()).ToList(); }
            private set { population = value; }
        }

        public World(int generation, IList<Individual<G,F>> population)
        {
            Generation = generation;
            Population = population;
        }

        public int Generation { get; }


    }
}
