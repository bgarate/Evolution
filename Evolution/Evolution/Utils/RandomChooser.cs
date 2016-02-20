using System.Collections.Generic;
using System.Linq;

namespace Singular.Evolution.Utils
{
    public static class RandomChooser
    {
        public static IEnumerable<T> ToRandomEnumerable<T>(this IList<T> list)
        {
            RandomGenerator rnd = RandomGenerator.GetInstance();
            while (true)
            {
                yield return list[rnd.NextInt(list.Count)];
            }
        }

        public static IEnumerable<T> RandomTake<T>(this IList<T> list, int n)
        {
            return list.ToRandomEnumerable().Take(n);
        }

        public static IEnumerable<T> RandomSort<T>(this IEnumerable<T> source)
        {
            List<T> destination = new List<T>();
            foreach (T item in source)
            {
                int rnd = RandomGenerator.GetInstance().NextInt(0, destination.Count + 1);
                if (rnd == destination.Count)
                {
                    destination.Add(item);
                }
                else
                {
                    destination.Add(destination[rnd]);
                    destination[rnd] = item;
                }
            }
            return destination;
        }
    }
}