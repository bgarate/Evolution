using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Singular.Evolution.Core;

namespace Singular.Evolution
{
    /// <summary>
    /// Singleton class to get concrete factories.
    /// </summary>
    public class Factory
    {
        private static readonly Factory Instance = new Factory();

        private readonly Dictionary<Type, TypeLambda> factories = new Dictionary<Type, TypeLambda>();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Factory()
        {
        }

        private Factory()
        {
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                FactoryAttribute factoryAttribute =
                    type.GetCustomAttributes(typeof (FactoryAttribute), true).SingleOrDefault() as FactoryAttribute;
                if (factoryAttribute == null)
                    continue;

                factories.Add(type, new TypeLambda(factoryAttribute.FactoryType));
            }
        }

        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        /// <returns></returns>
        public static Factory GetInstance()
        {
            return Instance;
        }

        /// <summary>
        /// Builds a factory instance given it's type and the genotype it must build
        /// </summary>
        /// <typeparam name="T">The type of the genotype to be built by the factory</typeparam>
        /// <typeparam name="G">The type of the factory</typeparam>
        /// <returns></returns>
        public G BuildFactory<T, G>() where T : IGenotype
        {
            TypeLambda typeLambda;

            if (factories.ContainsKey(typeof (T)))
            {
                typeLambda = factories[typeof (T)];
            }
            else
            {
                Type genericType = typeof (T).GetGenericTypeDefinition();
                Type genericFactory = factories[genericType].FactoryType;
                Type newFactoryType = genericFactory.MakeGenericType(typeof (T).GetGenericArguments());
                typeLambda = new TypeLambda(newFactoryType);
                factories.Add(typeof (T), typeLambda);
            }

            if (typeLambda.Activator == null)
            {
                ConstructorInfo ctor = typeLambda.FactoryType.GetConstructors().First();
                typeLambda.Activator = Activator.GetActivator(ctor);
            }

            Activator.ObjectActivator objectActivator = typeLambda.Activator;
            return (G) objectActivator();
        }

        private class TypeLambda
        {
            public TypeLambda(Type factoryType)
            {
                FactoryType = factoryType;
            }

            public Type FactoryType { get; }
            public Activator.ObjectActivator Activator { get; set; }
        }
    }
}