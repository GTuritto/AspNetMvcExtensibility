namespace System.Web.Mvc.Extensibility.Tests
{
    using Collections.Generic;

    using Microsoft.Practices.ServiceLocation;

    public abstract class FakeServiceLocator : IServiceLocator, IInjection, IDisposable
    {
        public abstract object GetInstance(Type serviceType);

        public abstract object GetInstance(Type serviceType, string key);

        public abstract IEnumerable<object> GetAllInstances(Type serviceType);

        public abstract TService GetInstance<TService>();

        public abstract TService GetInstance<TService>(string key);

        public abstract IEnumerable<TService> GetAllInstances<TService>();

        public abstract object GetService(Type serviceType);

        public abstract void Inject(object instance);

        public abstract void Dispose();
    }
}