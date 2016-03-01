using System;
using System.Collections.Generic;
using Singular.Evolution.Algorithms;

namespace Singular.Evolution.Core
{
    /// <summary>
    /// The engine mantains the state for the execution of the algorithm
    /// </summary>
    /// <typeparam name="G">Genotype</typeparam>
    /// <typeparam name="F">Fitness</typeparam>
    public class Engine<G, F> where F : IComparable<F> where G : IGenotype
    {
        private Stats<G, F> statistics;

        private Engine()
        {
        }

        /// <summary>
        /// Gets the statistics for the evolution
        /// </summary>
        /// <value>
        /// The statistics.
        /// </value>
        public IStats<G, F> Statistics => statistics;

        /// <summary>
        /// Gets a value indicating whether the engine has reached stop criteria.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has reached stop criteria; otherwise, <c>false</c>.
        /// </value>
        public bool HasReachedStopCriteria { get; private set; }

        /// <summary>
        /// Gets the algorithm being executed
        /// </summary>
        /// <value>
        /// The algorithm.
        /// </value>
        public IAlgorithm<G, F> Algorithm { get; private set; }

        /// <summary>
        /// Gets the current world.
        /// </summary>
        /// <value>
        /// The current world.
        /// </value>
        public World<G, F> CurrentWorld { get; private set; }

        /// <summary>
        /// Gets the executor.
        /// </summary>
        /// <value>
        /// The executor.
        /// </value>
        public IExecutor<G, F> Executor { get; private set; }

        /// <summary>
        /// Runs a new generation of the algorithm
        /// </summary>
        /// <exception cref="System.Exception">Algorithm has reach stop criteria</exception>
        public void NextGeneration()
        {
            if (HasReachedStopCriteria)
                throw new Exception("Algorithm has reach stop criteria");

            if (CurrentWorld == null)
            {
                IList<Individual<G, F>> firstGeneration = Algorithm.Initialize();
                CurrentWorld = new World<G, F>(firstGeneration);
            }
            else
            {
                IList<Individual<G, F>> newGeneration = Algorithm.Execute(CurrentWorld);
                CurrentWorld = new World<G, F>(CurrentWorld, newGeneration);
            }

            statistics = Stats<G, F>.CalculateNewStatistis(CurrentWorld, Statistics);

            if (Algorithm.ShouldStop(CurrentWorld))
            {
                HasReachedStopCriteria = true;
            }
        }

        /// <summary>
        /// Builder class for the <see cref="Engine{G,F}"/>
        /// </summary>
        public class Builder
        {
            private readonly Engine<G, F> engine = new Engine<G, F>();

            /// <summary>
            /// Sets the algorithm.
            /// </summary>
            /// <value>
            /// The algorithm.
            /// </value>
            public IAlgorithm<G, F> Algorithm
            {
                set { engine.Algorithm = value; }
            }

            /// <summary>
            /// Fluid setter for the algorithm.
            /// </summary>
            /// <param name="algorithm">The algorithm.</param>
            /// <returns></returns>
            public Builder WithAlgorithm(IAlgorithm<G, F> algorithm)
            {
                engine.Algorithm = algorithm;
                return this;
            }

            /// <summary>
            /// Fluid setter for the executor.
            /// </summary>
            /// <param name="executor">The executor.</param>
            /// <returns></returns>
            public Builder WithExecutor(IExecutor<G, F> executor)
            {
                engine.Executor = executor;
                return this;
            }

            /// <summary>
            /// Builds the engine.
            /// </summary>
            /// <returns>The engine</returns>
            /// <exception cref="System.Exception"></exception>
            public Engine<G, F> Build()
            {
                if (engine.Algorithm == null)
                    throw new Exception($"{nameof(engine.Algorithm)} must be set");

                if (engine.Executor == null)
                {
                    engine.Executor =
                        new MultithreadedCachedExecutor<G, F>(engine.Algorithm.FitnessFunction);

                    engine.Algorithm.Engine = engine;
                }

                return engine;
            }
        }
    }
}