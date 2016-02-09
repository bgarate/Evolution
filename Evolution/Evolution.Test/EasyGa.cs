using System.Linq;
using NUnit.Framework;
using Singular.Evolution.Algorithms;
using Singular.Evolution.Alterers;
using Singular.Evolution.Core;
using Singular.Evolution.Genes;
using Singular.Evolution.Genotypes;
using Singular.Evolution.Selectors;

namespace Evolution.Test
{
    [TestFixture]
    public class GATest
    {
        [Test]
        public void EasyGaTest()
        {
            var crossover = new MultipointCrossover<ListGenotype<BitGene>, BitGene, double>(1);

            var algorithm =
                new EasyGa<ListGenotype<BitGene>, double>.Builder()
                    .WithElitismPercentage(0.20)
                    .WithFitnessFunction(g => ((IListGenotype<BitGene>) g).Count(b => b.Value))
                    .WithStopCriteria(w => w.Generation > 500 || w.Population.Max(i => i.Fitness) == 20)
                    .Register(new RandomBitBreeder(20,20))
                    .Register(new RouletteWheelSelector<ListGenotype<BitGene>>(20))
                    .Register(new Recombinator<ListGenotype<BitGene>, double>(crossover, 2, 10))
                    .Build();

            var engineBuilder = new Engine<ListGenotype<BitGene>, double>.Builder()
                .WithAlgorithm(algorithm)
                .Build();
        }
    }
}