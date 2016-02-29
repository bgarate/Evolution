using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Singular.Evolution.Core;

namespace Singular.Evolution.Algorithms
{
    /// <summary>
    /// Simple Genetic Algorithm. Basic flow with elitism
    /// </summary>
    /// <typeparam name="G">Genotype</typeparam>
    /// <typeparam name="F">Fitness</typeparam>
    /// <seealso cref="Singular.Evolution.Algorithms.IAlgorithm{G, F}" />
    public class EasyGa<G, F> : IAlgorithm<G, F> where G : IGenotype where F : IComparable<F>
    {

        private Engine<G, F> engine;

        private EasyGa()
        {
            Breeders = new List<object>();
            Selectors = new List<ISelector<G, F>>();
            Alterers = new List<IAlterer<G, F>>();
        }
        //
        public IList<object> Breeders { get; }
        public IList<ISelector<G, F>> Selectors { get; }
        public IList<IAlterer<G, F>> Alterers { get; }
        public StopCriteriaDelegate<G, F> StopCriteria { get; private set; }

        public double ElitismPercentage { get; private set; }

        public Engine<G, F> Engine
        {
            get { return engine; }
            set
            {
                if (engine != null)
                    throw new Exception($"{nameof(Engine)} is already set");

                engine = value;
            }
        }

        public FitnessFunctionDelegate<G, F> FitnessFunction { get; private set; }

        public IList<Individual<G, F>> Initialize()
        {
            List<Individual<G, F>> firstGeneration = new List<Individual<G, F>>();

            foreach (object item in Breeders)
            {
                IBreeder<G> breeder = item as IBreeder<G>;

                if (breeder != null)
                {
                    firstGeneration.AddRange(Individual<G, F>.FromGenotypes(breeder.Breed()));
                }
                else
                {
                    IAlterer<G, F> alterer = item as IAlterer<G, F>;
                    Debug.Assert(alterer != null);
                    firstGeneration = alterer.Apply(firstGeneration).ToList();
                }
            }
            firstGeneration = UpdateFitness(firstGeneration).ToList();
            return firstGeneration;
        }

        public IList<Individual<G, F>> Execute(World<G, F> world)
        {
            List<Individual<G, F>> original = world.Population.ToList();

            List<Individual<G, F>> individuals = Select(original);

            individuals = ApplyAlterers(individuals);

            individuals.AddRange(GetElite(original));

            individuals = UpdateFitness(individuals);
            return individuals;
        }

        public bool ShouldStop(World<G, F> world)
        {
            return StopCriteria(world);
        }

        private List<Individual<G, F>> Select(List<Individual<G, F>> original)
        {
            return Selectors.SelectMany(s => s.Apply(original)).ToList();
        }

        private List<Individual<G, F>> ApplyAlterers(List<Individual<G, F>> individuals)
        {
            individuals = Alterers.Aggregate(individuals, (current, alterer) => alterer.Apply(current).ToList());
            return individuals;
        }

        private List<Individual<G, F>> GetElite(List<Individual<G, F>> original)
        {
            List<Individual<G, F>> sortedOriginal = new List<Individual<G, F>>(original);
            sortedOriginal.Sort((i1, i2) => -i1.CompareTo(i2));
            List<Individual<G, F>> elite = sortedOriginal.Take((int) (original.Count*ElitismPercentage)).ToList();
            return elite;
        }

        private List<Individual<G, F>> UpdateFitness(List<Individual<G, F>> original)
        {
            return Engine.Executor.UpdateFitness(original);
        }

        public class Builder
        {
            private readonly EasyGa<G, F> algorithm = new EasyGa<G, F>();

            public FitnessFunctionDelegate<G, F> FitnessFunction
            {
                set { algorithm.FitnessFunction = value; }
            }

            public StopCriteriaDelegate<G, F> StopCriteria
            {
                set { algorithm.StopCriteria = value; }
            }

            public double ElitismPercentage
            {
                set
                {
                    if (value < 0 || value > 1)
                        throw new Exception($"{nameof(algorithm.ElitismPercentage)} must be between 0 and 1");

                    algorithm.ElitismPercentage = value;
                }
            }

            public Builder WithFitnessFunction(FitnessFunctionDelegate<G, F> value)
            {
                FitnessFunction = value;
                return this;
            }

            public Builder WithStopCriteria(StopCriteriaDelegate<G, F> value)
            {
                StopCriteria = value;
                return this;
            }

            public Builder RegisterBreeder(IBreeder<G> breeder)
            {
                algorithm.Breeders.Add(breeder);
                return this;
            }

            public Builder RegisterBreeder(IAlterer alterer)
            {
                algorithm.Breeders.Add(alterer);
                return this;
            }

            public Builder Register(ISelector<G, F> selector)
            {
                algorithm.Selectors.Add(selector);
                return this;
            }

            public Builder Register(IAlterer<G, F> alterer)
            {
                algorithm.Alterers.Add(alterer);
                return this;
            }

            public Builder WithElitismPercentage(double value)
            {
                ElitismPercentage = value;
                return this;
            }

            public EasyGa<G, F> Build()
            {
                if (!algorithm.Breeders.Any())
                    throw new Exception("Must have at least one breeder");

                if (!algorithm.Selectors.Any())
                    throw new Exception("Must have at least one selector");

                if (!algorithm.Alterers.Any())
                    throw new Exception("Must have at least one alterer");

                if (algorithm.FitnessFunction == null)
                    throw new Exception($"Must define a {nameof(algorithm.FitnessFunction)}");

                if (algorithm.StopCriteria == null)
                    throw new Exception($"Must define a {nameof(algorithm.StopCriteria)}");

                return algorithm;
            }
        }
    }
}