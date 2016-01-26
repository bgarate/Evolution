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
    public class MultipointCrossOverTest
    {
        [Test]
        public void TestMultipointCrossover()
        {
            RandomGenerator rnd = RandomGenerator.GetInstance();

            ListGenotype<FloatGene> genotype =
                new ListGenotype<FloatGene>(new[]
                {new FloatGene(1), new FloatGene(2), new FloatGene(3), new FloatGene(4), new FloatGene(5)});

            ListGenotype<FloatGene> genotype2 =
                new ListGenotype<FloatGene>(new[]
                {new FloatGene(1.5), new FloatGene(2.5), new FloatGene(3.5), new FloatGene(4.5), new FloatGene(5.5)});

            MultipointCrossover<FloatGene,int> crossover = new MultipointCrossover<FloatGene,int>(3);

            List<Individual<IListGenotype<FloatGene>,int>> offspring =
                crossover.Apply(
                    Individual<IListGenotype<FloatGene>, int>.FromGenotypes(new List<IListGenotype<FloatGene>>()
                    {
                        genotype,
                        genotype2
                    })).ToList();

            Assert.AreEqual(offspring.Count, 2);

            foreach (Individual<IListGenotype<FloatGene>, int> child in offspring)
            {
                bool previousWasInteger = Math.Abs(child.Genotype[0].Value - (int)child.Genotype[0].Value) < Double.Epsilon;
                int cuts = 0;
                foreach (FloatGene gene in child.Genotype)
                {
                    bool currentIsInteger = Math.Abs(gene.Value - (int) gene.Value) < Double.Epsilon;
                    if (currentIsInteger != previousWasInteger)
                        cuts++;
                    previousWasInteger = currentIsInteger;
                }
                Assert.AreEqual(3,cuts);
            }
        }
    }
}