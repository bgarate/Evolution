using System;
using System.Collections.Generic;
using Singular.Evolution.Algorithms;

namespace Singular.Evolution.Core
{
    public class Engine<G, F> where F : IComparable<F> where G : IGenotype
    {
        private Engine()
        {
        }
        
        public bool HasReachedStopCriteria { get; private set; }

        public IAlgorithm<G, F> Algorithm { get; private set; }
        public World<G, F> World { get; private set; }

        public void NextGeneration()
        {
            if (HasReachedStopCriteria)
                throw new Exception("Algorithm has reach stop criteria");

            if (World == null)
            {
                IList<Individual<G, F>> firstGeneration = Algorithm.Initialize();
                World = new World<G, F>(firstGeneration);
            }
            else
            {
                IList<Individual<G, F>> newGeneration = Algorithm.Execute(World);
                World = new World<G, F>(World, newGeneration);
            }
            
            if (Algorithm.ShouldStop(World))
            {
                HasReachedStopCriteria = true;
            }
        }

        public class Builder
        {
            private readonly Engine<G, F> engine = new Engine<G, F>();

            public Builder()
            {
                
            }

            public Builder WithAlgorithm(IAlgorithm<G,F> algorithm)
            {
                engine.Algorithm = algorithm;
                return this;
            }

            public IAlgorithm<G, F> Algorithm
            {
                set { engine.Algorithm = value; }
            }

            public Engine<G, F> Build()
            {
                if(engine.Algorithm == null)
                    throw  new Exception($"{nameof(engine.Algorithm)} must be set");

                return engine;
            } 
        }
    }
}