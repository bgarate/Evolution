using System;

namespace Singular.Evolution.Core
{
    public interface INumericGene<G,T> : IGene where G : IGene, IComparable<G> where T : IComparable<T>
    {
        T Value { get; }
        
        G Sum(G b);
        G Substract(G b);
        G Multiply(G b);
        G Divide(G b);
    }
}