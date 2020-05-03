using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.General.Reflection
{
    public static class ReflectionHelper
    {
        public static Type[] GetAllDerivativeTypesFrom(Type baseType) =>
            baseType.Assembly.GetTypes().Where(type => type.IsSubclassOf(baseType) && !type.IsAbstract).ToArray();
        
        public static T[] CreateInstance<T>(IEnumerable<Type> typesToCreate, params object[] parameters) =>
            typesToCreate.Select(type => (T) Activator.CreateInstance(type, parameters)).ToArray();
    }
}