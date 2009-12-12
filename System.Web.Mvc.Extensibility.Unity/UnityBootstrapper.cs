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

            container.RegisterInstance(RouteTable.Routes);
            container.RegisterInstance(ControllerBuilder.Current);
            container.RegisterInstance(ModelBinders.Binders);
            container.RegisterInstance(ViewEngines.Engines);

            IEnumerable<Type> concreteTypes = ReferencedAssemblies.ConcreteTypes();

            concreteTypes.Where(type => KnownTypes.BootstrapperTaskType.IsAssignableFrom(type))
                         .Each(type => container.RegisterType(KnownTypes.BootstrapperTaskType, type, type.FullName));

            concreteTypes.Where(type => KnownTypes.ModelBinderType.IsAssignableFrom(type) && type.IsDefined(KnownTypes.BindingAttributeType, true))
                         .Each(type => container.RegisterType(KnownTypes.ModelBinderType, type, type.FullName, new ContainerControlledLifetimeManager()));

            concreteTypes.Where(type => KnownTypes.ControllerType.IsAssignableFrom(type))
                         .Each(type => container.RegisterType(type, type));

            concreteTypes.Where(type => KnownTypes.ViewEngineType.IsAssignableFrom(type))
                         .Each(type => container.RegisterType(KnownTypes.ViewEngineType, type, type.FullName, new ContainerControlledLifetimeManager()));

            concreteTypes.Where(type => moduleType.IsAssignableFrom(type) && type.HasDefaultConstructor())
                         .Select(type => Activator.CreateInstance(type))
                         .Cast<IModule>()
                         .Each(module => module.Load(container));

            return new UnityServiceLocator(container);
        }
    }
}