using System;

namespace Singular.Evolution.Core
{
    /// <summary>
    /// Summary of historic statistics of the GA Algorithm
    /// </summary>
    /// <typeparam name="G"></typeparam>
    /// <typeparam name="F"></typeparam>
    /// <seealso cref="Singular.Evolution.Core.IStats{G, F}" />
    public class Stats<G, F> : IStats<G, F> where F : IComparable<F> where G : IGenotype
    {
        private Stats(World<G, F> bestWorld)
        {
            BestWorld = bestWorld;
        }

        /// <summary>
        /// Gets the best world in the history of execution.
        /// </summary>
        /// <value>
        /// The best world.
        /// </value>
        public World<G, F> BestWorld { get; }

        /// <summary>
        /// Gets the first generation which produced the current best individual.
        /// </summary>
        /// <value>
        /// The best individual's generation.
        /// </value>
        public int BestIndividualGeneration => BestWorld.Generation;

        /// <summary>
        /// Gets the best individual.
        /// </summary>
        /// <value>
        /// The best individual.
        /// </value>
        public Individual<G, F> BestIndividual => BestWorld.BestIndividual;

        /// <summary>
        /// Gets the best individual's fitness.
        /// </summary>
        /// <value>
        /// The best fitness.
        /// </value>
        public F BestFitness => BestWorld.BestFitness;

        /// <summary>
        /// Gets the best individual's genotype.
        /// </summary>
        /// <value>
        /// The best genotype.
        /// </value>
        public G BestGenotype => BestWorld.BestGenotype;

        /// <summary>
        /// Returns a new <see cref="Stats{G,F}"/> using the previous statistics information and the newly generated world
        /// </summary>
        /// <param name="newWorld">The new world.</param>
        /// <param name="oldStats">The old stats.</param>
        /// <returns></returns>
        public static Stats<G, F> CalculateNewStatistics(World<G, F> newWorld, IStats<G, F> oldStats)
        {
            if (oldStats == null || oldStats.BestFitness.CompareTo(newWorld.BestFitness) == -1)
            {
                return new Stats<G, F>(newWorld);
            }
            return new Stats<G, F>(oldStats.BestWorld);
        }
    }
}