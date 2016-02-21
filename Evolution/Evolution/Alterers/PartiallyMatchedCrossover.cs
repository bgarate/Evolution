using System;
using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Core;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Alterers
{
    public class PartiallyMatchedCrossover<G, R, F> : CrossoverBase<G,R, F> where G : IListGenotype<G,R>
        where F : IComparable<F>
        where R : class, IGene,new()
    {

        public PartiallyMatchedCrossover() : base(2, 2, 2, true)
        {

        }

        protected override IList<G> GetOffspring(IEnumerable<G> parents)
        {
            List<G> parentsList = parents.ToList();
            List<R> parent1 = parentsList[0].ToList();
            List<R> parent2 = parentsList[1].ToList();

            int count = parent1.Count;

            RandomGenerator rnd = RandomGenerator.GetInstance();

            int point1 = rnd.NextInt(0, count);
            int point2 = rnd.NextInt(point1 + 1, count + 1);

            IEnumerable<R> child1 = GetChild(parent1, parent2, point1, point2);
            IEnumerable<R> child2 = GetChild(parent2, parent1, point1, point2);

            IListGenotypeFactory<G, R> factory = Factory.GetInstance().BuildFactory<G,IListGenotypeFactory<G,R>>();

            return new List<G> {factory.Create(child1),factory.Create(child2)};
        }

        private static IEnumerable<R> GetChild(IList<R> parent1, IList<R> parent2, int point1, int point2)
        {
            int count = parent1.Count;
            List<R> child = new List<R>(Enumerable.Repeat<R>(null, count));

            CopySlice(child, parent1, point1, point2);
            PassCycles(child, parent1, parent2, point1, point2);
            ParentToChild(child, parent2);

            return child;
        }

        private static void PassCycles(List<R> child, IList<R> parent1, IList<R> parent2, int point1, int point2)
        {
            List<int> inParent2NotInSlice = GetInParent2NotInSlice(parent1, parent2, point1, point2);

            foreach (int i in inParent2NotInSlice)
            {
                int indexInParent2 = i;
                do
                {
                    R valueInParent1 = parent1[indexInParent2];
                    indexInParent2 = parent2.IndexOf(valueInParent1);
                } while (indexInParent2 >= point1 && indexInParent2 < point2);
                child[indexInParent2] = parent2[i];
            }
        }

        private static void ParentToChild(IList<R> child,IList<R> parent2)
        {
            int count = parent2.Count;

            for (int i = 0; i < count; i++)
            {
                if (child[i] == null)
                    child[i] = parent2[i];
            }
        }

        private static List<int> GetInParent2NotInSlice(IList<R> parent1, IList<R> parent2, int point1, int point2)
        {
            List<int> inParent2NotInSlice = new List<int>();
            HashSet<R> slice1 = new HashSet<R>(parent1.Skip(point1).Take(point2 - point1));


            for (int i = point1; i < point2; i++)
            {
                if (!slice1.Contains(parent2[i]))
                    inParent2NotInSlice.Add(i);
            }
            return inParent2NotInSlice;
        }

        private static void CopySlice(IList<R> destination, IList<R> source, int point1, int point2)
        {
            for (int i = point1; i < point2; i++)
            {
                destination[i] = source[i];
            }
        }

    }
}