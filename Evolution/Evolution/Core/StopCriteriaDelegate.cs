using System;

namespace Singular.Evolution.Core
{
    public delegate bool StopCriteriaDelegate<G, F>(World<G, F> world) where G : IGenotype where F : IComparable<F>;

    public static class StopCriteriaBuilder
    {
        public static StopCriteriaDelegate<G, F> Or<G, F>(this StopCriteriaDelegate<G, F> a,
            StopCriteriaDelegate<G, F> b) where G : IGenotype where F : IComparable<F>
        {
            return world => a(world) || b(world);
        }

        public static StopCriteriaDelegate<G, F> And<G, F>(this StopCriteriaDelegate<G, F> a,
    StopCriteriaDelegate<G, F> b) where G : IGenotype where F : IComparable<F>
        {
            return world => a(world) && b(world);
        }

        public static StopCriteriaDelegate<G, F> Not<G, F>(this StopCriteriaDelegate<G, F> a) where G : IGenotype
            where F : IComparable<F>
        {
            return world => !a(world);
        }

        public static StopCriteriaDelegate<G, F> StopAtGeneration<G, F>(int generationNumber) where G : IGenotype
            where F : IComparable<F>
        {
            return world => world.Generation >= generationNumber;
        }

        public static StopCriteriaDelegate<G, F> StopAtFitness<G, F>(F fitness) where G : IGenotype
            where F : IComparable<F>
        {
            return world => world.BestFitness.CompareTo(fitness) != -1;
        }
    } 
}