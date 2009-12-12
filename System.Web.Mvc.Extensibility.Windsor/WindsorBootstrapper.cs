namespace System.Web.Mvc.Extensibility.Windsor
{
    using Collections.Generic;
    using Linq;
    using Routing;

    using Microsoft.Practices.ServiceLocation;
    using Castle.Core;
    using Castle.Windsor;

    public class WindsorBootstrapper : BootstrapperBase
    {
        private static readonly Type moduleType = typeof(IModule);

        protected override IServiceLocator CreateServiceLocator()
        {
            IWindsorContainer container = new WindsorContainer();
            IServiceLocator serviceLocator = new WindsorServiceLocator(container);

            container.Kernel.AddComponentInstance(typeof(IServiceLocator).FullName, typeof(IServiceLocator), serviceLocator);
            container.Kernel.AddComponentInstance(typeof(RouteCollection).FullName, RouteTable.Routes);
            container.Kernel.AddComponentInstance(typeof(ControllerBuilder).FullName, ControllerBuilder.Current);
            container.Kernel.AddComponentInstance(typeof(ModelBinderDictionary).FullName, ModelBinders.Binders);
            container.Kernel.AddComponentInstance(typeof(ViewEngineCollection).FullName, ViewEngines.Engines);

            container.AddComponentLifeStyle(typeof(IControllerFactory).FullName, typeof(IControllerFactory), typeof(ExtendedControllerFactory), LifestyleType.Transient)
                     .AddComponentLifeStyle(typeof(IActionInvoker).FullName, typeof(IActionInvoker), typeof(ExtendedControllerActionInvoker), LifestyleType.Transient);

            IEnumerable<Type> concreteTypes = ReferencedAssemblies.ConcreteTypes();

            concreteTypes.Where(type => KnownTypes.BootstrapperTaskType.IsAssignableFrom(type))
                         .Each(type => container.AddComponentLifeStyle(type.FullName, KnownTypes.BootstrapperTaskType, type, LifestyleType.Transient));

            concreteTypes.Where(type => KnownTypes.ModelBinderType.IsAssignableFrom(type) && type.IsDefined(KnownTypes.BindingAttributeType, true))
                         .Each(type => container.AddComponentLifeStyle(type.FullName, KnownTypes.ModelBinderType, type, LifestyleType.Singleton));

            concreteTypes.Where(type => KnownTypes.ControllerType.IsAssignableFrom(type))
                         .Each(type => container.AddComponentLifeStyle(type.FullName, type, type, LifestyleType.Transient));

            concreteTypes.Where(type => KnownTypes.ViewEngineType.IsAssignableFrom(type))
                         .Each(type => container.AddComponentLifeStyle(type.FullName, type, type, LifestyleType.Singleton));

            concreteTypes.Where(type => moduleType.IsAssignableFrom(type) && type.HasDefaultConstructor())
                         .Select(type => Activator.CreateInstance(type))
                         .Cast<IModule>()
                         .Each(module => module.Load(container));

            return serviceLocator;
        }
    }
}