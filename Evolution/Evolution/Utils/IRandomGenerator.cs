using System.Collections.Generic;

namespace Singular.Evolution.Utils
{
    /// <summary>
    /// Interface for Random Number Generators
    /// </summary>
    public interface IRandomGenerator
    {
        /// <summary>
        /// Returns an infinite sequence of random non negative doubles
        /// </summary>
        /// <returns></returns>
        IEnumerable<double> DoubleSequence();

        /// <summary>
        /// Returns an infinite sequence of random doubles in the range [0,max]
        /// </summary>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        IEnumerable<double> DoubleSequence(double max);

        /// <summary>
        /// Returns an infinite sequence of random doubles in the range [min,max]
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        IEnumerable<double> DoubleSequence(double min, double max);

        /// <summary>
        /// Returns an infinite sequence of random non negative integers in the range
        /// </summary>
        /// <returns></returns>
        IEnumerable<int> IntSequence();

        /// <summary>
        /// Returns an infinite sequence of random integers in the range [0,max)
        /// </summary>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        IEnumerable<int> IntSequence(int max);

        /// <summary>
        /// Returns an infinite sequence of random integers in the range [min,max)
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        IEnumerable<int> IntSequence(int min, int max);

        /// <summary>
        /// Returns a non negative double
        /// </summary>
        /// <returns></returns>
        double NextDouble();

        /// <summary>
        /// Returns a double in the range [0,max]
        /// </summary>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        double NextDouble(double max);

        /// <summary>
        /// Returns a double in the range [min,max]
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        double NextDouble(double min, double max);

        /// <summary>
        /// Returns a non negative integer
        /// </summary>
        /// <returns></returns>
        int NextInt();

        /// <summary>
        /// Returns an integer in the range [0,max)
        /// </summary>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        int NextInt(int max);

        /// <summary>
        /// Returns an integer in the range [min,max)
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        int NextInt(int min, int max);

        /// <summary>
        /// Returns a double sampled from a normal distribution
        /// </summary>
        /// <returns></returns>
        double NextGaussian();

        /// <summary>
        /// Returns a double sampled from a normal distribution with the specified mean and standard deviation
        /// </summary>
        /// <param name="mean">The mean.</param>
        /// <param name="deviation">The deviation.</param>
        /// <returns></returns>
        double NextGaussian(double mean, double deviation);

        /// <summary>
        /// Returns a double sampled from a normal distribution where aproximately 99.7% (3 standard deviations) lies between min and max and clamped for values lying
        /// outside these bounds
        /// </summary>
        /// <param name="mean">The mean.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        double NextBoundedGaussian(double mean, double min, double max);
    }
}
