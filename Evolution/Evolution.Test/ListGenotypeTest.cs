using System.Linq;
using NUnit.Framework;
using Singular.Evolution.Genes;
using Singular.Evolution.Genotypes;

namespace Evolution.Test
{
    [TestFixture]
    public class ListGenotypeTest
    {
        [Test]
        public void TestClone()
        {
            ListGenotype<FloatGene> genotype = new ListGenotype<FloatGene>(5);

            ListGenotype<FloatGene> genotype2 = genotype.Clone();

            Assert.IsTrue(genotype.All(g1 => genotype2.All(g2 => !ReferenceEquals(g1, g2) && g1.Equals(g2))));
        }

        [Test]
        public void TestConstructors()
        {
            ListGenotype<FloatGene> genotype = new ListGenotype<FloatGene>(5);

            Assert.AreEqual(genotype.Count, 5);

            ListGenotype<FloatGene> genotype2 = new ListGenotype<FloatGene>(genotype);

            Assert.AreEqual(genotype2.Count, 5);

            Assert.IsTrue(genotype.All(g1 => genotype2.All(g2 => !ReferenceEquals(g1, g2))));
        }
    }
}