using System;
using Singular.Evolution.Core;

namespace Singular.Evolution.Genes
{
    public class FloatGene : INumericGene<FloatGene, double>, IEquatable<FloatGene>, IComparable<FloatGene>
    {
        public FloatGene()
        {
        }

        public FloatGene(double value, double? max = null, double? min = null)
        {
            Value = value;

            if (min != max && (min == null || max == null))
                throw new ArgumentException("Both or none bounds must be null");

            MaxValue = max;
            MinValue = min;
        }

        public double? MaxValue { get; }
        public double? MinValue { get; }

        public double Value { get; }

        public bool IsBounded => MinValue != null;

        public bool Equals(FloatGene other)
        {
            return Math.Abs(Value - other.Value) < double.Epsilon;
        }

        public bool IsValid => (MaxValue == null && MinValue == null) || (MaxValue >= Value && MinValue <= Value);

        public IGene Clone()
        {
            return new FloatGene(Value, MaxValue, MinValue);
        }

        public int CompareTo(FloatGene other)
        {
            return Value.CompareTo(other);
        }

        public FloatGene Sum(FloatGene b)
        {
            return MinimumBoundedGene(Value + b.Value, this, b);
        }

        public FloatGene Substract(FloatGene b)
        {
            return MinimumBoundedGene(Value - b.Value, this, b);
        }

        public FloatGene Multiply(FloatGene b)
        {
            return MinimumBoundedGene(Value*b.Value, this, b);
        }

        public FloatGene Divide(FloatGene b)
        {
            return MinimumBoundedGene(Value/b.Value, this, b);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode() + MaxValue.GetHashCode()*17 + MinValue.GetHashCode()*17*17;
        }

        public override bool Equals(object obj)
        {
            FloatGene g = obj as FloatGene;

            if (g == null)
                return false;

            return Equals(g);
        }

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
    }
}