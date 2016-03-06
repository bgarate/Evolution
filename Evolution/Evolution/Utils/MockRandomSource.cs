using System;
using System.Collections.Generic;

namespace Singular.Evolution.Utils
{
    /// <summary>
    /// Mock random source, specially for testing purpouses
    /// </summary>
    /// <seealso cref="Singular.Evolution.Utils.IRandomSource" />
    public class MockRandomSource : IRandomSource
    {
        private readonly IEnumerator<double> doubleEnumerator;
        private readonly IEnumerator<int> intEnumerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockRandomSource"/> class.
        /// </summary>
        /// <param name="intEnumerator">The int enumerator.</param>
        /// <param name="doubleEnumerator">The double enumerator.</param>
        public MockRandomSource(IEnumerable<int> intEnumerator, IEnumerable<double> doubleEnumerator)
        {
            this.intEnumerator = intEnumerator.GetEnumerator();
            this.doubleEnumerator = doubleEnumerator.GetEnumerator();
        }

        /// <summary>
        /// Returns a new random integer
        /// </summary>
        /// <returns></returns>
        public int NextInt()
        {
            intEnumerator.MoveNext();

            return intEnumerator.Current;
        }


        /// <summary>
        /// Returns a new random double
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Mock values must be between 0 and 1</exception>
        public double NextDouble()
        {
            doubleEnumerator.MoveNext();
            if (doubleEnumerator.Current < 0 || doubleEnumerator.Current > 1)
                throw new Exception("Mock values must be between 0 and 1");
            return doubleEnumerator.Current;
        }
    }
}