using System.Collections.Generic;

namespace Singular.Evolution.Utils
{
    public interface IRandomGenerator
    {
        IEnumerable<double> DoubleSequence();
        IEnumerable<double> DoubleSequence(double max);
        IEnumerable<double> DoubleSequence(double min, double max);
        IEnumerable<int> IntSequence();
        IEnumerable<int> IntSequence(int max);
        IEnumerable<int> IntSequence(int min, int max);
        double NextDouble();
        double NextDouble(double max);
        double NextDouble(double min, double max);
        int NextInt();
        int NextInt(int max);
        int NextInt(int min, int max);
        double NextGaussian();
        double NextGaussian(double mean, double deviation);
        double NextBoundedGaussian(double mean, double min, double max);
    }
}