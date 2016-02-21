using System;
using System.Collections.Generic;
using System.Linq;

namespace Singular.Evolution.Core
{
    public class LinearRankScaling : IFitnessScaling<double>
    {
        public LinearRankScaling(double selectionPresure)
        {
            if (selectionPresure < 1 || selectionPresure > 2)
                throw new ArgumentException($"{SelectionPresure} must be in the range [1,2]");

            SelectionPresure = selectionPresure;
        }

        public double SelectionPresure { get; }

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