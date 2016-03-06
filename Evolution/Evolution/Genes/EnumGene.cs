using System;
using System.Globalization;
using Singular.Evolution.Core;

namespace Singular.Evolution.Genes
{
    /// <summary>
    /// Represents a Gene which value is form an Enum type
    /// </summary>
    /// <typeparam name="T">Enum</typeparam>
    /// <seealso cref="IGene" />
    public class EnumGene<T> : IEquatable<EnumGene<T>>, IComparable<EnumGene<T>>, IGene
        where T : struct, IComparable, IConvertible, IFormattable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumGene{T}"/> class.
        /// </summary>
        /// <exception cref="System.ArgumentException">T must be an enum</exception>
        public EnumGene()
        {
            if (!typeof (T).IsEnum)
                throw new ArgumentException("T must be an enum");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumGene{T}"/> class with an initial value.
        /// </summary>
        /// <param name="value">The initial value.</param>
        public EnumGene(T value) : this()
        {
            Value = value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public T Value { get; }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public int CompareTo(EnumGene<T> other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Determines whether the represented gene equals other
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(EnumGene<T> other)
        {
            return Value.CompareTo(other.Value) == 0;
        }

        /// <summary>
        /// Returns true if the value is valid.
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
            return new EnumGene<T>(Value);
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
            EnumGene<T> g = obj as EnumGene<T>;

            return g != null && Equals(g);
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}