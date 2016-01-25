using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Core;
using Singular.Evolution.Genotypes;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Alterers
{
    public class MultipointCrossover<G,F>: IAlterer where G : IGene, new() where F: IComparable<F>
    {
        public MultipointCrossover(int points)
        {
            Points = points;
        }

        public int Points { get; }

        public IList<Individual<IListGenotype<G>,F>> Apply(IList<Individual<IListGenotype<G>,F>> parents)
        {
            if (parents.Count() != 2)
                throw new ArgumentException("Input expected two parents");

            IListGenotype<G> parent1 = parents[0].Genotype;
            IListGenotype<G> parent2 = parents[1].Genotype;

            if (parent1.Count != parent2.Count)
                throw new ArgumentException("Parents should have same length");

            int count = parent1.Count;

            if (Points >= count)
                throw new ArgumentException($"Parents should have at least {count} genes");

            return Individual<IListGenotype<G>,F>.FromGenotypes(GetOffsprig(parent1, parent2));
        }

        private IList<IListGenotype<G>> GetOffsprig(IListGenotype<G> parent1, IListGenotype<G> parent2)
        {
            int count = parent1.Count;

            List<G> child1 = new List<G>();
            List<G> child2 = new List<G>();

            Queue<G> parentGenes1 = new Queue<G>(parent1);
            Queue<G> parentGenes2 = new Queue<G>(parent2);

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

            return new List<IListGenotype<G>> {new ListGenotype<G>(child1), new ListGenotype<G>(child2)};
        }

        private static void FromParentToChilds(int index, int lastIndex, bool parent1ToChild1, Queue<G> parentGenes1,
            Queue<G> parentGenes2, List<G> child1, List<G> child2)
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
        
        IList<Individual<G1, F1>> IAlterer.Apply<G1, F1>(IList<Individual<G1, F1>> individuals)
        {
            return (IList<Individual<G1, F1>>) Apply((IList<Individual<IListGenotype<G>, F>>) individuals);
        }
    }
}