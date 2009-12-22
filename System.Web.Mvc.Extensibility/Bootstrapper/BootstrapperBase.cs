#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Diagnostics;

    using Microsoft.Practices.ServiceLocation;
    using CurrentLocator = Microsoft.Practices.ServiceLocation.ServiceLocator;

    public abstract class BootstrapperBase : DisposableBase, IBootstrapper
    {
        private IServiceLocator serviceLocator;

        protected BootstrapperBase(IBuildManager buildManager)
        {
            Invariant.IsNotNull(buildManager, "buildManager");

            BuildManager = buildManager;
        }

        public IServiceLocator ServiceLocator
        {
            [DebuggerStepThrough]
            get
            {
                return serviceLocator ?? (serviceLocator = CreateAndSetCurrent());
            }
        }

        protected IBuildManager BuildManager
        {
            get;
            private set;
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