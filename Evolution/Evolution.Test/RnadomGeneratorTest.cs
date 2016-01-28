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
    public class RnadomGeneratorTest
    {
        [Test]
        public void TestInt()
        {
            RandomGenerator rnd = RandomGenerator.GetInstance();


            for(int i = 0; i<10000;i++)
            {
                int a = rnd.NextInt();
                Assert.True(a >= 0);
            }

            for (int i = 0; i < 10000; i++)
            {
                int c = rnd.NextInt() + 1;
                int a = rnd.NextInt(c);
                Assert.True(a >= 0 && a < c);
            }

            for (int i = 0; i < 10000; i++)
            {
                int b = rnd.NextInt() + 1;
                int c = rnd.NextInt();
                int a = rnd.NextInt(Math.Min(b,c), Math.Max(b,c));
                Assert.True(a >= Math.Min(b, c) && a < Math.Max(b, c));
            }

        }

        [Test]
        public void TestDouble()
        {
            RandomGenerator rnd = RandomGenerator.GetInstance();
            
            for (int i = 0; i < 10000; i++)
            {
                double a = rnd.NextDouble();
                Assert.True(a >= 0 && a <= 1);
            }

            for (int i = 0; i < 10000; i++)
            {
                double c = rnd.NextDouble() + 1;
                double a = rnd.NextDouble(1/c);
                Assert.True(a >= 0 && a <= 1/c);
            }

            for (int i = 0; i < 10000; i++)
            {
                double b = rnd.NextDouble();
                double c = rnd.NextDouble();
                double a = rnd.NextDouble(Math.Min(1/b, 1/c), Math.Max(1/b, 1/c));
                Assert.True(a >= Math.Min(1/b, 1/c) && a <= Math.Max(1/b, 1/c));
            }
        }
    }
}