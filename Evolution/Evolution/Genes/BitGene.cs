using System;
using Singular.Evolution.Core;

namespace Singular.Evolution.Genes
{
    /// <summary>
    /// Represents a binary gene 
    /// </summary>
    public class BitGene : INumericGene<BitGene, bool>, IEquatable<BitGene>, IComparable<BitGene>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BitGene"/> class.
        /// </summary>
        public BitGene()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BitGene"/> class.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public BitGene(bool value)
        {
            Value = value;
        }

        /// <summary>
        /// Compares to another BitGene's value.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public int CompareTo(BitGene other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Returns true if other represents an equivalent gene
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(BitGene other)
        {
            return Value == other.Value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public bool Value { get; }

        /// <summary>
        /// Returns true if the gene is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid => true;

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public IGene Clone()
        {
            return new BitGene(Value);
        }

        /// <summary>
        /// Returns a genes wich represents the XOR of this instance with the specified b.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public BitGene Sum(BitGene b)
        {
            return new BitGene(Value ^ b.Value);
        }

        /// <summary>
        /// Returns a genes wich represents the XOR of this this instance with thespecified b.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public BitGene Subtract(BitGene b)
        {
            return Sum(b);
        }

        /// <summary>
        /// Returns a genes wich represents the AND of this instance by the specified b.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public BitGene Multiply(BitGene b)
        {
            return new BitGene(Value && b.Value);
        }

        /// <summary>
        /// Is not applicable for BitGenes
        /// </summary>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public BitGene Divide(BitGene b)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            BitGene g = obj as BitGene;

            if (g == null)
                return false;

            return Equals(g);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Value ? "1" : "0";
        }
    }
}