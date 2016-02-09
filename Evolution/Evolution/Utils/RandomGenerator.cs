using System;
using System.Collections.Generic;

namespace Singular.Evolution.Utils
{
    public class RandomGenerator : IRandomGenerator
    {
        private static readonly RandomGenerator Instance = new RandomGenerator();
        private readonly BoxMullerTransformation gaussian;

        private IRandomSource rnd = new SystemRandomSource();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static RandomGenerator()
        {
        }

        private RandomGenerator()
        {
            gaussian = new BoxMullerTransformation(this);
        }

        public IRandomSource RandomSource
        {
            set
            {
                if (value == null)
                    throw new Exception("Random source not set");

                rnd = value;
            }
        }

        public int NextInt()
        {
            return rnd.NextInt();
        }

        public int NextInt(int min, int max)
        {
            return min + rnd.NextInt()%(max - min);
        }

        public int NextInt(int max)
        {
            return rnd.NextInt()%max;
        }

        public double NextDouble()
        {
            return rnd.NextDouble();
        }

        public double NextDouble(double min, double max)
        {
            return min + rnd.NextDouble()*(max - min);
        }

        public double NextDouble(double max)
        {
            return rnd.NextDouble()*max;
        }

        public IEnumerable<int> IntSequence()
        {
            while (true)
            {
                yield return NextInt();
            }
        }

        public IEnumerable<int> IntSequence(int max)
        {
            while (true)
            {
                yield return NextInt(max);
            }
        }

        public IEnumerable<int> IntSequence(int min, int max)
        {
            while (true)
            {
                yield return NextInt(min, max);
            }
        }

        public IEnumerable<double> DoubleSequence()
        {
            while (true)
            {
                yield return NextDouble();
            }
        }

        public IEnumerable<double> DoubleSequence(double max)
        {
            while (true)
            {
                yield return NextDouble(max);
            }
        }

        public IEnumerable<double> DoubleSequence(double min, double max)
        {
            while (true)
            {
                yield return NextDouble(min, max);
            }
        }

        public double NextGaussian()
        {
            return gaussian.NextGaussian();
        }

        public double NextGaussian(double mean, double deviation = 1)
        {
            return gaussian.NextGaussian(mean, deviation);
        }

        public double NextBoundedGaussian(double mean, double min, double max)
        {
            return gaussian.NextBoundedGaussian(mean, min, max);
        }

        public static RandomGenerator GetInstance()
        {
            return Instance;
        }
    }

    internal class BoxMullerTransformation
    {
        private const int BOUNDED_DEVIATIONS = 3;

        private readonly IRandomGenerator rnd;
        private double? secondDerivative;

        public BoxMullerTransformation(IRandomGenerator rnd)
        {
            this.rnd = rnd;
        }

        public double NextGaussian()
        {
            return CalculateGaussianDistribution();
        }

        public double NextGaussian(double mean, double deviation)
        {
            return mean + NextGaussian()*deviation;
        }

        public double NextBoundedGaussian(double mean, double min, double max)
        {
            double deviation = (min + max)/2/BOUNDED_DEVIATIONS;

            return MoreMath.Clamp(mean + NextGaussian()*deviation, min, max);
        }

        private double CalculateGaussianDistribution()
        {
            if (secondDerivative != null)
            {
                double ret = secondDerivative.Value;
                secondDerivative = null;
                return ret;
            }

            double squares;
            double x;
            double y;

            do
            {
                x = rnd.NextDouble(-1, 1);
                y = rnd.NextDouble(-1, 1);
                squares = x*x + y*y;
            } while (squares >= 1 || Math.Abs(squares) < double.Epsilon);

            double f = Math.Sqrt(-2.0*Math.Log(squares)/squares);

            secondDerivative = y*f;
            return x;
        }
    }
}