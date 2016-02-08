using System;
using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Core;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Alterers
{
    public class MultipointCrossover<G, R, F> : IAlterer<G, F> where G : IListGenotype<R>
        where F : IComparable<F>
        where R : IGene, new()
    {
        public MultipointCrossover(int points)
        {
            Points = points;
        }

        public int Points { get; }

        public IList<Individual<G, F>> Apply(IList<Individual<G, F>> parents)
        {
            if (parents.Count() != 2)
                throw new ArgumentException("Input expected two parents");

            G parent1 = parents[0].Genotype;
            G parent2 = parents[1].Genotype;

            if (parent1.Count != parent2.Count)
                throw new ArgumentException("Parents should have same length");

            int count = parent1.Count;

            if (Points >= count)
                throw new ArgumentException($"Parents should have at least {count} genes");

            return Individual<G, F>.FromGenotypes(GetOffspring(parent1, parent2));
        }

        private IList<G> GetOffspring(IListGenotype<R> parent1, IListGenotype<R> parent2)
        {
            int count = parent1.Count;

            List<R> child1 = new List<R>();
            List<R> child2 = new List<R>();

            Queue<R> parentGenes1 = new Queue<R>(parent1);
            Queue<R> parentGenes2 = new Queue<R>(parent2);

            List<int> indexes = GenerateIndexes(count);
            indexes.Add(count);

            int lastIndex = 0;
            bool parent1ToChild1 = true;

            foreach (int index in indexes)
            {
                FromParentToChilds(index, lastIndex, parent1ToChild1, parentGenes1, parentGenes2, child1, child2);
                parent1ToChild1 = !parent1ToChild1;
                lastIndex = index;
            }

            IListGenotypeFactory<G, R> factory = Factory.GetInstance().BuildFactory<G, IListGenotypeFactory<G, R>>();
            return new List<G> {factory.Create(child1), factory.Create(child2)};
        }

        private static void FromParentToChilds(int index, int lastIndex, bool parent1ToChild1, Queue<R> parentGenes1,
            Queue<R> parentGenes2, List<R> child1, List<R> child2)
        {
            int toTake = index - lastIndex;

            for (int i = 0; i < toTake; i++)
            {
                if (parent1ToChild1)
                {
                    child1.Add(parentGenes1.Dequeue());
                    child2.Add(parentGenes2.Dequeue());
                }
                else
                {
                    child1.Add(parentGenes2.Dequeue());
                    child2.Add(parentGenes1.Dequeue());
                }
            }
        }

        private List<int> GenerateIndexes(int count)
        {
            RandomGenerator rnd = RandomGenerator.GetInstance();

            List<int> indexes = new List<int>();

            while (indexes.Count < Points)
            {
                int next = rnd.NextInt(1, count);
                if (!indexes.Contains(next))
                    indexes.Add(next);
            }

            indexes.Sort();
            return indexes;
        }
    }
}