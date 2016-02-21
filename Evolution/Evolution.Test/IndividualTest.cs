using System;
using System.Collections.Generic;
using NUnit.Framework;
using Singular.Evolution.Core;
using Singular.Evolution.Genes;
using Singular.Evolution.Genotypes;

namespace Evolution.Test
{
    [TestFixture]
    public class IndividualTest
    {
        [Test]
        public void TestIndividualConstructors()
        {
            ListGenotype<FloatGene> genotype1 = new ListGenotype<FloatGene>(new[] {new FloatGene(1)});
            ListGenotype<FloatGene> genotype2 = new ListGenotype<FloatGene>(new[] {new FloatGene(2)});

            Individual<ListGenotype<FloatGene>, int> individual1 =
                new Individual<ListGenotype<FloatGene>, int>(genotype1);
            Individual<ListGenotype<FloatGene>, int> individual2 =
                new Individual<ListGenotype<FloatGene>, int>(genotype2, 30);

            Assert.AreEqual(1, individual1.Genotype.Count);
            Assert.AreEqual(1, individual2.Genotype.Count);

            Assert.False(individual1.HasFitnessAssigned);
            Assert.True(individual2.HasFitnessAssigned);

            int a;
            Assert.Throws<InvalidOperationException>(() => a = individual1.Fitness);
            Assert.AreEqual(30, individual2.Fitness);

            ListGenotype<FloatGene> genotype3 = individual1.Genotype;
            Assert.AreSame(individual1.Genotype, genotype3);

            IList<Individual<ListGenotype<FloatGene>, int>> individuals =
                Individual<ListGenotype<FloatGene>, int>.FromGenotypes(new[] {genotype1, genotype2, genotype3});

            Assert.AreSame(individuals[0].Genotype, genotype1);
            Assert.AreSame(individuals[1].Genotype, genotype2);
            Assert.AreSame(individuals[2].Genotype, genotype3);
        }
    }
}