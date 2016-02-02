using System;
using System.Collections.Generic;

namespace Singular.Evolution.Utils
{
    public class MockRandomSource : IRandomSource
    {
        private readonly IEnumerator<double> doubleEnumerator;
        private readonly IEnumerator<int> intEnumerator;

        public MockRandomSource(IEnumerable<int> intEnumerator, IEnumerable<double> doubleEnumerator)
        {
            this.intEnumerator = intEnumerator.GetEnumerator();
            this.doubleEnumerator = doubleEnumerator.GetEnumerator();
        }

        public int NextInt()
        {
            intEnumerator.MoveNext();

            return intEnumerator.Current;
        }


        public double NextDouble()
        {
            doubleEnumerator.MoveNext();
            if (doubleEnumerator.Current < 0 || doubleEnumerator.Current > 1)
                throw new Exception("Mock values must be between 0 and 1");
            return doubleEnumerator.Current;
        }
    }
}