using System;
using Singular.Evolution.Core;

namespace Singular.Evolution.Genes
{
    /// <summary>
    /// Represents a floating precision valued gene
    /// </summary>
    public class FloatGene : INumericGene<FloatGene, double>, IEquatable<FloatGene>, IComparable<FloatGene>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FloatGene"/> class.
        /// </summary>
        public FloatGene()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FloatGene"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="min">The minimum.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public FloatGene(double value, double? max = null, double? min = null)
        {
            Value = value;

            if (min.HasValue != max.HasValue)
                throw new ArgumentException(Resources.Both_or_none_bounds_must_be_null);

            MaxValue = max;
            MinValue = min;
        }

        /// <summary>
        /// Gets the maximum value.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        public double? MaxValue { get; }

        /// <summary>
        /// Gets the minimum value.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        public double? MinValue { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is bounded.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is bounded; otherwise, <c>false</c>.
        /// </value>
        public bool IsBounded => MinValue != null;

        /// <summary>
        /// Compares to other
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public int CompareTo(FloatGene other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Determines whether other, is equal to this instance.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(FloatGene other)
        {
            return Math.Abs(Value - other.Value) < double.Epsilon && EqualBounds(other);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public double Value { get; }

        /// <summary>
        /// Returns true if this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid => (MaxValue == null && MinValue == null) || (MaxValue >= Value && MinValue <= Value);

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public IGene Clone()
        {
            return new FloatGene(Value, MaxValue, MinValue);
        }

        /// <summary>
        /// Returns a genes wich represents the sum of this instance with the specified b.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public FloatGene Sum(FloatGene b)
        {
            return MinimumBoundedGene(Value + b.Value, this, b);
        }

        /// <summary>
        /// Returns a genes wich represents the difference of this this instance with thespecified b.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public FloatGene Subtract(FloatGene b)
        {
            return MinimumBoundedGene(Value - b.Value, this, b);
        }

        /// <summary>
        /// Returns a genes wich represents the product of this instance by the specified b.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public FloatGene Multiply(FloatGene b)
        {
            return MinimumBoundedGene(Value*b.Value, this, b);
        }

        /// <summary>
        /// Returns a genes wich represents the quotient of this instance with the specified b.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public FloatGene Divide(FloatGene b)
        {
            return MinimumBoundedGene(Value/b.Value, this, b);
        }


        private bool EqualBounds(FloatGene other)
        {
            bool bothBounded = IsBounded && other.IsBounded;
            return (bothBounded && Math.Abs(MaxValue.Value - other.MaxValue.Value) < double.Epsilon &&
                    Math.Abs(MinValue.Value - other.MinValue.Value) < double.Epsilon)
                   || (!IsBounded && !other.IsBounded);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode() + MaxValue.GetHashCode()*17 + MinValue.GetHashCode()*17*17;
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
            FloatGene g = obj as FloatGene;

            if (g == null)
                return false;

            return Equals(g);
        }

        /// <summary>
        /// Returns a gene with newValue value and whose bounds are the maximum range which fits in a and b bounds
        /// </summary>
        /// <param name="newValue">The new value.</param>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public static FloatGene MinimumBoundedGene(double newValue, FloatGene a, FloatGene b)
        {
            double? max = a.MaxValue;
            double? min = a.MinValue;

            if (!a.IsBounded)
            {
                max = b.MaxValue;
                min = b.MinValue;
            }
            else if (b.IsBounded)
            {
                max = Math.Min(a.MaxValue.Value, b.MaxValue.Value);
                min = Math.Max(a.MinValue.Value, b.MinValue.Value);
            }

            return new FloatGene(newValue, max, min);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{{{(IsBounded ? MinValue + "-" : "")}{Value}{(IsBounded ? "-" + MaxValue : "")}}}";
        }
    }
}