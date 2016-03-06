using System;

namespace Singular.Evolution
{
    /// <summary>
    /// Determines that a represents a factory
    /// </summary>
    /// <seealso cref="System.Attribute" />
    /// <seealso cref="Factory"/>
    [AttributeUsage(AttributeTargets.Class)]
    public class FactoryAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FactoryAttribute"/> class.
        /// </summary>
        /// <param name="factoryType">Type of the factory.</param>
        public FactoryAttribute(Type factoryType)
        {
            FactoryType = factoryType;
        }
        /// <summary>
        /// Gets the type of the factory.
        /// </summary>
        /// <value>
        /// The type of the factory.
        /// </value>
        public Type FactoryType { get; }
    }
}