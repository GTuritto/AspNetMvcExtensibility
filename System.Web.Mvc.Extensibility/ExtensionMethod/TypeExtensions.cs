#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using Diagnostics;
    using Linq;
    using Reflection;

    /// <summary>
    /// Defines an static class which contains extension methods of <see cref="Type"/>.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Determines whether the specified type has parameterless contructor..
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>
        /// <c>true</c> if parameterless constructor exists; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        public static bool HasDefaultConstructor(this Type instance)
        {
            Invariant.IsNotNull(instance, "instance");

            return instance.GetConstructors(BindingFlags.Instance | BindingFlags.Public).Any(ctor => ctor.GetParameters().Length == 0);
        }

        /// <summary>
        /// Gets the concretes the types of the given assembly.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        [DebuggerHidden]
        public static IEnumerable<Type> ConcreteTypes(this Assembly instance)
        {
            return (instance == null) ?
                   Enumerable.Empty<Type>() :
                   instance.GetExportedTypes()
                           .Where(type => type.IsClass && !type.IsAbstract && !type.IsInterface && !type.IsGenericType);
        }

        /// <summary>
        /// Gets the concretes the types of the given assemblies.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        [DebuggerHidden]
        public static IEnumerable<Type> ConcreteTypes(this IEnumerable<Assembly> instance)
        {
            return (instance == null) ?
                   Enumerable.Empty<Type>() :
                   instance.SelectMany(assembly => assembly.ConcreteTypes());
        }
    }
}