using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Singular.Evolution.Core
{
    public class Algorithm<G,F> where G : IGenotype where F : IComparable<F>
    {
        private Algorithm(IPipe<G,F> initialPipe, IList<G> initialGenotypes)
        {
            InitialPipe = initialPipe;
            InitialGenotypes = initialGenotypes;
            CurrentWorld = new World<G, F>(0, Individual<G,F>.FromGenotypes(initialGenotypes));
        }
        
        public IPipe<G,F> InitialPipe { get; }
        public IList<G>  InitialGenotypes { get; }
        public World<G,F> CurrentWorld { get; private set; }  

        public void Process()
        {
            IList<Individual<G,F>> output = InitialPipe.Input(CurrentWorld.Population);
            CurrentWorld = new World<G, F>(CurrentWorld.Generation, output);
        }

        public class Builder
        {
               
        }
    }
}
