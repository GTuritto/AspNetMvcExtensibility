#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using Compilation;
    using Diagnostics;
    using Linq;
    using Reflection;

    using Microsoft.Practices.ServiceLocation;
    using CurrentLocator = Microsoft.Practices.ServiceLocation.ServiceLocator;

    public abstract class BootstrapperBase : DisposableBase, IBootstrapper
    {
        private static readonly Func<IEnumerable<Assembly>> getReferencedAssemblies = () => BuildManager.GetReferencedAssemblies().Cast<Assembly>();
        private IEnumerable<Assembly> referencedAssemblies;

        private IServiceLocator serviceLocator;

        public IServiceLocator ServiceLocator
        {
            [DebuggerStepThrough]
            get
            {
                return serviceLocator ?? (serviceLocator = CreateAndSetCurrent());
            }
        }

        protected virtual IEnumerable<Assembly> ReferencedAssemblies
        {
            [DebuggerStepThrough]
            get
            {
                return referencedAssemblies ?? (referencedAssemblies = getReferencedAssemblies());
            }
        }

        public void Execute()
        {
            ServiceLocator.GetAllInstances<IBootstrapperTask>()
                          .Each(task => task.Execute(ServiceLocator));
        }

        protected abstract IServiceLocator CreateServiceLocator();

        protected override void DisposeCore()
        {
            if (serviceLocator != null)
            {
                serviceLocator.GetAllInstances<IBootstrapperTask>()
                              .Each(task => task.Dispose());

                IDisposable disposable = serviceLocator as IDisposable;

                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }

        private IServiceLocator CreateAndSetCurrent()
        {
            IServiceLocator locator = CreateServiceLocator();
            CurrentLocator.SetLocatorProvider(() => locator);

            return locator;
        }
    }
}