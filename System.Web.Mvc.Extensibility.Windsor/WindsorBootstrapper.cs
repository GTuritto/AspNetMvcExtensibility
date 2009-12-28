#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Windsor
{
    using Collections.Generic;
    using Linq;
    using Routing;

    using Microsoft.Practices.ServiceLocation;
    using Castle.Core;
    using Castle.Windsor;

    /// <summary>
    /// Defines a <seealso cref="BootstrapperBase">Bootstrapper</seealso> which is backed by Windsor <seealso cref="IWindsorContainer">Container</seealso>.
    /// </summary>
    public class WindsorBootstrapper : BootstrapperBase
    {
        private static readonly Type moduleType = typeof(IModule);

        /// <summary>
        /// Initializes a new instance of the <see cref="WindsorBootstrapper"/> class.
        /// </summary>
        /// <param name="buildManager">The build manager.</param>
        public WindsorBootstrapper(IBuildManager buildManager) : base(buildManager)
        {
        }

        /// <summary>
        /// Creates the service locator.
        /// </summary>
        /// <returns></returns>
        protected override IServiceLocator CreateServiceLocator()
        {
            IWindsorContainer container = new WindsorContainer();
            WindsorServiceLocator serviceLocator = new WindsorServiceLocator(container);

            RegisterKnownTypes(container, BuildManager, serviceLocator);

            IEnumerable<Type> concreteTypes = BuildManager.ConcreteTypes;

            RegisterDynamicTypes(container, concreteTypes);
            RegisterModules(container, concreteTypes);

            return serviceLocator;
        }

        private static void RegisterKnownTypes(IWindsorContainer container, IBuildManager buildManager, WindsorServiceLocator serviceLocator)
        {
            container.Kernel.AddComponentInstance(typeof(IBuildManager).FullName, typeof(IBuildManager), buildManager);
            container.Kernel.AddComponentInstance(typeof(IServiceLocator).FullName, typeof(IServiceLocator), serviceLocator);
            container.Kernel.AddComponentInstance(typeof(IInjector).FullName, typeof(IInjector), serviceLocator);
            container.Kernel.AddComponentInstance(typeof(RouteCollection).FullName, RouteTable.Routes);
            container.Kernel.AddComponentInstance(typeof(ControllerBuilder).FullName, ControllerBuilder.Current);
            container.Kernel.AddComponentInstance(typeof(ModelBinderDictionary).FullName, ModelBinders.Binders);
            container.Kernel.AddComponentInstance(typeof(ViewEngineCollection).FullName, ViewEngines.Engines);

            container.AddComponentLifeStyle<IFilterRegistry, FilterRegistry>(LifestyleType.Singleton)
                     .AddComponentLifeStyle<IControllerFactory, ExtendedControllerFactory>(LifestyleType.Singleton)
                     .AddComponentLifeStyle<IActionInvoker, ExtendedControllerActionInvoker>(LifestyleType.Transient);

            #if (!MVC1)

            container.AddComponentLifeStyle<IModelMetadataRegistry, ModelMetadataRegistry>(LifestyleType.Singleton);
            container.AddComponentLifeStyle<IAreaManager, AreaManager>(LifestyleType.Singleton);

            #endif
        }

        private static void RegisterDynamicTypes(IWindsorContainer container, IEnumerable<Type> concreteTypes)
        {
            concreteTypes.Where(type => KnownTypes.BootstrapperTaskType.IsAssignableFrom(type))
                         .Each(type => container.AddComponentLifeStyle(type.FullName, KnownTypes.BootstrapperTaskType, type, LifestyleType.Singleton));

            concreteTypes.Where(type => KnownTypes.PerRequestTaskType.IsAssignableFrom(type))
                         .Each(type => container.AddComponentLifeStyle(type.FullName, KnownTypes.PerRequestTaskType, type, LifestyleType.Singleton));

            concreteTypes.Where(type => KnownTypes.ModelBinderType.IsAssignableFrom(type) && type.IsDefined(KnownTypes.BindingAttributeType, true))
                         .Each(type => container.AddComponentLifeStyle(type.FullName, KnownTypes.ModelBinderType, type, LifestyleType.Singleton));

            concreteTypes.Where(type => KnownTypes.ControllerType.IsAssignableFrom(type))
                         .Each(type => container.AddComponentLifeStyle(type.FullName, type, type, LifestyleType.Transient));

            concreteTypes.Where(type => KnownTypes.FilterAttributeType.IsAssignableFrom(type))
                         .Each(type => container.AddComponentLifeStyle(type.FullName, type, type, LifestyleType.Transient));

            concreteTypes.Where(type => KnownTypes.ViewEngineType.IsAssignableFrom(type))
                         .Each(type => container.AddComponentLifeStyle(type.FullName, type, type, LifestyleType.Singleton));

            #if (!MVC1)

            concreteTypes.Where(type => KnownTypes.ModelMetadataConfigurationType.IsAssignableFrom(type))
                         .Each(type => container.AddComponentLifeStyle(type.FullName, KnownTypes.ModelMetadataConfigurationType, type, LifestyleType.Transient));

            concreteTypes.Where(type => KnownTypes.ExtendedModelMetadataProviderType.IsAssignableFrom(type))
                         .Each(type => container.AddComponentLifeStyle(type.FullName, KnownTypes.ExtendedModelMetadataProviderType, type, LifestyleType.Singleton));

            concreteTypes.Where(type => KnownTypes.AreaType.IsAssignableFrom(type))
                         .Each(type => container.AddComponentLifeStyle(type.FullName, KnownTypes.AreaType, type, LifestyleType.Singleton));

            #endif
        }

        private static void RegisterModules(IWindsorContainer container, IEnumerable<Type> concreteTypes)
        {
            concreteTypes.Where(type => moduleType.IsAssignableFrom(type) && type.HasDefaultConstructor())
                         .Select(type => Activator.CreateInstance(type))
                         .Cast<IModule>()
                         .Each(module => module.Load(container));
        }
    }
}