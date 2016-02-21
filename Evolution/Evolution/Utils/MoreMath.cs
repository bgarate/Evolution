using System;

namespace Singular.Evolution.Utils
{
    public static class MoreMath
    {
        public static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
        {
            return value.CompareTo(min) < 0 ? min : value.CompareTo(max) > 0 ? max : value;
        }

        public static bool IsInteger(double number)
        {
            return Math.Abs(number - (int) number) < Double.Epsilon;
        }

        public static bool IsInteger(float number)
        {
            return Math.Abs(number - (int)number) < Single.Epsilon;
        }

        public static bool IsProbabilty(double number)
        {
            return number >= 0 && number <= 1;
        }
    }
}