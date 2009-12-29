#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Windsor
{
    using Collections.Generic;
    using Diagnostics;
    using Linq;
    using Reflection;

    using Microsoft.Practices.ServiceLocation;

    using Castle.Core;
    using Castle.Windsor;

    /// <summary>
    /// Defines an adapter class which with backed by Windsor <seealso cref="IWindsorContainer">Container</seealso>.
    /// </summary>
    public class WindsorAdapter : ServiceLocatorImplBase, IRegistrar, IInjector, IDisposable
    {
        private bool isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindsorAdapter"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public WindsorAdapter(IWindsorContainer container)
        {
            Invariant.IsNotNull(container, "container");

            Container = container;
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="WindsorAdapter"/> is reclaimed by garbage collection.
        /// </summary>
        [DebuggerStepThrough]
        ~WindsorAdapter()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>The container.</value>
        public IWindsorContainer Container
        {
            get;
            private set;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        [DebuggerStepThrough]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Registers the type.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        /// <param name="asSingleton">if set to <c>true</c> [as singleton].</param>
        /// <returns></returns>
        public virtual IRegistrar RegisterType(string key, Type serviceType, Type implementationType, bool asSingleton)
        {
            Invariant.IsNotNull(serviceType, "serviceType");
            Invariant.IsNotNull(implementationType, "implementationType");

            key = key ?? MakeKey(serviceType, implementationType);
            LifestyleType lifestyle = asSingleton ? LifestyleType.Singleton : LifestyleType.Transient;

            Container.AddComponentLifeStyle(key, serviceType, implementationType, lifestyle);

            return this;
        }

        /// <summary>
        /// Registers the instance.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public virtual IRegistrar RegisterInstance(string key, Type serviceType, object instance)
        {
            Invariant.IsNotNull(serviceType, "serviceType");
            Invariant.IsNotNull(instance, "instance");

            key = key ?? MakeKey(serviceType, instance.GetType());

            Container.Kernel.AddComponentInstance(key, serviceType, instance);

            return this;
        }

        /// <summary>
        /// Injects the matching dependences.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public virtual void Inject(object instance)
        {
            if (instance != null)
            {
                Func<PropertyInfo, bool> isMatching = property =>
                {
                    if (property.CanWrite)
                    {
                        MethodInfo setMethod = property.GetSetMethod();

                        if ((setMethod != null) && (setMethod.GetParameters().Length == 1))
                        {
                            if (Container.Kernel.HasComponent(property.PropertyType))
                            {
                                return true;
                            }
                        }
                    }

                    return false;
                };

                instance.GetType()
                        .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty)
                        .Where(isMatching)
                        .Each(property => property.SetValue(instance, Container.Resolve(property.PropertyType), null));
            }
        }

        /// <summary>
        /// Gets the matching instance for the given type and key.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected override object DoGetInstance(Type serviceType, string key)
        {
            return string.IsNullOrEmpty(key) ? Container.Resolve(serviceType) : Container.Resolve(serviceType, key);
        }

        /// <summary>
        /// Gets all the instances for the given type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return Container.ResolveAll(serviceType).Cast<object>();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        [DebuggerStepThrough]
        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
            {
                Container.Dispose();
            }

            isDisposed = true;
        }

        private static string MakeKey(Type serviceType, Type implementationType)
        {
            return serviceType.FullName + "->" + implementationType.FullName;
        }
    }
}