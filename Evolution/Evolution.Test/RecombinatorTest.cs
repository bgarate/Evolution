using System.Collections.Generic;
using NUnit.Framework;
using Singular.Evolution.Alterers;
using Singular.Evolution.Core;
using Singular.Evolution.Genes;
using Singular.Evolution.Genotypes;

namespace Evolution.Test
{
    [TestFixture]
    public class RecombinatorTest
    {
        [Test]
        public void TestRecombinator()
        {

            MultipointCrossover<ListGenotype<FloatGene>,FloatGene,int> crossover = new MultipointCrossover<ListGenotype<FloatGene>,FloatGene, int>(1);
            Recombinator<ListGenotype<FloatGene>,int> recombinator = new Recombinator<ListGenotype<FloatGene>,int>(crossover, 3, 2);
            recombinator.Apply(new List<Individual<ListGenotype<FloatGene>, int>>()
            {
                new Individual<ListGenotype<FloatGene>, int>(new ListGenotype<FloatGene>(4)),
                new Individual<ListGenotype<FloatGene>, int>(new ListGenotype<FloatGene>(4)),
                new Individual<ListGenotype<FloatGene>, int>(new ListGenotype<FloatGene>(4))
            });
        }
        
    }
}