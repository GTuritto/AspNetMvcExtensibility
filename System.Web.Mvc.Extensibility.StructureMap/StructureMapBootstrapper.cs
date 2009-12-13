namespace System.Web.Mvc.Extensibility.StructureMap
{
    using Collections.Generic;
    using Linq;
    using Routing;

    using Microsoft.Practices.ServiceLocation;

    using Container = global::StructureMap.Container;
    using IContainer = global::StructureMap.IContainer;
    using InstanceScope = global::StructureMap.Attributes.InstanceScope;
    using Registry = global::StructureMap.Configuration.DSL.Registry;

    public class StructureMapBootstrapper : BootstrapperBase
    {
        private static readonly Type registryType = typeof(Registry);

        protected override IServiceLocator CreateServiceLocator()
        {
            IContainer container = new Container();
            IServiceLocator serviceLocator = new StructureMapServiceLocator(container);

            container.Configure(x =>
                                {
                                    x.ForRequestedType<IServiceLocator>().TheDefault.IsThis(serviceLocator);
                                    x.ForRequestedType<RouteCollection>().TheDefault.IsThis(RouteTable.Routes);
                                    x.ForRequestedType<ControllerBuilder>().TheDefault.IsThis(ControllerBuilder.Current);
                                    x.ForRequestedType<ModelBinderDictionary>().TheDefault.IsThis(ModelBinders.Binders);
                                    x.ForRequestedType<ViewEngineCollection>().TheDefault.IsThis(ViewEngines.Engines);
                                    x.ForRequestedType<IFilterRegistry>().CacheBy(InstanceScope.Singleton).TheDefaultIsConcreteType<FilterRegistry>();
                                    x.ForRequestedType<IControllerFactory>().TheDefaultIsConcreteType<ExtendedControllerFactory>();
                                    x.ForRequestedType<IActionInvoker>().TheDefaultIsConcreteType<ExtendedControllerActionInvoker>();
                                });

            IEnumerable<Type> concreteTypes = ReferencedAssemblies.ConcreteTypes();

            concreteTypes.Where(type => KnownTypes.BootstrapperTaskType.IsAssignableFrom(type))
                         .Each(type => container.Configure(x => x.ForRequestedType(KnownTypes.BootstrapperTaskType).AddType(type)));

            concreteTypes.Where(type => KnownTypes.ModelBinderType.IsAssignableFrom(type) && type.IsDefined(KnownTypes.BindingAttributeType, true))
                         .Each(type => container.Configure(x => x.ForRequestedType(KnownTypes.ModelBinderType).CacheBy(InstanceScope.Singleton).AddType(type)));

            concreteTypes.Where(type => KnownTypes.ControllerType.IsAssignableFrom(type))
                         .Each(type => container.Configure(x => x.ForRequestedType(type).TheDefaultIsConcreteType(type)));

            concreteTypes.Where(type => KnownTypes.FilterAttributeType.IsAssignableFrom(type))
                         .Each(type => container.Configure(x => x.ForRequestedType(type).TheDefaultIsConcreteType(type)));

            concreteTypes.Where(type => KnownTypes.ViewEngineType.IsAssignableFrom(type))
                         .Each(type => container.Configure(x => x.ForRequestedType(KnownTypes.ViewEngineType).CacheBy(InstanceScope.Singleton).AddType(type)));

            concreteTypes.Where(type => registryType.IsAssignableFrom(type) && type.HasDefaultConstructor())
                         .Select(type => Activator.CreateInstance(type))
                         .Cast<Registry>()
                         .Each(registry => container.Configure(x => x.AddRegistry(registry)));

            return serviceLocator;
        }
    }
}