using System.Collections.Generic;

namespace Singular.Evolution.Utils
{
    internal class MockRandomSource : IRandomSource
    {
        private readonly IEnumerator<double> enumerator;

        public MockRandomSource(IEnumerable<double> enumerable)
        {
            enumerator = enumerable.GetEnumerator();
        }

        public int NextInt()
        {
            enumerator.MoveNext();
            return (int)(enumerator.Current * 10);
        }


        public double NextDouble()
        {
            enumerator.MoveNext();
            return enumerator.Current;
        }

    }
}