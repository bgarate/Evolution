using System.Collections;
using System.Collections.Generic;

namespace Singular.Evolution.Core
{
    public class GeneEnumerator<G> : IEnumerator<G> where G : IGene
    {
        private readonly IEnumerator<G> internalEnumerator;

        public GeneEnumerator(IEnumerable<G> enumerable)
        {
            internalEnumerator = enumerable.GetEnumerator();
        }


        public void Dispose()
        {
            internalEnumerator.Dispose();
        }

        public bool MoveNext()
        {
            return internalEnumerator.MoveNext();
        }

        public void Reset()
        {
            internalEnumerator.Reset();
        }

        public G Current => (G) internalEnumerator.Current?.Clone();

        object IEnumerator.Current => Current;
    }
}