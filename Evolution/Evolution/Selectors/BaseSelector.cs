using System;
using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Core;

namespace Singular.Evolution.Selectors
{
    public abstract class BaseSelector<G, F> : ISelector<G, F> where G : IGenotype where F : IComparable<F>
    {
        protected BaseSelector(int numberOfSelected, IFitnessScaling<F> scaling = null)
        {
            NumberOfSelected = numberOfSelected;
        }

        public int NumberOfSelected { get; }
        
        public IList<Individual<G, F>> Apply(IList<Individual<G, F>> individuals)
        {
            IList<IndividualScore> scoredIndividuals = Score(individuals);
            return Select(scoredIndividuals);
        }

        protected abstract IList<Individual<G, F>> Select(IList<IndividualScore> scoredIndividuals);
        
        private IList<IndividualScore> Score(IList<Individual<G, F>> individuals)
        {
            List<F> originalScores = individuals.Select(s => s.Fitness).ToList();
            List<F> newScores = Scaling?.Scale(originalScores) ?? originalScores;

            return individuals.Select((individual, index) => new IndividualScore(newScores[index], individual)).ToList();
        }

        protected class IndividualScore
        {

            public IndividualScore(F score, Individual<G, F> individual)
            {
                Score = score;
                Individual = individual;
            }

            public F Score { get; }
            public Individual<G, F> Individual { get; }
        }

        public IFitnessScaling<F> Scaling { get; }
    }
}