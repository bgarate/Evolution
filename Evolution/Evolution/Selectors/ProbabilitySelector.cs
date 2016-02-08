using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Singular.Evolution.Core;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Selectors
{
    public abstract class ProbabilitySelector<G, F> : ISelector<G, F> where G : IGenotype where F : IComparable<F>
    {
        protected ProbabilitySelector(int numberOfSelected)
        {
            NumberOfSelected = numberOfSelected;
        }

        public int NumberOfSelected { get; }

        private List<IndividualScore> sortedIndividuals;
        private List<double> sortedScores;
        private double sum;

        public IList<Individual<G, F>> Apply(IList<Individual<G, F>> individuals)
        {
            SortIndividuals(individuals);

            IList<Individual<G, F>> selection = new List<Individual<G, F>>();

            for (int i = 0; i < NumberOfSelected; i++)
            {
                selection.Add(SelectIndividual());
            }

            return selection;

        }

        private Individual<G, F> SelectIndividual()
        {
            double next = RandomGenerator.GetInstance().NextDouble(0, sum);
            int index = sortedScores.BinarySearch(next);

            int selectionIndex = index > 0 ? index : ~index;

            return sortedIndividuals[selectionIndex].Individual;
        }

        private void SortIndividuals(IList<Individual<G, F>> individuals)
        {
            sortedIndividuals = new List<IndividualScore>(Score(individuals));
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

        protected abstract IList<IndividualScore> Score(IList<Individual<G, F>> individuals);

        protected class IndividualScore
        {

            public IndividualScore(double score, Individual<G, F> individual)
            {
                Score = score;
                Individual = individual;
            }

            public double Score { get; }
            public Individual<G, F> Individual { get; }
        }
    }
}