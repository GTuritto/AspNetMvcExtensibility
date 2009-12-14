#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using Diagnostics;
    using Linq;
    using Reflection;

    public static class TypeExtensions
    {
        [DebuggerHidden]
        public static bool HasDefaultConstructor(this Type instance)
        {
            Invariant.IsNotNull(instance, "instance");

            return instance.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                           .Any(ctor => ctor.GetParameters().Length == 0);
        }

        [DebuggerHidden]
        public static IEnumerable<Type> ConcreteTypes(this IEnumerable<Assembly> instance)
        {
            return (instance == null) ?
                   Enumerable.Empty<Type>() :
                   instance.SelectMany(assembly => assembly.GetExportedTypes())
                           .Where(type => type.IsClass && !type.IsAbstract && !type.IsGenericType);
        }
    }
}