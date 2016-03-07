using System;
using System.Collections.Generic;

namespace Singular.Evolution.Utils
{
    /// <summary>
    /// Represents a singleton Random genereator
    /// </summary>
    /// <seealso cref="Singular.Evolution.Utils.IRandomGenerator" />
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

        /// <summary>
        /// Sets the random source.
        /// </summary>
        /// <value>
        /// The random source.
        /// </value>
        /// <exception cref="System.Exception">Random source not set</exception>
        public IRandomSource RandomSource
        {
            set
            {
                if (value == null)
                    throw new Exception("Random source not set");

                rnd = value;
            }
        }

        /// <summary>
        /// Returns a non negative integer
        /// </summary>
        /// <returns></returns>
        public int NextInt()
        {
            return rnd.NextInt();
        }

        /// <summary>
        /// Returns an integer in the range [min,max)
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        public int NextInt(int min, int max)
        {
            return min + rnd.NextInt()%(max - min);
        }

        /// <summary>
        /// Returns an integer in the range [0,max)
        /// </summary>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        public int NextInt(int max)
        {
            return rnd.NextInt()%max;
        }

        /// <summary>
        /// Returns a non negative double
        /// </summary>
        /// <returns></returns>
        public double NextDouble()
        {
            return rnd.NextDouble();
        }

        /// <summary>
        /// Returns a double in the range [min,max]
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        public double NextDouble(double min, double max)
        {
            return min + rnd.NextDouble()*(max - min);
        }

        /// <summary>
        /// Returns a double in the range [0,max]
        /// </summary>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        public double NextDouble(double max)
        {
            return rnd.NextDouble()*max;
        }

        /// <summary>
        /// Returns an infinite sequence of random non negative integers in the range
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> IntSequence()
        {
            while (true)
            {
                yield return NextInt();
            }
        }

        /// <summary>
        /// Returns an infinite sequence of random integers in the range [0,max)
        /// </summary>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        public IEnumerable<int> IntSequence(int max)
        {
            while (true)
            {
                yield return NextInt(max);
            }
        }

        /// <summary>
        /// Returns an infinite sequence of random integers in the range [min,max)
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        public IEnumerable<int> IntSequence(int min, int max)
        {
            while (true)
            {
                yield return NextInt(min, max);
            }
        }

        /// <summary>
        /// Returns an infinite sequence of random non negative doubles
        /// </summary>
        /// <returns></returns>
        public IEnumerable<double> DoubleSequence()
        {
            while (true)
            {
                yield return NextDouble();
            }
        }

        /// <summary>
        /// Returns an infinite sequence of random doubles in the range [0,max]
        /// </summary>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        public IEnumerable<double> DoubleSequence(double max)
        {
            while (true)
            {
                yield return NextDouble(max);
            }
        }

        /// <summary>
        /// Returns an infinite sequence of random doubles in the range [min,max]
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        public IEnumerable<double> DoubleSequence(double min, double max)
        {
            while (true)
            {
                yield return NextDouble(min, max);
            }
        }

        /// <summary>
        /// Returns a double sampled from a normal distribution
        /// </summary>
        /// <returns></returns>
        public double NextGaussian()
        {
            return gaussian.NextGaussian();
        }

        /// <summary>
        /// Returns a double sampled from a normal distribution with the specified mean and standard deviation
        /// </summary>
        /// <param name="mean">The mean.</param>
        /// <param name="deviation">The deviation.</param>
        /// <returns></returns>
        public double NextGaussian(double mean, double deviation = 1)
        {
            return gaussian.NextGaussian(mean, deviation);
        }

        /// <summary>
        /// Returns a double sampled from a normal distribution where aproximately 99.7% (3 standard deviations) lies between min and max and clamped for values lying
        /// outside these bounds
        /// </summary>
        /// <param name="mean">The mean.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        public double NextBoundedGaussian(double mean, double min, double max)
        {
            return gaussian.NextBoundedGaussian(mean, min, max);
        }

        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        /// <returns></returns>
        public static RandomGenerator GetInstance()
        {
            return Instance;
        }
    }

    public class BoxMullerTransformation
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

            return MathHelper.Clamp(mean + NextGaussian()*deviation, min, max);
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
            return x*f;
        }
    }
}