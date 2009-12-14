#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Ninject
{
    using Collections.Generic;
    using Diagnostics;

    using Microsoft.Practices.ServiceLocation;
    using Extension = global::Ninject.ResolutionExtensions;
    using IKernel = global::Ninject.IKernel;

    public class NinjectServiceLocator : ServiceLocatorImplBase, IInjection, IDisposable
    {
        private bool isDisposed;

        public NinjectServiceLocator(IKernel kernel)
        {
            Invariant.IsNotNull(kernel, "kernel");

            Kernel = kernel;
        }

        [DebuggerStepThrough]
        ~NinjectServiceLocator()
        {
            Dispose(false);
        }

        public IKernel Kernel
        {
            get;
            private set;
        }

        [DebuggerStepThrough]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Inject(object instance)
        {
            if (instance != null)
            {
                Kernel.Inject(instance);
            }
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return Extension.Get(Kernel, serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return Extension.GetAll(Kernel, serviceType);
        }

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