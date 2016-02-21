using System;
using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Alterers;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Core
{
    public class Recombinator<G, F> : IAlterer<G, F> where F : IComparable<F> where G : IGenotype

    {
        public enum RecombinatioNumberType
        {
            Absolute,
            Probability
        }

        public Recombinator(IAlterer<G, F> alterer, int numberOfParents, double numberOfRecombinations,
            RecombinatioNumberType recombinationType)
        {
            Alterer = alterer;
            NumberOfRecombinations = numberOfRecombinations;
            NumberOfParents = numberOfParents;
            RecombinationType = recombinationType;

            if ((recombinationType == RecombinatioNumberType.Probability) &&
                !MathHelper.IsProbabilty(numberOfRecombinations))
                throw new ArgumentException($"The value of {numberOfRecombinations} must in the range [0,1]");

            if ((recombinationType == RecombinatioNumberType.Absolute) && !MathHelper.IsInteger(numberOfRecombinations))
                throw new ArgumentException($"The value of {numberOfRecombinations} must be an integer");
        }

        public IAlterer<G, F> Alterer { get; }
        public double NumberOfRecombinations { get; }
        public int NumberOfParents { get; }
        public RecombinatioNumberType RecombinationType { get; set; }

        public IList<Individual<G, F>> Apply(IList<Individual<G, F>> individuals)
        {
            List<Individual<G, F>> offspring = new List<Individual<G, F>>();

            int recombinations =
                (int)
                    (NumberOfRecombinations*
                     (RecombinationType == RecombinatioNumberType.Probability ? individuals.Count : 1));

            for (int i = 0; i < recombinations; i++)
            {
                IList<Individual<G, F>> parents = individuals.RandomTake(NumberOfParents).ToList();
                offspring.AddRange(Alterer.Apply(parents));
            }

            return offspring;
        }

        public static Recombinator<G2, F> FromCrossover<G2, R>(CrossoverBase<G2, R, F> crossover,
            double numberOfRecombinations, Recombinator<G2, F>.RecombinatioNumberType recombinatioType)
            where R : IGene, new()
            where G2 : IListGenotype<G2, R>
        {
            return new Recombinator<G2, F>(crossover, crossover.NumberOfParentsNeeded, numberOfRecombinations,
                recombinatioType);
        }
    }
}