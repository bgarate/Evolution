using System;
using System.Collections.Generic;
using Singular.Evolution.Core;

namespace Singular.Evolution.Algorithms
{
    public delegate F FitnessFunctionDelegate<in G, out F>(G genotype) where F : IComparable<F> where G : IGenotype;

    /// <summary>
    /// Interface for Evolutive Algorithms
    /// Algorithms doesn't have an internal state. This state is tracked by the <see cref="Engine"/>
    /// </summary>
    /// <typeparam name="G">Genotype</typeparam>
    /// <typeparam name="F">Fitness</typeparam>
    public interface IAlgorithm<G, F> where F : IComparable<F> where G : IGenotype
    {
        /// <summary>
        /// Fitness delegate
        /// </summary>
        /// <value>
        /// The fitness function.
        /// </value>
        FitnessFunctionDelegate<G, F> FitnessFunction { get; }

        /// <summary>
        /// Sets the engine which acts on the algorithm. Is for internal use of the algorithm
        /// </summary>
        /// <value>
        /// The engine.
        /// </value>
        Engine<G, F> Engine { set; }

        /// <summary>
        /// Runs a full algorithm epoch on the input <see cref="World{G,F}"/> and returns a List of <see cref="Individual{G,F}"/>
        /// </summary>
        /// <param name="original">The current world</param>
        /// <returns>List of new individuals</returns>
        IList<Individual<G, F>> Execute(World<G, F> original);

        /// <summary>
        /// Returns the first population for this algorithm
        /// </summary>
        /// <returns></returns>
        IList<Individual<G, F>> Initialize();

        /// <summary>
        /// Returns true it the algorithm's ending criteria is met
        /// </summary>
        /// <param name="world">The current world</param>
        /// <returns></returns>
        bool ShouldStop(World<G, F> world);
    }
}