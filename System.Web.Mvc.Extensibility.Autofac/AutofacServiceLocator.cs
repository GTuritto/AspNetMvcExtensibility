namespace System.Web.Mvc.Extensibility.Autofac
{
    using Collections;
    using Collections.Generic;
    using Diagnostics;
    using Linq;

    using Microsoft.Practices.ServiceLocation;
    using IContainer = global::Autofac.IContainer;

    public class AutofacServiceLocator : ServiceLocatorImplBase, IInjection, IDisposable
    {
        private bool isDisposed;

        public AutofacServiceLocator(IContainer container)
        {
            Invariant.IsNotNull(container, "container");

            Container = container;
        }

        [DebuggerStepThrough]
        ~AutofacServiceLocator()
        {
            Dispose(false);
        }

        public IContainer Container
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
                Container.InjectProperties(instance);
            }
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return string.IsNullOrEmpty(key) ? Container.Resolve(serviceType) : Container.Resolve(key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            Type type = typeof(IEnumerable<>).MakeGenericType(serviceType);

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
                Container.Dispose();
            }

            isDisposed = true;
        }
    }
}