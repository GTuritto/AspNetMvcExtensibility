namespace System.Web.Mvc.Extensibility.Autofac
{
    using Collections.Generic;
    using Linq;
    using Routing;

    using Microsoft.Practices.ServiceLocation;

    using CollectionModule = global::Autofac.Modules.ImplicitCollectionSupportModule;
    using ContainerBuilder = global::Autofac.Builder.ContainerBuilder;
    using IContainer = global::Autofac.IContainer;
    using IModule = global::Autofac.IModule;

    public class AutofacBootstrapper : BootstrapperBase
    {
        private static readonly Type moduleType = typeof(IModule);

        protected override IServiceLocator CreateServiceLocator()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.Register(RouteTable.Routes);
            builder.Register(ControllerBuilder.Current);
            builder.Register(ModelBinders.Binders);
            builder.Register(ViewEngines.Engines);

            builder.RegisterModule(new CollectionModule());

            IEnumerable<Type> concreteTypes = ReferencedAssemblies.ConcreteTypes();

            concreteTypes.Where(type => KnownTypes.BootstrapperTaskType.IsAssignableFrom(type))
                         .Each(type => builder.Register(type).As(KnownTypes.BootstrapperTaskType).FactoryScoped());

            concreteTypes.Where(type => KnownTypes.ModelBinderType.IsAssignableFrom(type) && type.IsDefined(KnownTypes.BindingAttributeType, true))
                         .Each(type => builder.Register(type).As(KnownTypes.ModelBinderType).ContainerScoped());

            concreteTypes.Where(type => KnownTypes.ControllerType.IsAssignableFrom(type))
                         .Each(type => builder.Register(type).FactoryScoped());

            concreteTypes.Where(type => KnownTypes.ViewEngineType.IsAssignableFrom(type))
                         .Each(type => builder.Register(type).As(KnownTypes.ViewEngineType).ContainerScoped());

            concreteTypes.Where(type => moduleType.IsAssignableFrom(type) && type.HasDefaultConstructor())
                         .Where(type => !type.Namespace.StartsWith("Autofac", StringComparison.OrdinalIgnoreCase))
                         .Each(type => builder.RegisterModule(Activator.CreateInstance(type) as IModule));

            IContainer container = builder.Build();

            return new AutofacServiceLocator(container);
        }
    }
}