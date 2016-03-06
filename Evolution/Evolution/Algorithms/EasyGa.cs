using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Singular.Evolution.Core;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Algorithms
{
    /// <summary>
    /// Simple Genetic Algorithm. Basic flow with elitism
    /// </summary>
    /// <typeparam name="G">Genotype</typeparam>
    /// <typeparam name="F">Fitness</typeparam>
    /// <seealso cref="IAlgorithm{G,F}" />
    public class EasyGa<G, F> : IAlgorithm<G, F> where G : IGenotype where F : IComparable<F>
    {

        private Engine<G, F> engine;
        private double elitismPercentage;

        private EasyGa()
        {
            Breeders = new List<object>();
            Selectors = new List<ISelector<G, F>>();
            Alterers = new List<IAlterer<G, F>>();
        }
        /// <summary>
        /// Gets the breeders for the original population
        /// </summary>
        /// <value>
        /// The breeders.
        /// </value>
        public IList<object> Breeders { get; }

        /// <summary>
        /// Gets the selectors which take the parents from the generation
        /// </summary>
        /// <value>
        /// The selectors.
        /// </value>
        public IList<ISelector<G, F>> Selectors { get; }

        /// <summary>
        /// Gets the alterers which apply on the parents
        /// </summary>
        /// <value>
        /// The alterers.
        /// </value>
        public IList<IAlterer<G, F>> Alterers { get; }

        /// <summary>
        /// Gets the delegate which determines if the algorithm has met the stop criteria
        /// </summary>
        /// <value>
        /// The stop criteria.
        /// </value>
        public StopCriteriaDelegate<G, F> StopCriteria { get; private set; }


        /// <summary>
        /// Gets the elitism percentage. The range is [0,1]. 
        /// </summary>
        /// <value>
        /// The elitism percentage.
        /// </value>
        /// <exception cref="System.ArgumentException">The elitism percentage must belong to the [0,1] range</exception>
        public double ElitismPercentage
        {
            get { return elitismPercentage; }
            private set
            {
                if (!MathHelper.IsProbabilty(elitismPercentage))
                    throw new ArgumentException("The elitism percentage must belong to the [0,1] range");
                elitismPercentage = value;
            }
        }

        /// <summary>
        /// Sets the engine which acts on the algorithm. Is for internal use of the algorithm
        /// </summary>
        /// <value>
        /// The engine.
        /// </value>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Fitness delegate
        /// </summary>
        /// <value>
        /// The fitness function.
        /// </value>
        public FitnessFunctionDelegate<G, F> FitnessFunction { get; private set; }

        /// <summary>
        /// Returns the first population for this algorithm
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Executes the specified world.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <returns></returns>
        public IList<Individual<G, F>> Execute(World<G, F> world)
        {
            List<Individual<G, F>> original = world.Population.ToList();

            List<Individual<G, F>> individuals = Select(original);

            individuals = ApplyAlterers(individuals);

            individuals.AddRange(GetElite(original));

            individuals = UpdateFitness(individuals);
            return individuals;
        }

        /// <summary>
        /// Returns true it the algorithm's ending criteria is met
        /// </summary>
        /// <param name="world">The current world</param>
        /// <returns></returns>
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

        /// <summary>
        /// Builder class for EasyGa Algorithm
        /// </summary>
        /// <seealso cref="Singular.Evolution.Core.IAlgorithm{G, F}" />
        public class Builder
        {
            private readonly EasyGa<G, F> algorithm = new EasyGa<G, F>();

            /// <summary>
            /// Sets the fitness function.
            /// </summary>
            /// <value>
            /// The fitness function.
            /// </value>
            public FitnessFunctionDelegate<G, F> FitnessFunction
            {
                set { algorithm.FitnessFunction = value; }
            }

            /// <summary>
            /// Sets the stop criteria.
            /// </summary>
            /// <value>
            /// The stop criteria.
            /// </value>
            public StopCriteriaDelegate<G, F> StopCriteria
            {
                set { algorithm.StopCriteria = value; }
            }

            /// <summary>
            /// Sets the elitism percentage.
            /// </summary>
            /// <value>
            /// The elitism percentage.
            /// </value>
            /// <exception cref="System.Exception"></exception>
            public double ElitismPercentage
            {
                set
                {
                    if (value < 0 || value > 1)
                        throw new Exception($"{nameof(algorithm.ElitismPercentage)} must be between 0 and 1");

                    algorithm.ElitismPercentage = value;
                }
            }

            /// <summary>
            /// Fluent setter for the fitness function.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            public Builder WithFitnessFunction(FitnessFunctionDelegate<G, F> value)
            {
                FitnessFunction = value;
                return this;
            }

            /// <summary>
            /// Fluent setter for the stop criteria.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            public Builder WithStopCriteria(StopCriteriaDelegate<G, F> value)
            {
                StopCriteria = value;
                return this;
            }

            /// <summary>
            /// Registers a breeder.
            /// </summary>
            /// <param name="breeder">The breeder.</param>
            /// <returns></returns>
            public Builder RegisterBreeder(IBreeder<G> breeder)
            {
                algorithm.Breeders.Add(breeder);
                return this;
            }

            /// <summary>
            /// Registers an alterer to apply to breeder's output for first population.
            /// </summary>
            /// <param name="alterer">The alterer.</param>
            /// <returns></returns>
            public Builder RegisterBreeder(IAlterer<G,F> alterer)
            {
                algorithm.Breeders.Add(alterer);
                return this;
            }

            /// <summary>
            /// Registers the specified selector.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <returns></returns>
            public Builder Register(ISelector<G, F> selector)
            {
                algorithm.Selectors.Add(selector);
                return this;
            }

            /// <summary>
            /// Registers the specified alterer.
            /// </summary>
            /// <param name="alterer">The alterer.</param>
            /// <returns></returns>
            public Builder Register(IAlterer<G, F> alterer)
            {
                algorithm.Alterers.Add(alterer);
                return this;
            }

            /// <summary>
            /// Fluent setter for the elitism percentage.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            public Builder WithElitismPercentage(double value)
            {
                ElitismPercentage = value;
                return this;
            }

            /// <summary>
            /// Builds the EasyGa algorithm.
            /// </summary>
            /// <returns>Instance of the EasyGa algorithm</returns>
            /// <exception cref="System.Exception">
            /// Must have at least one breeder
            /// or
            /// Must have at least one selector
            /// or
            /// Must have at least one alterer
            /// or
            /// or
            /// </exception>
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