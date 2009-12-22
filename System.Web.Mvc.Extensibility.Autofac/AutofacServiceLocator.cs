#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Autofac
{
    using Collections;
    using Collections.Generic;
    using Diagnostics;
    using Linq;

    using Microsoft.Practices.ServiceLocation;
    using IContainer = global::Autofac.IContainer;

    public class AutofacServiceLocator : ServiceLocatorImplBase, IInjector, IDisposable
    {
        private static readonly Type genericEnumerableType = typeof(IEnumerable<>);

        private bool isDisposed;

        [DebuggerStepThrough]
        ~AutofacServiceLocator()
        {
            Dispose(false);
        }

        public IContainer Container
        {
            get;
            set;
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
                Container.InjectProperties(instance);
            }
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return string.IsNullOrEmpty(key) ? Container.Resolve(serviceType) : Container.Resolve(key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            Type type = genericEnumerableType.MakeGenericType(serviceType);

            object instances;

            return Container.TryResolve(type, out instances) ?
                   ((IEnumerable) instances).Cast<object>() :
                   Enumerable.Empty<object>();
        }

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