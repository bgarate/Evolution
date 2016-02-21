using System;
using System.Collections.Generic;
using Singular.Evolution.Algorithms;

namespace Singular.Evolution.Core
{
    public class Engine<G, F> where F : IComparable<F> where G : IGenotype
    {
        private Stats<G, F> statistics;

        private Engine()
        {
        }


        public IStats<G, F> Statistics => statistics;

        public bool HasReachedStopCriteria { get; private set; }

        public IAlgorithm<G, F> Algorithm { get; private set; }
        public World<G, F> CurrentWorld { get; private set; }
        public Executor Executor { get; private set; }

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

        public class Builder
        {
            private readonly Engine<G, F> engine = new Engine<G, F>();

            public IAlgorithm<G, F> Algorithm
            {
                set { engine.Algorithm = value; }
            }

            public Builder WithAlgorithm(IAlgorithm<G, F> algorithm)
            {
                engine.Algorithm = algorithm;
                return this;
            }

            public Engine<G, F> Build()
            {
                if (engine.Algorithm == null)
                    throw new Exception($"{nameof(engine.Algorithm)} must be set");

                engine.Executor = new Executor();
                engine.Algorithm.Executor = engine.Executor;

                return engine;
            }
        }
    }
}