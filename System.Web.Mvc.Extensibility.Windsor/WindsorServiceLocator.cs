namespace System.Web.Mvc.Extensibility.Windsor
{
    using Collections.Generic;
    using Diagnostics;
    using Linq;
    using Reflection;

    using Microsoft.Practices.ServiceLocation;
    using Castle.Windsor;

    public class WindsorServiceLocator : ServiceLocatorImplBase, IInjection, IDisposable
    {
        private bool isDisposed;

        public WindsorServiceLocator(IWindsorContainer container)
        {
            Invariant.IsNotNull(container, "container");

            Container = container;
        }

        [DebuggerStepThrough]
        ~WindsorServiceLocator()
        {
            Dispose(false);
        }

        public IWindsorContainer Container
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

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return string.IsNullOrEmpty(key) ? Container.Resolve(serviceType) : Container.Resolve(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return Container.ResolveAll(serviceType).Cast<object>();
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