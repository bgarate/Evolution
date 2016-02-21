using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Singular.Evolution.Breeders;
using Singular.Evolution.Genes;
using Singular.Evolution.Genotypes;

namespace Evolution.Test
{
    [TestFixture]
    public class BreedersTest
    {
        [Test]
        public void FloatBreederTest()
        {
            GenotypeGeneratorBreeder<ListGenotype<FloatGene>> breeder1 = BreederGenerators.FloatBreeder(5, -3, 5, 50);
            GenotypeGeneratorBreeder<ListGenotype<FloatGene>> breeder2 = BreederGenerators.FloatBreeder(15, null, null,
                500);

            Assert.Throws<ArgumentException>(() => BreederGenerators.FloatBreeder(15, null, 10, 500));
            Assert.Throws<ArgumentException>(() => BreederGenerators.FloatBreeder(15, 5, 3, 500));
            Assert.Throws<ArgumentException>(() => BreederGenerators.FloatBreeder(-15, null, null, 500));
            Assert.Throws<ArgumentException>(() => BreederGenerators.FloatBreeder(15, null, null, -500));
            Assert.Throws<ArgumentException>(() => BreederGenerators.FloatBreeder(0, null, 10, 500));

            IList<ListGenotype<FloatGene>> genotypes1 = breeder1.Breed();
            IList<ListGenotype<FloatGene>> genotypes2 = breeder2.Breed();

            Assert.AreEqual(50, genotypes1.Count);
            foreach (ListGenotype<FloatGene> genotype in genotypes1)
            {
                Assert.AreEqual(5, genotype.Count);
                Assert.True(genotype.All(g => g.Value >= -3 && g.Value <= 5 && g.MinValue == -3 && g.MaxValue == 5));
            }

            Assert.AreEqual(500, genotypes2.Count);
            foreach (ListGenotype<FloatGene> genotype in genotypes2)
            {
                Assert.AreEqual(15, genotype.Count);
                Assert.True(genotype.All(g => g.Value >= 0 && !g.IsBounded));
            }
        }

        [Test]
        public void GenotypeGeneratorBreederWithIndexTest()
        {
            GenotypeGeneratorBreeder<ListGenotype<FloatGene>>.BreederDelegateWithIndex withIndex =
                index => new ListGenotype<FloatGene>(
                    Enumerable.Range(0, 200).ToList().Select(i => new FloatGene(index*i)));

            GenotypeGeneratorBreeder<ListGenotype<FloatGene>> breederWithIndex =
                new GenotypeGeneratorBreeder<ListGenotype<FloatGene>>(withIndex, 500);

            IList<ListGenotype<FloatGene>> genotypesWithIndex = breederWithIndex.Breed();

            Assert.AreEqual(500, genotypesWithIndex.Count);

            for (int i = 0; i < genotypesWithIndex.Count; i++)
            {
                Assert.AreEqual(200, genotypesWithIndex[i].Count);

                for (int j = 0; j < genotypesWithIndex[i].Count; j++)
                {
                    Assert.AreEqual(i*j, genotypesWithIndex[i][j].Value);
                }
            }
        }

        [Test]
        public void GenotypeGeneratorBreederWithoutIndexTest()
        {
            GenotypeGeneratorBreeder<ListGenotype<FloatGene>>.BreederDelegateWithoutIndex withoutIndex =
                () => new ListGenotype<FloatGene>(
                    Enumerable.Range(0, 10).ToList().Select(i => new FloatGene(i)));

            GenotypeGeneratorBreeder<ListGenotype<FloatGene>> breederWithoutIndex =
                new GenotypeGeneratorBreeder<ListGenotype<FloatGene>>(withoutIndex, 100);

            IList<ListGenotype<FloatGene>> genotypesWithoutIndex = breederWithoutIndex.Breed();

            Assert.AreEqual(100, genotypesWithoutIndex.Count);

            foreach (ListGenotype<FloatGene> genotype in genotypesWithoutIndex)
            {
                Assert.AreEqual(10, genotype.Count);

                for (int j = 0; j < genotype.Count; j++)
                {
                    Assert.AreEqual(j, genotype[j].Value);
                }
            }
        }
    }
}