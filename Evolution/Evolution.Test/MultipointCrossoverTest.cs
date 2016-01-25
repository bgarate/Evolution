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
            rnd.MockSource = new double[] { 1, 1, 2, 3, 4,5,6,7,8 };
            rnd.MockEnabled = true;

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

            IListGenotype<FloatGene> child1 = offspring[0].Genotype;
            IListGenotype<FloatGene> child2 = offspring[1].Genotype;

            Assert.AreEqual(child1[0].Value, 1);
            Assert.AreEqual(child1[1].Value, 2.5);
            Assert.AreEqual(child1[2].Value, 3);
            Assert.AreEqual(child1[3].Value, 4.5);
            Assert.AreEqual(child1[4].Value, 5.5);

            Assert.AreEqual(child2[0].Value, 1.5);
            Assert.AreEqual(child2[1].Value, 2);
            Assert.AreEqual(child2[2].Value, 3.5);
            Assert.AreEqual(child2[3].Value, 4);
            Assert.AreEqual(child2[4].Value, 5);
        }
    }
}