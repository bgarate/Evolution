using System;

namespace Singular.Evolution.Utils
{
    /// <summary>
    /// Represents a Randon Number Source which uses the <see cref="Random"/>
    /// </summary>
    /// <seealso cref="Singular.Evolution.Utils.IRandomSource" />
    public class SystemRandomSource : IRandomSource
    {
        private readonly Random rnd;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemRandomSource"/> class.
        /// </summary>
        public SystemRandomSource()
        {
            rnd = new Random();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemRandomSource"/> class intiliazed with
        /// a given seed.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public SystemRandomSource(int seed)
        {
            rnd = new Random(seed);
        }

        /// <summary>
        /// Returns a new random integer
        /// </summary>
        /// <returns></returns>
        public int NextInt()
        {
            return rnd.Next();
        }

        /// <summary>
        /// Returns a new random double
        /// </summary>
        /// <returns></returns>
        public double NextDouble()
        {
            return rnd.NextDouble();
        }
    }
}