using System;
using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Core
{
    public abstract class Recombinator<A, F> : IAlterer<IGenotype,F> where A:IAlterer<IGenotype,F> where F : IComparable<F>

    {
        public Recombinator(IAlterer<IGenotype, F> alterer, int numberOfRecombinations, int numberOfParents)
        {
            Alterer = alterer;
            NumberOfRecombinations = numberOfRecombinations;
            NumberOfParents = numberOfParents;
        }

        public IAlterer<IGenotype, F> Alterer { get;}
        public int NumberOfRecombinations { get; }
        public int NumberOfParents { get; }

        public IList<Individual<IGenotype, F>> Apply(IList<Individual<IGenotype, F>> individuals)
        {
            List<Individual<IGenotype,F>> offspring = new List<Individual<IGenotype, F>>();

            for (int i = 0; i < NumberOfRecombinations; i++)
            {
                IList<Individual<IGenotype, F>> parents = individuals.RandomTake(NumberOfParents).ToList();
                offspring.AddRange(Alterer.Apply(parents));
            }

            return offspring;
        }
    }
}