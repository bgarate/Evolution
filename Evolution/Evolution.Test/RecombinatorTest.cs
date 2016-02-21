using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Singular.Evolution;
using Singular.Evolution.Core;
using Singular.Evolution.Genes;
using Singular.Evolution.Genotypes;
using Singular.Evolution.Utils;

namespace Evolution.Test
{
    [TestFixture]
    public class RecombinatorTest
    {
        [Test]
        public void TestRecombinator()
        {
            RandomGenerator.GetInstance().RandomSource = new SystemRandomSource();

            SumAlterer<ListGenotype<FloatGene>, FloatGene, int> sumAlterer =
                new SumAlterer<ListGenotype<FloatGene>, FloatGene, int>();

            ListGenotype<FloatGene> genotype = new ListGenotype<FloatGene>(new[] {new FloatGene(1)});
            Individual<ListGenotype<FloatGene>, int> individual = new Individual<ListGenotype<FloatGene>, int>(genotype);
            List<Individual<ListGenotype<FloatGene>, int>> list =
                new List<Individual<ListGenotype<FloatGene>, int>>(new[] {individual});

            Recombinator<ListGenotype<FloatGene>, int> recombinator =
                new Recombinator<ListGenotype<FloatGene>, int>(sumAlterer, 3, 2,
                    Recombinator<ListGenotype<FloatGene>, int>.RecombinatioNumberType.Absolute);

            IList<Individual<ListGenotype<FloatGene>, int>> offspring = recombinator.Apply(list);

            Assert.AreEqual(2, offspring.Count);

            foreach (Individual<ListGenotype<FloatGene>, int> i in offspring)
            {
                Assert.AreEqual(1, i.Genotype.Count);
                Assert.AreEqual(3, i.Genotype[0].Value);
            }
        }
    }

    public class SumAlterer<G, R, F> : IAlterer<G, F> where G : IListGenotype<G, R>
        where F : IComparable<F>
        where R : INumericGene<R, double>, IComparable<R>, new()
    {
        public IList<Individual<G, F>> Apply(IList<Individual<G, F>> individuals)
        {
            IListGenotypeFactory<G, R> factory = Factory.GetInstance().BuildFactory<G, IListGenotypeFactory<G, R>>();

            R sum = individuals.Aggregate(new R(), (i, p) => i.Sum(p.Genotype.First()));
            G genotype = factory.Create(new[] {sum});
            Individual<G, F> individual = new Individual<G, F>(genotype);
            return new List<Individual<G, F>>(new[] {individual});
        }
    }
}