using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Core;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Selectors
{
    /// <summary>
    /// Represents a Roulette Wheel Selector, also called Fitness Proportionate Selection
    /// </summary>
    /// <typeparam name="G"></typeparam>
    public class RouletteWheelSelector<G> : BaseSelector<G, double> where G : IGenotype
    {
        private List<IndividualScore> sortedIndividuals;
        private List<double> sortedScores;
        private double sum;

        /// <summary>
        /// Initializes a new instance of the <see cref="RouletteWheelSelector{G}"/> class.
        /// </summary>
        /// <param name="selectionSize">The number of selected.</param>
        /// <param name="scaling">The scaling.</param>
        public RouletteWheelSelector(int selectionSize, IFitnessScaling<double> scaling = null)
            : base(selectionSize, scaling)
        {

        }

        /// <summary>
        /// Selects from the individuals whose fitness has been previously scaled.
        /// </summary>
        /// <param name="scoredIndividuals">The input individuals.</param>
        /// <returns>
        /// Selected individuals
        /// </returns>
        protected override IList<Individual<G, double>> Select(IList<IndividualScore> scoredIndividuals)
        {
            SortIndividuals(scoredIndividuals);

            IList<Individual<G, double>> selection = new List<Individual<G, double>>();

            for (int i = 0; i < SelectionSize; i++)
            {
                selection.Add(SelectIndividual());
            }

            return selection;
        }

        private Individual<G, double> SelectIndividual()
        {
            double next = RandomGenerator.GetInstance().NextDouble(0, sum);
            int index = sortedScores.BinarySearch(next);

            int selectionIndex = index > 0 ? index : ~index;

            return sortedIndividuals[selectionIndex].Individual;
        }

        private void SortIndividuals(IList<IndividualScore> individuals)
        {
            sortedIndividuals = new List<IndividualScore>(individuals);
            sortedIndividuals.Sort((i1, i2) => -i1.Score.CompareTo(i2.Score));
            sortedScores = new List<double>();

            double sumUpToLastScore = 0;
            foreach (IndividualScore individual in sortedIndividuals)
            {
                sumUpToLastScore += individual.Score;
                sortedScores.Add(sumUpToLastScore);
            }

            sum = sumUpToLastScore;
        }
        
    }
}