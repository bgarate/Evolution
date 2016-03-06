using System;
using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Core;

namespace Singular.Evolution.Scaling
{
    /// <summary>
    /// Represents a Linear fitness scaling
    /// </summary>
    public class LinearRankScaling : IFitnessScaling<double>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinearRankScaling"/> class.
        /// </summary>
        /// <param name="selectionPresure">The selection presure.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public LinearRankScaling(double selectionPresure)
        {
            if (selectionPresure < 1 || selectionPresure > 2)
                throw new ArgumentException($"{SelectionPresure} must be in the range [1,2]");

            SelectionPresure = selectionPresure;
        }

        /// <summary>
        /// Gets the selection presure.
        /// </summary>
        /// <value>
        /// The selection presure.
        /// </value>
        public double SelectionPresure { get; }

        /// <summary>
        /// Applies the linear rank scaling function to the list of fitneses
        /// The i-th element of the returned list matches with the i-th element of the input list
        /// </summary>
        /// <param name="originalFitneses"></param>
        /// <returns></returns>
        public List<double> Scale(List<double> originalFitneses)
        {
            List<double> sorted = originalFitneses.ToList();
            sorted.Sort();

            int count = originalFitneses.Count;

            return
                originalFitneses.Select(o => sorted.BinarySearch(o))
                    .Select(pos => 2 - SelectionPresure + 2*(SelectionPresure - 1)*(pos - 1)/(count - 1))
                    .ToList();
        }
    }
}