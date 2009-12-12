namespace System.Web.Mvc.Extensibility.Unity
{
    using Collections.Generic;
    using Diagnostics;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

    public class UnityServiceLocator : ServiceLocatorImplBase, IInjection, IDisposable
    {
        private bool isDisposed;

        public UnityServiceLocator(IUnityContainer container)
        {
            Invariant.IsNotNull(container, "container");

            Container = container;
        }

        [DebuggerStepThrough]
        ~UnityServiceLocator()
        {
            Dispose(false);
        }

        public IUnityContainer Container
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
                Container.BuildUp(instance.GetType(), instance);
            }
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return string.IsNullOrEmpty(key) ? Container.Resolve(serviceType) : Container.Resolve(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            IEnumerable<object> namedInstances = Container.ResolveAll(serviceType);
            object defaultInstance = null;

            try
            {
                defaultInstance = Container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                // When default instance is missing
            }

            return (defaultInstance == null) ? namedInstances : new List<object>(namedInstances) { defaultInstance };
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