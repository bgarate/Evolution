using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Singular.Evolution.Core;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Alterers
{
    public class ReverseAlterer<G, R, F> : IAlterer<G, F> where G : IListGenotype<G,R>
        where F : IComparable<F>
        where R : class, IGene,new()
    {
        
        public double Probability { get; }

        public ReverseAlterer(double probability)
        {
            Probability = probability;
        }

        public IList<Individual<G, F>> Apply(IList<Individual<G, F>> parents)
        {

            List<G> offspring = new List<G>(parents.Select(g=>g.Genotype));

            int numberOfMutations = (int) (parents.Count*Probability);

            foreach (int location in RandomGenerator.GetInstance().IntSequence(0,offspring.Count).Take(numberOfMutations))
            {
                offspring[location] = Mutate(offspring[location]);
            }

            return Individual<G, F>.FromGenotypes(offspring).ToList();
        }

        private G Mutate(G parent)
        {
            int count = parent.Count;

            RandomGenerator rnd = RandomGenerator.GetInstance();

            int point1 = rnd.NextInt(1, count);
            int point2 = rnd.NextInt(point1 + 1, count + 1);
            int toCenter = (point2 - point1)/2;

            List<R> genes = new List<R>(parent);
            
            for (int i = point1; i < toCenter; i++)
            {
                R gene = genes[i];
                genes[i] = genes[toCenter + i];
                genes[toCenter + i] = gene;
            }
            
            IListGenotypeFactory<G, R> factory = Factory.GetInstance().BuildFactory<G,IListGenotypeFactory<G,R>>();

            return factory.Create(genes);
        }
        
    }
}