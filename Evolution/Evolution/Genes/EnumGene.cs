using System;
using System.Globalization;
using Singular.Evolution.Core;

namespace Singular.Evolution.Genes
{
    public class EnumGene<T> : IEquatable<EnumGene<T>>, IComparable<EnumGene<T>>, IGene
        where T : struct, IComparable, IConvertible, IFormattable
    {
        public EnumGene()
        {
            if (!typeof (T).IsEnum)
                throw new ArgumentException("T must be an enum");
        }

        public EnumGene(T value) : this()
        {
            Value = value;
        }

        public T Value { get; }

        public int CompareTo(EnumGene<T> other)
        {
            return Value.CompareTo(other.Value);
        }

        public bool Equals(EnumGene<T> other)
        {
            return Value.CompareTo(other.Value) == 0;
        }

        public bool IsValid => true;

        public IGene Clone()
        {
            return new EnumGene<T>(Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            EnumGene<T> g = obj as EnumGene<T>;

            return g != null && Equals(g);
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}