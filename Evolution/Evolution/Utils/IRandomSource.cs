namespace Singular.Evolution.Utils
{
    /// <summary>
    /// Interface for sources of random numbers
    /// </summary>
    public interface IRandomSource
    {
        /// <summary>
        /// Returns a new random double
        /// </summary>
        /// <returns></returns>
        double NextDouble();

        /// <summary>
        /// Returns a new random integer
        /// </summary>
        /// <returns></returns>
        int NextInt();
    }
}