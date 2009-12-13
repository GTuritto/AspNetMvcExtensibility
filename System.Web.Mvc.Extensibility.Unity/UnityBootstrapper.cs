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

        protected override IServiceLocator CreateServiceLocator()
        {
            IUnityContainer container = new UnityContainer();
            IServiceLocator serviceLocator = new UnityServiceLocator(container);

            RegisterKnwonTypes(container, serviceLocator);

            IEnumerable<Type> concreteTypes = ReferencedAssemblies.ConcreteTypes();

            RegisterDynamicTypes(container, concreteTypes);
            RegisterModules(container, concreteTypes);

            return serviceLocator;
        }

        private static void RegisterKnwonTypes(IUnityContainer container, IServiceLocator serviceLocator)
        {
            container.RegisterInstance(RouteTable.Routes)
                     .RegisterInstance(ControllerBuilder.Current)
                     .RegisterInstance(ModelBinders.Binders)
                     .RegisterInstance(ViewEngines.Engines)
                     .RegisterInstance(serviceLocator)
                     .RegisterType<IFilterRegistry, FilterRegistry>(new ContainerControlledLifetimeManager())
                     .RegisterType<IControllerFactory, ExtendedControllerFactory>()
                     .RegisterType<IActionInvoker, ExtendedControllerActionInvoker>();
        }

        private static void RegisterDynamicTypes(IUnityContainer container, IEnumerable<Type> concreteTypes)
        {
            concreteTypes.Where(type => KnownTypes.BootstrapperTaskType.IsAssignableFrom(type))
                         .Each(type => container.RegisterType(KnownTypes.BootstrapperTaskType, type, type.FullName));

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