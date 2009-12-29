#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Unity
{
    using Microsoft.Practices.Unity;

    /// <summary>
    /// Defines an static class which contains extension methods of <see cref="IUnityContainer"/>.
    /// </summary>
    public static class UnityContainerExtensions
    {
        /// <summary>
        /// Determines whether the specified instance is registered.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="type">The type.</param>
        /// <returns>
        /// <c>true</c> if the specified instance is registered; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsRegistered(this IUnityContainer instance, Type type)
        {
            Invariant.IsNotNull(instance, "instance");
            Invariant.IsNotNull(type, "type");

            return InternalUnityExtension.IsRegistered(instance, type);
        }
    }
}