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
    }
}