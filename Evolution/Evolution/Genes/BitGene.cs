using System;
using Singular.Evolution.Core;

namespace Singular.Evolution.Genes
{
    public class BitGene : INumericGene<BitGene, bool>, IEquatable<BitGene>, IComparable<BitGene>
    {
        public BitGene()
        {
        }

        public BitGene(bool value)
        {
            Value = value;
        }
        
        public bool Value { get; }
        
        public bool Equals(BitGene other)
        {
            return Value == other.Value;
        }

        public bool IsValid => true;

        public IGene Clone()
        {
            return new BitGene(Value);
        }

        public int CompareTo(BitGene other)
        {
            return Value.CompareTo(other);
        }

        public BitGene Sum(BitGene b)
        {
            return new BitGene(Value ^ b.Value);
        }
    
        public BitGene Subtract(BitGene b)
        {
            return Sum(b);
        }

        public BitGene Multiply(BitGene b)
        {
            return new BitGene(Value && b.Value);
        }

        public BitGene Divide(BitGene b)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            BitGene g = obj as BitGene;

            if (g == null)
                return false;

            return Equals(g);
        }

    }
}