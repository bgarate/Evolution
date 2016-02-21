using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Singular.Evolution.Alterers;
using Singular.Evolution.Core;
using Singular.Evolution.Genes;
using Singular.Evolution.Genotypes;
using Singular.Evolution.Utils;

namespace Evolution.Test
{
    [TestFixture]
    public class GaussianMutatorTest
    {
        [Test]
        public void TestGaussianMutator()
        {
            RandomGenerator rnd = RandomGenerator.GetInstance();

            ListGenotype<FloatGene> genotype =
                new ListGenotype<FloatGene>(10000);

            GaussianMutator<ListGenotype<FloatGene>, int> mutator =
                new GaussianMutator<ListGenotype<FloatGene>, int>(0.1);
            List<ListGenotype<FloatGene>> parents = new List<ListGenotype<FloatGene>>();

            parents.Add(genotype);

            IList<Individual<ListGenotype<FloatGene>, int>> individuals =
                Individual<ListGenotype<FloatGene>, int>.FromGenotypes(parents);

            IList<Individual<ListGenotype<FloatGene>, int>> offspring =
                mutator.Apply(individuals);

            Assert.AreEqual(offspring.Count, 1);

            ListGenotype<FloatGene> child1 = offspring.First().Genotype;

            int changes = 0;
            for (int i = 0; i < 5; i++)
            {
                if (Math.Abs(child1[i].Value - i) > double.Epsilon)
                    changes++;
            }

            Assert.LessOrEqual(changes, 1000);
        }
    }
}