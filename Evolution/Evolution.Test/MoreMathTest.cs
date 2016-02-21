using NUnit.Framework;
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
            Assert.AreEqual(2, MathHelper.Clamp(2, -1, 5));
            Assert.AreEqual(-1, MathHelper.Clamp(-2, -1, 5));
            Assert.AreEqual(5, MathHelper.Clamp(15, -1, 5));

            Assert.AreEqual(new FloatGene(4), MathHelper.Clamp(new FloatGene(4), new FloatGene(3), new FloatGene(15)));
            Assert.AreEqual(new FloatGene(15),
                MathHelper.Clamp(new FloatGene(23.5), new FloatGene(3), new FloatGene(15)));
            Assert.AreEqual(new FloatGene(3), MathHelper.Clamp(new FloatGene(0.3), new FloatGene(3), new FloatGene(15)));

            Assert.True(Equals(2, MathHelper.Clamp(2, -1, 5)));
            Assert.True(Equals(-1, MathHelper.Clamp(-2, -1, 5)));
            Assert.True(Equals(5, MathHelper.Clamp(15, -1, 5)));

            Assert.True(Equals(new FloatGene(4), MathHelper.Clamp(new FloatGene(4), new FloatGene(3), new FloatGene(15))));
            Assert.True(Equals(new FloatGene(15),
                MathHelper.Clamp(new FloatGene(23.5), new FloatGene(3), new FloatGene(15))));
            Assert.True(Equals(new FloatGene(3),
                MathHelper.Clamp(new FloatGene(0.3), new FloatGene(3), new FloatGene(15))));
        }
    }
}