using System;

namespace Singular.Evolution.Core
{
    public interface INumericGene<T> : IGene, IComparable<T>
    {
        T Sum(T b);
        T Substract(T b);
        T Multiply(T b);
        T Divide(T b);
    }
}