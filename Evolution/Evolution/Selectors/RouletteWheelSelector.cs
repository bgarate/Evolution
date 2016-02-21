using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Core;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Selectors
{
    public class RouletteWheelSelector<G> : BaseSelector<G, double> where G : IGenotype
    {
        private List<IndividualScore> sortedIndividuals;
        private List<double> sortedScores;
        private double sum;

        public RouletteWheelSelector(int numberOfSelected, IFitnessScaling<double> scaling = null)
            : base(numberOfSelected, scaling)
        {
        }

        public IFitnessScaling<double> Scaling { get; }

        protected override IList<Individual<G, double>> Select(IList<IndividualScore> scoredIndividuals)
        {
            SortIndividuals(scoredIndividuals);

            IList<Individual<G, double>> selection = new List<Individual<G, double>>();

            for (int i = 0; i < NumberOfSelected; i++)
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

        private IList<IndividualScore> Score(IList<Individual<G, double>> individuals)
        {
            List<double> originalScores = individuals.Select(s => s.Fitness).ToList();
            List<double> newScores = Scaling?.Scale(originalScores) ?? originalScores;

            return individuals.Select((individual, index) => new IndividualScore(newScores[index], individual)).ToList();
        }
    }
}