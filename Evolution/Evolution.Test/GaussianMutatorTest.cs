using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Singular.Evolution;
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
            
            GaussianMutator<int> mutator = new GaussianMutator<int>(0.1);
            var parents = new List<IListGenotype<FloatGene>>();

            parents.Add(genotype);

            IList<Individual<IListGenotype<FloatGene>, int>> individuals = Individual<IListGenotype<FloatGene>, int>.FromGenotypes(parents);

            var offspring =
                mutator.Apply(individuals);

            Assert.AreEqual(offspring.Count, 1);

            IListGenotype<FloatGene> child1 = offspring.First().Genotype;

            var changes = 0;
            for (int i = 0; i < 5; i++)
            {
                if (Math.Abs(child1[i].Value - i) > Double.Epsilon)
                    changes++;
            }
            
            Assert.LessOrEqual(changes, 1000);
            
        }
    }
}