using System;

namespace Singular.Evolution.Core
{
    /// <summary>
    /// Interface for numeric genes. This genes implement some basic operations and have a value of a specified type
    /// </summary>
    /// <typeparam name="G"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Singular.Evolution.Core.IGene" />
    public interface INumericGene<G, T> : IGene where G : IGene, IComparable<G> where T : IComparable<T>
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        T Value { get; }

        /// <summary>
        /// Returns a genes wich represents the sum of this instance with the specified b.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        G Sum(G b);

        /// <summary>
        /// Returns a genes wich represents the difference of this this instance with thespecified b.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        G Subtract(G b);

        /// <summary>
        /// Returns a genes wich represents the product of this instance by the specified b.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        G Multiply(G b);

        /// <summary>
        /// Returns a genes wich represents the quotient of this instance with the specified b.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        G Divide(G b);
    }
}