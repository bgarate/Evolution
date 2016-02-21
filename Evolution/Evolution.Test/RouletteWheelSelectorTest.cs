using System.Collections.Generic;
using NUnit.Framework;
using Singular.Evolution.Core;
using Singular.Evolution.Genes;
using Singular.Evolution.Genotypes;
using Singular.Evolution.Selectors;
using Singular.Evolution.Utils;

namespace Evolution.Test
{
    [TestFixture]
    public class RouletteWheelSelectorTest
    {
        [Test]
        public void BasicTest()
        {
            RandomGenerator.GetInstance().RandomSource =
                new MockRandomSource(new List<int>(), new List<double> {0.350, 0.623, 0.311, 0.511, 0.999});

            ListGenotype<FloatGene> genotype1 =
                new ListGenotype<FloatGene>(10000);
            ListGenotype<FloatGene> genotype2 =
                new ListGenotype<FloatGene>(20000);
            ListGenotype<FloatGene> genotype3 =
                new ListGenotype<FloatGene>(30000);

            List<Individual<ListGenotype<FloatGene>, double>> individuals =
                new List<Individual<ListGenotype<FloatGene>, double>>
                {
                    new Individual<ListGenotype<FloatGene>, double>(genotype1, 300),
                    new Individual<ListGenotype<FloatGene>, double>(genotype2, 200),
                    new Individual<ListGenotype<FloatGene>, double>(genotype3, 500)
                };

            RouletteWheelSelector<ListGenotype<FloatGene>> selector =
                new RouletteWheelSelector<ListGenotype<FloatGene>>(5);

            IList<Individual<ListGenotype<FloatGene>, double>> selection = selector.Apply(individuals);

            Assert.AreEqual(5, selection.Count);
            Assert.AreSame(individuals[2], selection[0]);
            Assert.AreSame(individuals[0], selection[1]);
            Assert.AreSame(individuals[2], selection[2]);
            Assert.AreSame(individuals[0], selection[3]);
            Assert.AreSame(individuals[1], selection[4]);

            Assert.AreNotSame(individuals[0], selection[0]);
            Assert.AreNotSame(individuals[1], selection[0]);
        }
    }
}