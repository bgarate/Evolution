using System;
using NUnit.Framework;
using Singular.Evolution.Core;
using Singular.Evolution.Genes;

namespace Evolution.Test
{
    [TestFixture]
    public class BitGeneTest
    {
        [Test]
        public void BasicTest()
        {
            BitGene a = new BitGene();
            BitGene b = new BitGene(true);

            Assert.AreEqual(false, a.Value);
            Assert.AreEqual(true, b.Value);

            BitGene c = a.Sum(b);
            BitGene d = a.Multiply(b);
            BitGene e = a.Subtract(b);

            Assert.Throws<NotImplementedException>(() => a.Divide(b));
            Assert.AreEqual(true, c.Value);
            Assert.AreEqual(false, d.Value);
            Assert.AreEqual(true, e.Value);

            c = (BitGene) b.Clone();
            Assert.AreNotSame(c, b);
            Assert.True(c.Equals(b));

            IGene f = new BitGene(false);
            Assert.False(a.Equals(true));
            Assert.True(a.Equals(f));

            Assert.True(b.CompareTo(a) == 1);
        }
    }
}

