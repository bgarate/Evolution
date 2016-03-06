using System;

namespace Singular.Evolution.Core
{
    /// <summary>
    /// Inteface for the engine's statistics
    /// </summary>
    /// <typeparam name="G"></typeparam>
    /// <typeparam name="F"></typeparam>
    public interface IStats<G, F> where F : IComparable<F> where G : IGenotype
    {
        /// <summary>
        /// Gets the first generation the best individual was produced.
        /// </summary>
        /// <value>
        /// The best individual generation.
        /// </value>
        int BestIndividualGeneration { get; }

        /// <summary>
        /// Gets the best individual.
        /// </summary>
        /// <value>
        /// The best individual.
        /// </value>
        Individual<G, F> BestIndividual { get; }

        /// <summary>
        /// Gets the first world to have the best individual.
        /// </summary>
        /// <value>
        /// The best world.
        /// </value>
        World<G, F> BestWorld { get; }

        /// <summary>
        /// Gets the best fitness of the best individual.
        /// </summary>
        /// <value>
        /// The best fitness.
        /// </value>
        F BestFitness { get; }

        /// <summary>
        /// Gets the best genotype of the best individual.
        /// </summary>
        /// <value>
        /// The best genotype.
        /// </value>
        G BestGenotype { get; }
    }
}