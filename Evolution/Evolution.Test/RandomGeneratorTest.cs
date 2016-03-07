using System;
using System.Collections.Generic;
using System.Linq;
using Accord.Statistics.Testing;
using MathNet.Numerics.Statistics;
using NUnit.Framework;
using Singular.Evolution.Utils;

namespace Evolution.Test
{
    [TestFixture]
    public class RandomGeneratorTest
    {
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

        [Test]
        public void TestDoubleSequences()
        {
            RandomGenerator rnd = RandomGenerator.GetInstance();

            rnd.RandomSource = new SystemRandomSource(1234);
            List<double> uniformSamples1 = rnd.DoubleSequence().Take(10000).ToList();
            List<double> uniformSamples2 = rnd.DoubleSequence(15000.3).Take(500000).ToList();
            List<double> uniformSamples3 = rnd.DoubleSequence(-400, -1.2).Take(30000).ToList();

            double min1 = uniformSamples1.Min();

            double min2 = uniformSamples2.Min();
            double max2 = uniformSamples2.Max();

            double min3 = uniformSamples3.Min();
            double max3 = uniformSamples3.Max();

            Assert.GreaterOrEqual(min1, 0);

            Assert.GreaterOrEqual(min2, 0);
            Assert.LessOrEqual(max2, 15000.3);

            Assert.GreaterOrEqual(min3, -400);
            Assert.LessOrEqual(max3, -1.2);
        }

        [Test]
        public void TestInt()
        {
            RandomGenerator rnd = RandomGenerator.GetInstance();


            for (int i = 0; i < 10000; i++)
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
                int a = rnd.NextInt(Math.Min(b, c), Math.Max(b, c));
                Assert.True(a >= Math.Min(b, c) && a < Math.Max(b, c));
            }
        }


        [Test]
        public void TestIntSequences()
        {
            RandomGenerator rnd = RandomGenerator.GetInstance();

            rnd.RandomSource = new SystemRandomSource(1234);
            List<int> uniformSamples1 = rnd.IntSequence().Take(10000).ToList();
            List<int> uniformSamples2 = rnd.IntSequence(15000).Take(500000).ToList();
            List<int> uniformSamples3 = rnd.IntSequence(-400, -1).Take(30000).ToList();

            int min1 = uniformSamples1.Min();

            int min2 = uniformSamples2.Min();
            int max2 = uniformSamples2.Max();

            int min3 = uniformSamples3.Min();
            int max3 = uniformSamples3.Max();

            Assert.GreaterOrEqual(min1, 0);

            Assert.GreaterOrEqual(min2, 0);
            Assert.LessOrEqual(max2, 15000);

            Assert.GreaterOrEqual(min3, -400);
            Assert.LessOrEqual(max3, -1);

        }

        [Test]
        public void TestRandomGaussian()
        {
            RandomGenerator rnd = RandomGenerator.GetInstance();

            rnd.RandomSource = new SystemRandomSource(1234);
            List<double> gaussianSamples1 = Enumerable.Range(0, 100000).Select(s => rnd.NextGaussian()).ToList();
            List<double> gaussianSamples2 =
                Enumerable.Range(0, 100000).Select(s => rnd.NextGaussian(3, 2)).ToList();
            List<double> gaussianSamples3 =
                Enumerable.Range(0, 100000).Select(s => rnd.NextBoundedGaussian(4, 1, 5)).ToList();

            double mean1 = gaussianSamples1.Average();
            double mean2 = gaussianSamples2.Average();
            double mean3 = gaussianSamples3.Average();
            double min3 = gaussianSamples3.Min();
            double max3 = gaussianSamples3.Max();

            Assert.AreEqual(0, mean1, 0.01);
            Assert.AreEqual(3, mean2, 0.1);
            Assert.AreEqual(4, mean3, 0.1);
            Assert.GreaterOrEqual(min3, 1);
            Assert.LessOrEqual(max3, 5);
        }

        [Test]
        public void TestRandomGeneratorMock()
        {
            RandomGenerator rnd = RandomGenerator.GetInstance();
            rnd.RandomSource = new MockRandomSource(new[] {3, 450, 97},
                new[] {0, 0.6, 0.9, 3});


            Assert.AreEqual(0, rnd.NextDouble());
            Assert.AreEqual(3, rnd.NextInt());
            Assert.AreEqual(50, rnd.NextInt(100));
            Assert.AreEqual(3, rnd.NextInt(3, 100));
            Assert.AreEqual(0.3, rnd.NextDouble(0.5));
            Assert.AreEqual(2.93, rnd.NextDouble(0.5, 3.2));
            Assert.Throws(typeof (InvalidOperationException), () => rnd.NextInt());
            Assert.Throws(typeof (Exception), () => rnd.NextDouble());
        }

        [Test]
        public void TestXorShiftRandomSource()
        {
            XorShiftRandomSource source = new XorShiftRandomSource();
            double[] values = Enumerable.Range(0, 1000000).Select(i => source.NextDouble()).ToArray();
            UniformDistributionTest(values,0,1);
        }

        [Test]
        public void TestSystemRandomSource()
        {
            SystemRandomSource source = new SystemRandomSource();
            double[] values = Enumerable.Range(0, 1000000).Select(i => source.NextDouble()).ToArray();
            UniformDistributionTest(values, 0, 1);
        }

        [Test]
        public void TestBoxMullerTransformation()
        {
            BoxMullerTransformation boxMullerTransformation = new BoxMullerTransformation(RandomGenerator.GetInstance());
            double[] values = Enumerable.Range(0, 1000000).Select(i => boxMullerTransformation.NextGaussian(3,5)).ToArray();
            ShapiroWilkTest test = new ShapiroWilkTest(values);
            Assert.False(test.Significant);
        }

        [Test]
        public void TestBoxMullerTransformationClamped()
        {
            BoxMullerTransformation boxMullerTransformation = new BoxMullerTransformation(RandomGenerator.GetInstance());
            double[] values = Enumerable.Range(0, 1000000).Select(i => boxMullerTransformation.NextBoundedGaussian(3,2,10)).ToArray();
            ShapiroWilkTest test = new ShapiroWilkTest(values);
            Assert.True(test.Significant);
        }


        public void UniformDistributionTest(double[] sampleArr, double lowerBound, double upperBound)
        {
            Array.Sort(sampleArr);
            RunningStatistics runningStats = new RunningStatistics(sampleArr);

            // Skewness should be pretty close to zero (evenly distributed samples)
            if (Math.Abs(runningStats.Skewness) > 0.01) Assert.Fail();

            // Mean test.
            double range = upperBound - lowerBound;
            double expectedMean = lowerBound + (range / 2.0);
            double meanErr = expectedMean - runningStats.Mean;
            double maxExpectedErr = range / 1000.0;

            if (Math.Abs(meanErr) > maxExpectedErr) Assert.Fail();
            
            for (double tau = 0; tau <= 1.0; tau += 0.01)
            {
                double quantile = SortedArrayStatistics.Quantile(sampleArr, tau);
                double expectedQuantile = lowerBound + (tau * range);
                double quantileError = expectedQuantile - quantile;
                if (Math.Abs(quantileError) > maxExpectedErr) Assert.Fail();
            }

            ShapiroWilkTest test = new ShapiroWilkTest(sampleArr);
            Assert.True(test.Significant);
        }
    }
}