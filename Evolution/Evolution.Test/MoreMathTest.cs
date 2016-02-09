using System;
using NUnit.Framework;
using Singular.Evolution.Core;
using Singular.Evolution.Genes;
using Singular.Evolution.Utils;

namespace Evolution.Test
{
    [TestFixture]
    public class MoreMathTest
    {
        [Test]
        public void ClampTest()
        {
            Assert.AreEqual(2,MoreMath.Clamp(2,-1,5));
            Assert.AreEqual(-1, MoreMath.Clamp(-2, -1, 5));
            Assert.AreEqual(5, MoreMath.Clamp(15, -1, 5));

            Assert.AreEqual(new FloatGene(4), MoreMath.Clamp(new FloatGene(4), new FloatGene(3), new FloatGene(15)));
            Assert.AreEqual(new FloatGene(15), MoreMath.Clamp(new FloatGene(23.5), new FloatGene(3), new FloatGene(15)));
            Assert.AreEqual(new FloatGene(3), MoreMath.Clamp(new FloatGene(0.3), new FloatGene(3), new FloatGene(15)));

            Assert.True(Equals(2, MoreMath.Clamp(2, -1, 5)));
            Assert.True(Equals(-1, MoreMath.Clamp(-2, -1, 5)));
            Assert.True(Equals(5, MoreMath.Clamp(15, -1, 5)));

            Assert.True(Equals(new FloatGene(4), MoreMath.Clamp(new FloatGene(4), new FloatGene(3), new FloatGene(15))));
            Assert.True(Equals(new FloatGene(15), MoreMath.Clamp(new FloatGene(23.5), new FloatGene(3), new FloatGene(15))));
            Assert.True(Equals(new FloatGene(3), MoreMath.Clamp(new FloatGene(0.3), new FloatGene(3), new FloatGene(15))));
        }
    }
}

