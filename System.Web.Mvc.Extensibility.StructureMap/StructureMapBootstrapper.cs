#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

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

        public StructureMapBootstrapper(IBuildManager buildManager) : base(buildManager)
        {
        }

        protected override IServiceLocator CreateServiceLocator()
        {
            IContainer container = new Container();
            StructureMapServiceLocator serviceLocator = new StructureMapServiceLocator(container);

            RegisterKnownTypes(container, BuildManager, serviceLocator);

            IEnumerable<Type> concreteTypes = BuildManager.ConcreteTypes;

            RegisterDynamicTypes(container, concreteTypes);
            RegisterRegistry(container, concreteTypes);

            return serviceLocator;
        }

        private static void RegisterKnownTypes(IContainer container, IBuildManager buildManager, StructureMapServiceLocator serviceLocator)
        {
            container.Configure(x =>
                                {
                                    x.ForRequestedType<IBuildManager>().TheDefault.IsThis(buildManager);
                                    x.ForRequestedType<IServiceLocator>().TheDefault.IsThis(serviceLocator);
                                    x.ForRequestedType<IInjector>().TheDefault.IsThis(serviceLocator);
                                    x.ForRequestedType<RouteCollection>().TheDefault.IsThis(RouteTable.Routes);
                                    x.ForRequestedType<ControllerBuilder>().TheDefault.IsThis(ControllerBuilder.Current);
                                    x.ForRequestedType<ModelBinderDictionary>().TheDefault.IsThis(ModelBinders.Binders);
                                    x.ForRequestedType<ViewEngineCollection>().TheDefault.IsThis(ViewEngines.Engines);
                                    x.ForRequestedType<IFilterRegistry>().CacheBy(InstanceScope.Singleton).TheDefaultIsConcreteType<FilterRegistry>();
                                    x.ForRequestedType<IControllerFactory>().CacheBy(InstanceScope.Singleton).TheDefaultIsConcreteType<ExtendedControllerFactory>();
                                    x.ForRequestedType<IActionInvoker>().TheDefaultIsConcreteType<ExtendedControllerActionInvoker>();
                                });
        }

        private static void RegisterDynamicTypes(IContainer container, IEnumerable<Type> concreteTypes)
        {
            concreteTypes.Where(type => KnownTypes.BootstrapperTaskType.IsAssignableFrom(type))
                         .Each(type => container.Configure(x => x.ForRequestedType(KnownTypes.BootstrapperTaskType).CacheBy(InstanceScope.Singleton).AddType(type)));

            concreteTypes.Where(type => KnownTypes.PerRequestTaskType.IsAssignableFrom(type))
                         .Each(type => container.Configure(x => x.ForRequestedType(KnownTypes.PerRequestTaskType).CacheBy(InstanceScope.Singleton).AddType(type)));

            concreteTypes.Where(type => KnownTypes.ModelBinderType.IsAssignableFrom(type) && type.IsDefined(KnownTypes.BindingAttributeType, true))
                         .Each(type => container.Configure(x => x.ForRequestedType(KnownTypes.ModelBinderType).CacheBy(InstanceScope.Singleton).AddType(type)));

            concreteTypes.Where(type => KnownTypes.ControllerType.IsAssignableFrom(type))
                         .Each(type => container.Configure(x => x.ForRequestedType(type).TheDefaultIsConcreteType(type)));

            concreteTypes.Where(type => KnownTypes.FilterAttributeType.IsAssignableFrom(type))
                         .Each(type => container.Configure(x => x.ForRequestedType(type).TheDefaultIsConcreteType(type)));

            concreteTypes.Where(type => KnownTypes.ViewEngineType.IsAssignableFrom(type))
                         .Each(type => container.Configure(x => x.ForRequestedType(KnownTypes.ViewEngineType).CacheBy(InstanceScope.Singleton).AddType(type)));
        }

        private static void RegisterRegistry(IContainer container, IEnumerable<Type> concreteTypes)
        {
            concreteTypes.Where(type => registryType.IsAssignableFrom(type) && type.HasDefaultConstructor())
                         .Select(type => Activator.CreateInstance(type))
                         .Cast<Registry>()
                         .Each(registry => container.Configure(x => x.AddRegistry(registry)));
        }
    }
}