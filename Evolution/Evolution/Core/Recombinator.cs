using System;
using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Genes;
using Singular.Evolution.Genotypes;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Core
{
    public class Recombinator<G, F> : IAlterer<G, F> where F : IComparable<F> where G : IGenotype

    {
        public Recombinator(IAlterer<G, F> alterer, int numberOfParents, int numberOfRecombinations)
        {
            Alterer = alterer;
            NumberOfRecombinations = numberOfRecombinations;
            NumberOfParents = numberOfParents;
        }

        public IAlterer<G, F> Alterer { get; }
        public int NumberOfRecombinations { get; }
        public int NumberOfParents { get; }

        public IList<Individual<G, F>> Apply(IList<Individual<G, F>> individuals)
        {
            List<Individual<G, F>> offspring = new List<Individual<G, F>>();

            for (int i = 0; i < NumberOfRecombinations; i++)
            {
                IList<Individual<G, F>> parents = individuals.RandomTake(NumberOfParents).ToList();
                offspring.AddRange(Alterer.Apply(parents));
            }

            return offspring;
        }

        //}
        //    return Apply((IList<Individual<G, F>>) individuals);
        //{

            //public object Apply(object individuals)
    }
}