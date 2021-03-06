﻿using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Singular.Evolution
{
    /// <summary>
    /// Helper class to create a delegate which returns new objects based on their type's constructors
    /// </summary>
    public static class Activator
    {
        /// <summary>
        /// Delegate for creation of the object
        /// </summary>
        /// <param name="args">The arguments to be passed to the constructor</param>
        /// <returns>Instantiated object</returns>
        public delegate object ObjectActivator(params object[] args);


        /// <summary>
        /// Gets a delegate to instantiate an object from a given constructor
        /// </summary>
        /// <param name="ctor">Constructor of the object to return</param>
        /// <returns></returns>
        public static ObjectActivator GetActivator
            (ConstructorInfo ctor)
        {
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