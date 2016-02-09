using System;

namespace Singular.Evolution.Utils
{
    public class SystemRandomSource : IRandomSource
    {
        private readonly Random rnd;

        public SystemRandomSource()
        {
            rnd = new Random();
        }

        public SystemRandomSource(int seed)
        {
            rnd = new Random(seed);
        }

        public int NextInt()
        {
            return rnd.Next();
        }


        public double NextDouble()
        {
            return rnd.NextDouble();
        }
    }
}