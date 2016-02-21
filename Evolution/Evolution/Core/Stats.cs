using System;

namespace Singular.Evolution.Core
{
    public class Stats<G, F> : IStats<G, F> where F : IComparable<F> where G : IGenotype
    {
        private Stats(World<G, F> bestWorld)
        {
            BestWorld = bestWorld;
        }

        public World<G, F> BestWorld { get; }
        public int BestIndividualGeneration => BestWorld.Generation;
        public Individual<G, F> BestIndividual => BestWorld.BestIndividual;
        public F BestFitness => BestWorld.BestFitness;
        public G BestGenotype => BestWorld.BestGenotype;

        public static Stats<G, F> CalculateNewStatistis(World<G, F> newWorld, IStats<G, F> oldStats)
        {
            if (oldStats == null || oldStats.BestFitness.CompareTo(newWorld.BestFitness) == -1)
            {
                return new Stats<G, F>(newWorld);
            }
            return new Stats<G, F>(oldStats.BestWorld);
        }
    }
}