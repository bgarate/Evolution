using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Singular.Evolution.Algorithms;
using Singular.Evolution.Alterers;
using Singular.Evolution.Breeders;
using Singular.Evolution.Core;
using Singular.Evolution.Genes;
using Singular.Evolution.Genotypes;
using Singular.Evolution.Selectors;
using Singular.Evolution.Utils;

namespace Evolution.Samples.TravelingSalesmanProblem
{
    internal class Tsp
    {
        private const double CIRCLE_RADIUS = 0.4;
        private readonly List<City> cities;

        public Tsp(int numberOfCities, PositionSelection positionSelection = PositionSelection.Random)
        {
            cities = new List<City>();

            for (int i = 0; i < numberOfCities; i++)
            {
                PointF position;
                if (positionSelection == PositionSelection.Random)
                {
                    RandomGenerator rnd = RandomGenerator.GetInstance();
                    position = new PointF((float) rnd.NextDouble(), (float) rnd.NextDouble());
                }
                else
                {
                    double angle = (float) (Math.PI*2/numberOfCities*i);

                    position = new PointF((float) (Math.Sin(angle)*CIRCLE_RADIUS + 0.5),
                        (float) (Math.Cos(angle)*CIRCLE_RADIUS + 0.5));
                }

                cities.Add(new City(((char) ("A"[0] + i)).ToString(), position));
            }

            BuildEngine();
        }

        public List<City> Cities => cities.ToList();
        private Engine<ListGenotype<FloatGene>, double> Engine { get; set; }

        public int Generation => Engine.CurrentWorld?.Generation ?? 0;
        public double BestFitness => Engine.Statistics?.BestFitness ?? 0;
        public int BestFitnessGeneration => Engine.Statistics?.BestIndividualGeneration ?? 0;

        private double FitnessFunction(ListGenotype<FloatGene> genotype)
        {
            double squaredDistance = 0;
            for (int i = 1; i < genotype.Count; i++)
            {
                City city = Cities[(int) genotype[i].Value];
                City previousCity = Cities[(int) genotype[i - 1].Value];

                squaredDistance += Math.Pow(city.Position.X - previousCity.Position.X, 2) +
                                   Math.Pow(city.Position.Y - previousCity.Position.Y, 2);
            }

            return 1/squaredDistance;
        }

        private void BuildEngine()
        {
            GenotypeGeneratorBreeder<ListGenotype<FloatGene>>.BreederDelegateWithIndex generateNewGenotype =
                index => new ListGenotype<FloatGene>(
                    Enumerable.Range(0, cities.Count).ToList().RandomSort().Select(i => new FloatGene(i)));

            EasyGa<ListGenotype<FloatGene>, double> algorithm =
                new EasyGa<ListGenotype<FloatGene>, double>.Builder()
                    .WithElitismPercentage(0.01)
                    .WithFitnessFunction(FitnessFunction)
                    .WithStopCriteria(world => world.Generation == 1200)
                    .RegisterBreeder(new GenotypeGeneratorBreeder<ListGenotype<FloatGene>>(generateNewGenotype, 200))
                    .Register(new RouletteWheelSelector<ListGenotype<FloatGene>>(200))
                    .Register(
                        new Recombinator<ListGenotype<FloatGene>, double>(
                            new PartiallyMatchedCrossover<ListGenotype<FloatGene>, FloatGene, double>(), 2, 100,
                            Recombinator<ListGenotype<FloatGene>, double>.RecombinatioNumberType.Absolute))
                    .Register(new ReverseAlterer<ListGenotype<FloatGene>, FloatGene, double>(0.3))
                    .Register(new SwapAlterer<ListGenotype<FloatGene>, FloatGene, double>(0.3))
                    .Build();

            Engine = new Engine<ListGenotype<FloatGene>, double>.Builder()
                .WithAlgorithm(algorithm)
                .Build();
        }

        public bool Evolve()
        {
            if (!Engine.HasReachedStopCriteria)
                Engine.NextGeneration();
            return !Engine.HasReachedStopCriteria;
        }

        public List<City> GetPath()
        {
            List<City> list = new List<City>();
            if (Engine.CurrentWorld != null)
            {
                ListGenotype<FloatGene> bestGenotype = Engine.CurrentWorld.BestGenotype;
                list.AddRange(bestGenotype.Select(gene => Cities[(int) gene.Value]));
            }
            return list;
        }

        internal enum PositionSelection
        {
            Random,
            Circular
        }
    }

    internal class City
    {
        public City(string name, PointF position)
        {
            Position = position;
            Name = name;
        }

        public PointF Position { get; }
        public string Name { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}