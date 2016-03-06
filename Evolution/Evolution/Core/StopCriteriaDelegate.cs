using System;

namespace Singular.Evolution.Core
{
    /// <summary>
    /// Delegate that given a world, returns whether the stopping criteria has been reached
    /// </summary>
    /// <typeparam name="G"></typeparam>
    /// <typeparam name="F"></typeparam>
    /// <param name="world">The world.</param>
    /// <returns></returns>
    public delegate bool StopCriteriaDelegate<G, F>(World<G, F> world) where G : IGenotype where F : IComparable<F>;

    /// <summary>
    /// Helper class to build <see cref="StopCriteriaDelegate{G,F}"/>
    /// </summary>
    public static class StopCriteriaBuilder
    {
        /// <summary>
        /// Returns a <see cref="StopCriteriaDelegate{G,F}"/> which returns true when a or b return true
        /// </summary>
        /// <typeparam name="G"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public static StopCriteriaDelegate<G, F> Or<G, F>(this StopCriteriaDelegate<G, F> a,
            StopCriteriaDelegate<G, F> b) where G : IGenotype where F : IComparable<F>
        {
            return world => a(world) || b(world);
        }


        /// <summary>
        /// Returns a <see cref="StopAtFitness{G,F}"/> which returns true when both a and b return true
        /// </summary>
        /// <typeparam name="G"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public static StopCriteriaDelegate<G, F> And<G, F>(this StopCriteriaDelegate<G, F> a,
            StopCriteriaDelegate<G, F> b) where G : IGenotype where F : IComparable<F>
        {
            return world => a(world) && b(world);
        }

        /// <summary>
        /// Returns a <see cref="StopCriteriaDelegate{G,F}"/> which returns the negation of the return value of a
        /// </summary>
        /// <typeparam name="G"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <param name="a">a.</param>
        /// <returns></returns>
        public static StopCriteriaDelegate<G, F> Not<G, F>(this StopCriteriaDelegate<G, F> a) where G : IGenotype
            where F : IComparable<F>
        {
            return world => !a(world);
        }

        /// <summary>
        /// Returns a <see cref="StopCriteriaDelegate{G,F}"/> which returns true when the world generations is greater or equal than generationNumber
        /// </summary>
        /// <typeparam name="G"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <param name="generationNumber">The target generation number.</param>
        /// <returns></returns>
        public static StopCriteriaDelegate<G, F> StopAtGeneration<G, F>(int generationNumber) where G : IGenotype
            where F : IComparable<F>
        {
            return world => world.Generation >= generationNumber;
        }

        /// <summary>
        /// Returns a <see cref="StopCriteriaDelegate{G,F}"/> which returns true when the best fitness reached is greater or equal than fitness
        /// </summary>
        /// <typeparam name="G"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <param name="fitness">The fitness.</param>
        /// <returns></returns>
        public static StopCriteriaDelegate<G, F> StopAtFitness<G, F>(F fitness) where G : IGenotype
            where F : IComparable<F>
        {
            return world => world.BestFitness.CompareTo(fitness) != -1;
        }
    }
}