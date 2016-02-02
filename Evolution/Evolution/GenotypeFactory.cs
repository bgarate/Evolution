using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Singular.Evolution.Core;

namespace Singular.Evolution
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FactoryAttribute : Attribute
    {
        public FactoryAttribute(Type factoryType)
        {
            FactoryType = factoryType;
        }

        public Type FactoryType { get; }
    }

    public class Factory
    {
        private static readonly Factory Instance = new Factory();

        private readonly Dictionary<Type, TypeLambda> Factories = new Dictionary<Type, TypeLambda>();

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

                Factories.Add(type, new TypeLambda(factoryAttribute.FactoryType));
            }
        }

        public static Factory GetInstance()
        {
            return Instance;
        }

        public G BuildFactory<T, G>() where T : IGenotype
        {
            TypeLambda typeLambda;

            if (Factories.ContainsKey(typeof (T)))
            {
                typeLambda = Factories[typeof (T)];
            }
            else
            {
                Type genericType = typeof (T).GetGenericTypeDefinition();
                Type genericFactory = Factories[genericType].FactoryType;
                Type newFactoryType = genericFactory.MakeGenericType(typeof (T).GetGenericArguments());
                typeLambda = new TypeLambda(newFactoryType);
                Factories.Add(typeof (T), typeLambda);
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


    public static class Activator
    {
        public delegate object ObjectActivator(params object[] args);

        public static ObjectActivator GetActivator
            (ConstructorInfo ctor)
        {
            Type type = ctor.DeclaringType;
            ParameterInfo[] paramsInfo = ctor.GetParameters();

            //create a single param of type object[]
            ParameterExpression param =
                Expression.Parameter(typeof (object[]), "args");

            Expression[] argsExp =
                new Expression[paramsInfo.Length];

            //pick each arg from the params array 
            //and create a typed expression of them
            for (int i = 0; i < paramsInfo.Length; i++)
            {
                Expression index = Expression.Constant(i);
                Type paramType = paramsInfo[i].ParameterType;

                Expression paramAccessorExp =
                    Expression.ArrayIndex(param, index);

                Expression paramCastExp =
                    Expression.Convert(paramAccessorExp, paramType);

                argsExp[i] = paramCastExp;
            }

            //make a NewExpression that calls the
            //ctor with the args we just created
            NewExpression newExp = Expression.New(ctor, argsExp);

            //create a lambda with the New
            //Expression as body and our param object[] as arg
            LambdaExpression lambda =
                Expression.Lambda(typeof (ObjectActivator), newExp, param);

            //compile it
            ObjectActivator compiled = (ObjectActivator) lambda.Compile();
            return compiled;
        }
    }
}