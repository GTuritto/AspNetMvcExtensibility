#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Autofac
{
    using Collections;
    using Collections.Generic;
    using Diagnostics;
    using Linq;

    using Microsoft.Practices.ServiceLocation;
    using IContainer = global::Autofac.IContainer;

    /// <summary>
    /// Defines a <seealso cref="IServiceLocator">service locator</seealso> which with backed by Autofac <seealso cref="IContainer">Container</seealso>.
    /// </summary>
    public class AutofacServiceLocator : ServiceLocatorImplBase, IInjector, IDisposable
    {
        private static readonly Type genericEnumerableType = typeof(IEnumerable<>);

        private bool isDisposed;

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="AutofacServiceLocator"/> is reclaimed by garbage collection.
        /// </summary>
        [DebuggerStepThrough]
        ~AutofacServiceLocator()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>The container.</value>
        public IContainer Container
        {
            get;
            set;
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
        /// Injects the matching dependences.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public virtual void Inject(object instance)
        {
            if (instance != null)
            {
                Container.InjectProperties(instance);
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
            return string.IsNullOrEmpty(key) ? Container.Resolve(serviceType) : Container.Resolve(key);
        }

        /// <summary>
        /// Gets all the instances for the given type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            Type type = genericEnumerableType.MakeGenericType(serviceType);

            object instances;

            return Container.TryResolve(type, out instances) ?
                   ((IEnumerable) instances).Cast<object>() :
                   Enumerable.Empty<object>();
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
                if (Container != null)
                {
                    Container.Dispose();
                }
            }

            isDisposed = true;
        }
    }
}