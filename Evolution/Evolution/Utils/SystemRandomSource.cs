using System;

namespace Singular.Evolution.Utils
{
    public class SystemRandomSource : IRandomSource
    {
        private readonly Random rnd = new Random();

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