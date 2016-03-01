using System;
using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Genes;
using Singular.Evolution.Genotypes;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Breeders
{
    /// <summary>
    /// Helper methods to generate breeders
    /// </summary>
    public static class BreederGenerators
    {
        /// <summary>
        /// Returns a breeder which creates a population of genotypes with genes with the <see cref="FloatGene"/> with values between start and end sorted
        /// in random order
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="populationSize">Size of the population.</param>
        /// <returns></returns>
        public static GenotypeGeneratorBreeder<ListGenotype<FloatGene>> RangeBreeder(int start, int end,
            int populationSize)
        {
            return new GenotypeGeneratorBreeder<
                ListGenotype<FloatGene>>(
                index => new ListGenotype<FloatGene>(
                    Enumerable.Range(start, end).ToList().RandomSort().Select(i => new FloatGene(i))),
                populationSize);
        }

        /// <summary>
        /// Returns a breeder which creates a population of genotypes with random values for <see cref="FloatGene"/> optionally bounded
        /// </summary>
        /// <param name="numberOfGenes">The number of genes.</param>
        /// <param name="min">The minimum. If it is set, the gene will be bounded and its values will have this minimum</param>
        /// <param name="max">The maximum. If it is set, the gene will be bounded and its values will have this maximum</param>
        /// <param name="populationSize">Size of the population.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// </exception>
        public static GenotypeGeneratorBreeder<ListGenotype<FloatGene>> FloatBreeder(int numberOfGenes, double? min,
            double? max, int populationSize)
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
                sequence = RandomGenerator.GetInstance().DoubleSequence(max.Value, min.Value);
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