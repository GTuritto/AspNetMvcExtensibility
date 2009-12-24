#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Unity
{
    using Collections.Generic;
    using Linq;
    using Routing;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

    public class UnityBootstrapper : BootstrapperBase
    {
        private static readonly Type moduleType = typeof(IModule);

        public UnityBootstrapper(IBuildManager buildManager) : base(buildManager)
        {
        }

        protected override IServiceLocator CreateServiceLocator()
        {
            IUnityContainer container = new UnityContainer();
            UnityServiceLocator serviceLocator = new UnityServiceLocator(container);

            RegisterKnwonTypes(container, BuildManager, serviceLocator);

            IEnumerable<Type> concreteTypes = BuildManager.ConcreteTypes;

            RegisterDynamicTypes(container, concreteTypes);
            RegisterModules(container, concreteTypes);

            return serviceLocator;
        }

        private static void RegisterKnwonTypes(IUnityContainer container, IBuildManager buildManager, UnityServiceLocator serviceLocator)
        {
            container.RegisterInstance(RouteTable.Routes)
                     .RegisterInstance(ControllerBuilder.Current)
                     .RegisterInstance(ModelBinders.Binders)
                     .RegisterInstance(ViewEngines.Engines)
                     .RegisterInstance(buildManager)
                     .RegisterInstance<IServiceLocator>(serviceLocator)
                     .RegisterInstance<IInjector>(serviceLocator)
                     .RegisterType<IFilterRegistry, FilterRegistry>(new ContainerControlledLifetimeManager())
                     .RegisterType<IModelMetadataRegistry, ModelMetadataRegistry>(new ContainerControlledLifetimeManager())
                     .RegisterType<IControllerFactory, ExtendedControllerFactory>(new ContainerControlledLifetimeManager())
                     .RegisterType<IActionInvoker, ExtendedControllerActionInvoker>();
        }

        private static void RegisterDynamicTypes(IUnityContainer container, IEnumerable<Type> concreteTypes)
        {
            concreteTypes.Where(type => KnownTypes.BootstrapperTaskType.IsAssignableFrom(type))
                         .Each(type => container.RegisterType(KnownTypes.BootstrapperTaskType, type, type.FullName, new ContainerControlledLifetimeManager()));

            concreteTypes.Where(type => KnownTypes.PerRequestTaskType.IsAssignableFrom(type))
                         .Each(type => container.RegisterType(KnownTypes.PerRequestTaskType, type, type.FullName, new ContainerControlledLifetimeManager()));

            concreteTypes.Where(type => KnownTypes.ModelBinderType.IsAssignableFrom(type) && type.IsDefined(KnownTypes.BindingAttributeType, true))
                         .Each(type => container.RegisterType(KnownTypes.ModelBinderType, type, type.FullName, new ContainerControlledLifetimeManager()));

            concreteTypes.Where(type => KnownTypes.ControllerType.IsAssignableFrom(type))
                         .Each(type => container.RegisterType(type, type));

            concreteTypes.Where(type => KnownTypes.FilterAttributeType.IsAssignableFrom(type))
                         .Each(type => container.RegisterType(type, type));

            concreteTypes.Where(type => KnownTypes.ViewEngineType.IsAssignableFrom(type))
                         .Each(type => container.RegisterType(KnownTypes.ViewEngineType, type, type.FullName, new ContainerControlledLifetimeManager()));
        }

        private static void RegisterModules(IUnityContainer container, IEnumerable<Type> concreteTypes)
        {
            concreteTypes.Where(type => moduleType.IsAssignableFrom(type) && type.HasDefaultConstructor())
                         .Select(type => Activator.CreateInstance(type))
                         .Cast<IModule>()
                         .Each(module => module.Load(container));
        }
    }
}