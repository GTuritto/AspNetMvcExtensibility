#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    /// <summary>
    /// Defines an static class which contains extension methods of <see cref="IRegistrar"/>.
    /// </summary>
    public static class RegistrarExtensions
    {
        /// <summary>
        /// Registers the instance as a singleton service.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        public static IRegistrar RegisterInstance<TService>(this IRegistrar instance, object service)
        {
            return instance.RegisterInstance(null, typeof(TService), service);
        }

        /// <summary>
        /// Registers the service as singleton.
        /// </summary>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static IRegistrar RegisterAsSingleton<TImplementation>(this IRegistrar instance) where TImplementation : class
        {
            return RegisterAsSingleton<TImplementation, TImplementation>(instance);
        }

        /// <summary>
        /// Registers as singleton.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static IRegistrar RegisterAsSingleton<TService, TImplementation>(this IRegistrar instance) where TImplementation : TService where TService : class
        {
            return RegisterType<TService, TImplementation>(instance, true);
        }

        /// <summary>
        /// Registers the service as singleton.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        /// <returns></returns>
        public static IRegistrar RegisterAsSingleton(this IRegistrar instance, Type implementationType)
        {
            return RegisterAsSingleton(instance, implementationType, implementationType);
        }

        /// <summary>
        /// Registers the service as singleton.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        /// <returns></returns>
        public static IRegistrar RegisterAsSingleton(this IRegistrar instance, Type serviceType, Type implementationType)
        {
            return instance.RegisterType(null, serviceType, implementationType, true);
        }

        /// <summary>
        /// Registers the service as transient.
        /// </summary>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static IRegistrar RegisterAsTransient<TImplementation>(this IRegistrar instance) where TImplementation : class
        {
            return RegisterAsTransient<TImplementation, TImplementation>(instance);
        }

        /// <summary>
        /// Registers the service as transient.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static IRegistrar RegisterAsTransient<TService, TImplementation>(this IRegistrar instance) where TImplementation : TService where TService : class
        {
            return RegisterType<TService, TImplementation>(instance, false);
        }

        /// <summary>
        /// Registers the service as transient.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        /// <returns></returns>
        public static IRegistrar RegisterAsTransient(this IRegistrar instance, Type implementationType)
        {
            return RegisterAsTransient(instance, implementationType, implementationType);
        }

        /// <summary>
        /// Registers the service as transient.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        /// <returns></returns>
        public static IRegistrar RegisterAsTransient(this IRegistrar instance, Type serviceType, Type implementationType)
        {
            return instance.RegisterType(null, serviceType, implementationType, false);
        }

        private static IRegistrar RegisterType<TService, TImplementation>(this IRegistrar instance, bool asSingleton) where TImplementation : TService where TService : class
        {
            return instance.RegisterType(null, typeof(TService), typeof(TImplementation), asSingleton);
        }
    }
}