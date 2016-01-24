using System.Collections.Generic;

namespace Singular.Evolution.Utils
{
    internal class MockRandomSource : IRandomSource
    {
        private readonly IEnumerator<int> enumerator;

        public MockRandomSource()
        {
            enumerator = Increment().GetEnumerator();
        }

        public int NextInt()
        {
            enumerator.MoveNext();
            return enumerator.Current/2;
        }


        public double NextDouble()
        {
            enumerator.MoveNext();
            return 1/(double) enumerator.Current;
        }

        private IEnumerable<int> Increment()
        {
            int x = 0;

            while (true)
            {
                x = x + 1;
                yield return x;
            }
        }
    }
}