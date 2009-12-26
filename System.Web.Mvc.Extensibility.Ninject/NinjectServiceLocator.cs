#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Ninject
{
    using Collections.Generic;
    using Diagnostics;

    using Microsoft.Practices.ServiceLocation;
    using Extension = global::Ninject.ResolutionExtensions;
    using IKernel = global::Ninject.IKernel;

    /// <summary>
    /// Defines a <seealso cref="IServiceLocator">service locator</seealso> which with backed by Ninject <seealso cref="IKernel">Kernel</seealso>.
    /// </summary>
    public class NinjectServiceLocator : ServiceLocatorImplBase, IInjector, IDisposable
    {
        private bool isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectServiceLocator"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public NinjectServiceLocator(IKernel kernel)
        {
            Invariant.IsNotNull(kernel, "kernel");

            Kernel = kernel;
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="NinjectServiceLocator"/> is reclaimed by garbage collection.
        /// </summary>
        [DebuggerStepThrough]
        ~NinjectServiceLocator()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets the kernel.
        /// </summary>
        /// <value>The kernel.</value>
        public IKernel Kernel
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
        /// Injects the matching dependences.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public virtual void Inject(object instance)
        {
            if (instance != null)
            {
                Kernel.Inject(instance);
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
            return Extension.Get(Kernel, serviceType, key);
        }

        /// <summary>
        /// Gets all the instances for the given type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return Extension.GetAll(Kernel, serviceType);
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
                Kernel.Dispose();
            }

            isDisposed = true;
        }
    }
}