using System;
using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Core;

namespace Singular.Evolution.Selectors
{
    /// <summary>
    /// Represents a base class for selectors.
    /// It applies a scaling function if defined over the input individual's scores before
    /// passing them to the <see cref="Select"/> abstract method.
    /// </summary>
    /// <typeparam name="G"></typeparam>
    /// <typeparam name="F"></typeparam>
    /// <seealso cref="Singular.Evolution.Core.ISelector{G, F}" />
    public abstract class BaseSelector<G, F> : ISelector<G, F> where G : IGenotype where F : IComparable<F>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSelector{G, F}"/> class.
        /// </summary>
        /// <param name="selectionSize">The number of selected.</param>
        /// <param name="scaling">The scaling.</param>
        protected BaseSelector(int selectionSize, IFitnessScaling<F> scaling = null)
        {
            SelectionSize = selectionSize;
        }

        /// <summary>
        /// Gets the size of the selection.
        /// </summary>
        /// <value>
        /// The size of the selection.
        /// </value>
        public int SelectionSize { get; }

        /// <summary>
        /// Returns the selected individuals.
        /// </summary>
        /// <param name="individuals">Input individuals</param>
        /// <returns>
        /// Selected individuals
        /// </returns>
        public IList<Individual<G, F>> Apply(IList<Individual<G, F>> individuals)
        {
            IList<IndividualScore> scoredIndividuals = Score(individuals);
            return Select(scoredIndividuals);
        }

        /// <summary>
        /// The fitness scaling that is performed to the fitness of individuals to take into account on selection
        /// </summary>
        /// <value>
        /// The scaling.
        /// </value>
        public IFitnessScaling<F> Scaling { get; }

        /// <summary>
        /// Selects from the individuals whose fitness has been previously scaled.
        /// </summary>
        /// <param name="scoredIndividuals">The input individuals.</param>
        /// <returns>Selected individuals</returns>
        protected abstract IList<Individual<G, F>> Select(IList<IndividualScore> scoredIndividuals);
        
        private IList<IndividualScore> Score(IList<Individual<G, F>> individuals)
        {
            List<F> originalScores = individuals.Select(s => s.Fitness).ToList();
            List<F> newScores = Scaling?.Scale(originalScores) ?? originalScores;

            return individuals.Select((individual, index) => new IndividualScore(newScores[index], individual)).ToList();
        }

        /// <summary>
        /// Represents an individual and its scaled fitness
        /// </summary>
        /// <seealso cref="Singular.Evolution.Core.ISelector{G, F}" />
        protected class IndividualScore
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="IndividualScore"/> class.
            /// </summary>
            /// <param name="score">The score.</param>
            /// <param name="individual">The individual.</param>
            public IndividualScore(F score, Individual<G, F> individual)
            {
                Score = score;
                Individual = individual;
            }

            /// <summary>
            /// Gets the score.
            /// </summary>
            /// <value>
            /// The score.
            /// </value>
            public F Score { get; }

            /// <summary>
            /// Gets the individual.
            /// </summary>
            /// <value>
            /// The individual.
            /// </value>
            public Individual<G, F> Individual { get; }
        }
    }
}