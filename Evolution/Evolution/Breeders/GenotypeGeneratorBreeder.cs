using System;
using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Core;
using Singular.Evolution.Genes;
using Singular.Evolution.Genotypes;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Breeders
{
    public class GenotypeGeneratorBreeder<G> : IBreeder<G> where G : IGenotype
    {
        public delegate G BreederDelegateWithIndex(int i);
        public delegate G BreederDelegateWithoutIndex();

        public BreederDelegateWithIndex DelegateWithIndex { get; }
        public BreederDelegateWithoutIndex DelegateWithoutIndex { get; }

        public int PopulationSize { get; }
        
        private GenotypeGeneratorBreeder(int populationSize)
        {
            PopulationSize = populationSize;
        }

        public GenotypeGeneratorBreeder(BreederDelegateWithIndex breederDelegate, int populationSize)
            : this(populationSize)
        {
            DelegateWithIndex = breederDelegate;
            DelegateWithoutIndex = null;
        }

        public GenotypeGeneratorBreeder(BreederDelegateWithoutIndex breederDelegate, int populationSize)
            : this(populationSize)
        {
            DelegateWithoutIndex = breederDelegate;
            DelegateWithIndex = null;
        }

        public IList<G> Breed()
        {
            List<G> population = new List<G>();
            for (int i = 0; i < PopulationSize; i++)
            {
                G genotype = DelegateWithIndex != null ? DelegateWithIndex(i) : DelegateWithoutIndex();
                population.Add(genotype);
            }
            return population;
        }
    }

    public static class BreederGenerators
    {
        public static GenotypeGeneratorBreeder<ListGenotype<FloatGene>> RangeBreeder(int start, int end, int populationSize)
        {
            return new GenotypeGeneratorBreeder<
                ListGenotype<FloatGene>>(
                index => new ListGenotype<FloatGene>(
                    Enumerable.Range(start, end).ToList().RandomSort().Select(i => new FloatGene(i))),
                populationSize);

        }

        public static GenotypeGeneratorBreeder<ListGenotype<FloatGene>> FloatBreeder(int numberOfGenes, double? min, double? max, int populationSize)
        {
            IEnumerable<double> sequence;

            if (min.HasValue != max.HasValue)
                throw new ArgumentException(Resources.Both_or_none_bounds_must_be_null);

            if (numberOfGenes <= 0)
                throw new ArgumentException($"Must have a non-zero positive {nameof(numberOfGenes)}");

            if (populationSize < 0)
                throw new ArgumentException($"Must have a positive {nameof(populationSize)}");

            if (min.GetValueOrDefault() > max.GetValueOrDefault())
                throw new ArgumentException($"{nameof(max)} must be greater or equal than {nameof(min)}");

            if (min.HasValue)
                sequence = RandomGenerator.GetInstance().DoubleSequence(max.Value,min.Value);
            else
                sequence = RandomGenerator.GetInstance().DoubleSequence();

            return new GenotypeGeneratorBreeder<
                ListGenotype<FloatGene>>(
                () => new ListGenotype<FloatGene>(
                    sequence.Take(numberOfGenes).Select(i => new FloatGene(i, max, min))),
                populationSize);

        }
    }
}