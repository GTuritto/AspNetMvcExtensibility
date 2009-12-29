#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using Diagnostics;
    using Linq;
    using Routing;

    using Microsoft.Practices.ServiceLocation;
    using CurrentLocator = Microsoft.Practices.ServiceLocation.ServiceLocator;

    /// <summary>
    /// Defines a base class which is used for executing application startup and cleanup tasks.
    /// </summary>
    public abstract class BootstrapperBase : DisposableBase, IBootstrapper
    {
        private readonly object syncLock = new object();
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
                if (serviceLocator == null)
                {
                    lock (syncLock)
                    {
                        if (serviceLocator == null)
                        {
                            serviceLocator = CreateAndSetCurrent();
                            LoadModules();
                        }
                    }
                }

                return serviceLocator;
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
        /// Loads the container specific modules.
        /// </summary>
        protected abstract void LoadModules();

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

        private static void RegisterKnownTypes(IRegistrar registrar, IServiceLocator serviceLocator, IBuildManager buildManager)
        {
            registrar.RegisterInstance<RouteCollection>(RouteTable.Routes)
                     .RegisterInstance<ControllerBuilder>(ControllerBuilder.Current)
                     .RegisterInstance<ModelBinderDictionary>(ModelBinders.Binders)
                     .RegisterInstance<ViewEngineCollection>(ViewEngines.Engines)
                     .RegisterInstance<IBuildManager>(buildManager)
                     .RegisterInstance<IRegistrar>(registrar)
                     .RegisterInstance<IServiceLocator>(serviceLocator)
                     .RegisterAsSingleton<IEventAggregator, EventAggregator>()
                     .RegisterAsSingleton<IFilterRegistry, FilterRegistry>()
                     .RegisterAsSingleton<IControllerFactory, ExtendedControllerFactory>()
                     .RegisterAsTransient<IActionInvoker, ExtendedControllerActionInvoker>();

            if (serviceLocator is IInjector)
            {
                registrar.RegisterInstance<IInjector>(serviceLocator);
            }

            #if (!MVC1)

            registrar.RegisterAsSingleton<CompositeModelMetadataProvider>()
                     .RegisterAsSingleton<IModelMetadataRegistry, ModelMetadataRegistry>();

            #endif
        }

        private static void RegisterUnknownTypes(IRegistrar registrar, IEnumerable<Type> concreteTypes)
        {
            concreteTypes.Where(type => KnownTypes.BootstrapperTaskType.IsAssignableFrom(type))
                         .Each(type => registrar.RegisterAsSingleton(KnownTypes.BootstrapperTaskType, type));

            concreteTypes.Where(type => KnownTypes.PerRequestTaskType.IsAssignableFrom(type))
                         .Each(type => registrar.RegisterAsSingleton(KnownTypes.PerRequestTaskType, type));

            concreteTypes.Where(type => KnownTypes.ModelBinderType.IsAssignableFrom(type) && type.IsDefined(KnownTypes.BindingAttributeType, true))
                         .Each(type => registrar.RegisterAsSingleton(KnownTypes.ModelBinderType, type));

            concreteTypes.Where(type => KnownTypes.ControllerType.IsAssignableFrom(type))
                         .Each(type => registrar.RegisterAsTransient(type));

            concreteTypes.Where(type => KnownTypes.FilterAttributeType.IsAssignableFrom(type))
                         .Each(type => registrar.RegisterAsTransient(type));

            concreteTypes.Where(type => KnownTypes.ViewEngineType.IsAssignableFrom(type))
                         .Each(type => registrar.RegisterAsSingleton(KnownTypes.ViewEngineType, type));

            #if (!MVC1)

            concreteTypes.Where(type => KnownTypes.ModelMetadataConfigurationType.IsAssignableFrom(type))
                         .Each(type => registrar.RegisterAsTransient(KnownTypes.ModelMetadataConfigurationType, type));

            concreteTypes.Where(type => KnownTypes.ExtendedModelMetadataProviderType.IsAssignableFrom(type))
                         .Each(type => registrar.RegisterAsSingleton(KnownTypes.ExtendedModelMetadataProviderType, type));

            concreteTypes.Where(type => KnownTypes.ModelValidatorProviderType.IsAssignableFrom(type))
                         .Where(type => !KnownTypes.BuiltInModelValidatorProviderTypes.Any(builtInType => builtInType == type))
                         .Each(type => registrar.RegisterAsSingleton(KnownTypes.ModelValidatorProviderType, type));

            #endif
        }

        private IServiceLocator CreateAndSetCurrent()
        {
            IServiceLocator locator = CreateServiceLocator();
            IRegistrar registrar = locator as IRegistrar;

            if (registrar != null)
            {
                RegisterKnownTypes(registrar, locator, BuildManager);
                RegisterUnknownTypes(registrar, BuildManager.ConcreteTypes);
            }

            CurrentLocator.SetLocatorProvider(() => locator);

            return locator;
        }
    }
}