using System;

namespace Singular.Evolution
{
    public interface INumericGene<T> : IGene, IComparable<T>
    {
        T Sum(T a, T b);
        T Substract(T a, T b);
        T Multiply(T a, T b);
        T Divide(T a, T b);
    }
}