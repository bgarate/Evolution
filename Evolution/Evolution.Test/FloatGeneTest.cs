using NUnit.Framework;
using Singular.Evolution.Genes;

namespace Evolution.Test
{
    [TestFixture]
    public class FloatGeneTest
    {
        [Test]
        public void BasicTest()
        {
            FloatGene a = new FloatGene();

            Assert.AreEqual(0, a.Value);

            a = new FloatGene(2);

            Assert.False(a.IsBounded);

            Assert.AreEqual(2, a.Value);

            a = new FloatGene(5, 2, -1000000);

            Assert.False(a.IsValid);
            Assert.True(a.IsBounded);

            FloatGene b = new FloatGene(-3, 1000000, 0);

            Assert.False(b.IsValid);
            Assert.True(b.IsBounded);

            FloatGene c = a.Sum(b);

            Assert.True(c.IsValid);
            Assert.True(c.IsBounded);
            Assert.AreEqual(0, c.MinValue);
            Assert.AreEqual(2, c.MaxValue);
            Assert.AreEqual(2, c.Value);

            Assert.AreEqual(5, a.Value);
            Assert.AreEqual(-3, b.Value);

            Assert.AreEqual(-1000000, a.MinValue);
            Assert.AreEqual(0, b.MinValue);

            Assert.AreEqual(2, a.MaxValue);
            Assert.AreEqual(1000000, b.MaxValue);

            a = new FloatGene(3, 4, 2);
            b = new FloatGene(4);

            c = FloatGene.MinimumBoundedGene(0, a, b);

            Assert.AreEqual(0, c.Value);
            Assert.AreEqual(2, c.MinValue);
            Assert.AreEqual(4, c.MaxValue);
            Assert.False(c.IsValid);

            c = a.Subtract(b);

            Assert.AreEqual(-1, c.Value);
            Assert.AreEqual(2, c.MinValue);
            Assert.AreEqual(4, c.MaxValue);
            Assert.False(c.IsValid);

            c = a.Divide(b);

            Assert.AreEqual(0.75, c.Value);
            Assert.AreEqual(2, c.MinValue);
            Assert.AreEqual(4, c.MaxValue);
            Assert.False(c.IsValid);

            c = a.Multiply(b);

            Assert.AreEqual(12, c.Value);
            Assert.AreEqual(2, c.MinValue);
            Assert.AreEqual(4, c.MaxValue);
            Assert.False(c.IsValid);

            a = new FloatGene(1, 0, 3);
            b = new FloatGene(1);

            Assert.AreNotEqual(a, b);
        }
    }
}