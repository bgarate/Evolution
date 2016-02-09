﻿using System;
using System.Diagnostics;
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
    public class GaTest
    {
        [Test]
        public void EasyGaTest()
        {
            var crossover = new MultipointCrossover<ListGenotype<BitGene>, BitGene, double>(1);

            var algorithm =
                new EasyGa<ListGenotype<BitGene>, double>.Builder()
                    .WithElitismPercentage(0.5)
                    .WithFitnessFunction(g => ((ListGenotype<BitGene>) g).Count(b => b.Value))
                    .WithStopCriteria(w => w.Generation > 2500 || w.BestFitness == 20)
                    .RegisterBreeder(new BitBreeder(20,20))
                    .Register(new RouletteWheelSelector<ListGenotype<BitGene>>(20))
                    .Register(new Recombinator<ListGenotype<BitGene>, double>(crossover, 2, 10))
                    .Register(new BitMutator<ListGenotype<BitGene>, double>(0.05))
                    .Build();

            var engine = new Engine<ListGenotype<BitGene>, double>.Builder()
                .WithAlgorithm(algorithm)
                .Build();

            while (!engine.HasReachedStopCriteria)
            {
                //if (engine.World != null)
                //{
                //    TestContext.WriteLine($"Generation {engine.World.Generation} (Size: {engine.World.Population.Count})");
                //    foreach (Individual<ListGenotype<BitGene>, double> individual in engine.World.Population)
                //    {
                //        TestContext.WriteLine(individual);
                //    }
                //}
                engine.NextGeneration();
            }

            TestContext.WriteLine($"{engine.World.Generation} generations reached");
            TestContext.WriteLine($"{engine.World.BestFitness} is best Fitness");
            TestContext.WriteLine($"{engine.World.BestGenotype} is best Genotype");

        }
    }
}
