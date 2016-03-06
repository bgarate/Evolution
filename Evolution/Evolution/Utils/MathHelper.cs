using System;

namespace Singular.Evolution.Utils { 

    /// <summary>
    /// Mathematical helper tools
    /// </summary>
    public static class MathHelper
    {
        /// <summary>
        /// Clamps the specified value between min and max
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        public static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
        {
            return value.CompareTo(min) < 0 ? min : value.CompareTo(max) > 0 ? max : value;
        }

        /// <summary>
        /// Returns whether the specified double lies close enough (double.Epsilon) an integer value.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static bool IsInteger(double number)
        {
            return Math.Abs(number - (int) number) < double.Epsilon;
        }

        /// <summary>
        /// Returns whether the specified float lies close enough (double.Epsilon) an integer value.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static bool IsInteger(float number)
        {
            return Math.Abs(number - (int) number) < float.Epsilon;
        }

        /// <summary>
        /// Determines whether the specified double lies in the range [0,1]
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static bool IsProbabilty(double number)
        {
            return number >= 0 && number <= 1;
        }
    }
}
