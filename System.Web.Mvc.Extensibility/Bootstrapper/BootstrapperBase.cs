#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Diagnostics;

    using Microsoft.Practices.ServiceLocation;
    using CurrentLocator = Microsoft.Practices.ServiceLocation.ServiceLocator;

    /// <summary>
    /// Defines a base class to execute <seealso cref="IBootstrapperTask"/>.
    /// </summary>
    public abstract class BootstrapperBase : DisposableBase, IBootstrapper
    {
        private IServiceLocator serviceLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="BootstrapperBase"/> class.
        /// </summary>
        /// <param name="buildManager">The build manager.</param>
        protected BootstrapperBase(IBuildManager buildManager)
        {
            Invariant.IsNotNull(buildManager, "buildManager");

            BuildManager = buildManager;
        }

        /// <summary>
        /// Gets the service locator.
        /// </summary>
        /// <value>The service locator.</value>
        public IServiceLocator ServiceLocator
        {
            [DebuggerStepThrough]
            get
            {
                return serviceLocator ?? (serviceLocator = CreateAndSetCurrent());
            }
        }

        /// <summary>
        /// Gets or the build manager.
        /// </summary>
        /// <value>The build manager.</value>
        protected IBuildManager BuildManager
        {
            get;
            private set;
        }

        /// <summary>
        /// Executes the <seealso cref="IBootstrapperTask"/>.
        /// </summary>
        public void Execute()
        {
            ServiceLocator.GetAllInstances<IBootstrapperTask>()
                          .Each(task => task.Execute(ServiceLocator));
        }

        /// <summary>
        /// Creates the service locator.
        /// </summary>
        /// <returns></returns>
        protected abstract IServiceLocator CreateServiceLocator();

        /// <summary>
        /// Disposes the resources.
        /// </summary>
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