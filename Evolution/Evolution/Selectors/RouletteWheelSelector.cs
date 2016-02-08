using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Core;

namespace Singular.Evolution.Selectors
{
    public class RouletteWheelSelector<G> : ProbabilitySelector<G, double> where G : IGenotype
    {
        
        public RouletteWheelSelector(int numberOfSelected) : base(numberOfSelected)
        {
        }

        protected override IList<IndividualScore> Score(IList<Individual<G, double>> individuals)
        {
            return individuals.Select(i => new IndividualScore(i.Fitness, i)).ToList();
        }
    }
}