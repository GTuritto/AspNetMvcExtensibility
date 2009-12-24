#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Autofac
{
    using Collections.Generic;
    using Linq;
    using Routing;

    using Microsoft.Practices.ServiceLocation;

    using CollectionModule = global::Autofac.Modules.ImplicitCollectionSupportModule;
    using ContainerBuilder = global::Autofac.Builder.ContainerBuilder;
    using IModule = global::Autofac.IModule;

    public class AutofacBootstrapper : BootstrapperBase
    {
        private static readonly Type moduleType = typeof(IModule);

        public AutofacBootstrapper(IBuildManager buildManager) : base(buildManager)
        {
        }

        protected override IServiceLocator CreateServiceLocator()
        {
            ContainerBuilder builder = new ContainerBuilder();
            AutofacServiceLocator serviceLocator = new AutofacServiceLocator();

            builder.Register(serviceLocator).As<IServiceLocator>();
            builder.Register(serviceLocator).As<IInjector>();
            RegisterKnownTypes(builder);

            IEnumerable<Type> concreteTypes = BuildManager.ConcreteTypes;

            RegisterDynamicTypes(builder, concreteTypes);
            RegisterModules(builder, concreteTypes);

            serviceLocator.Container = builder.Build();

            return serviceLocator;
        }

        private static void RegisterDynamicTypes(ContainerBuilder builder, IEnumerable<Type> concreteTypes)
        {
            concreteTypes.Where(type => KnownTypes.BootstrapperTaskType.IsAssignableFrom(type))
                         .Each(type => builder.Register(type).As(KnownTypes.BootstrapperTaskType).ContainerScoped());

            concreteTypes.Where(type => KnownTypes.PerRequestTaskType.IsAssignableFrom(type))
                         .Each(type => builder.Register(type).As(KnownTypes.PerRequestTaskType).ContainerScoped());

            concreteTypes.Where(type => KnownTypes.ModelBinderType.IsAssignableFrom(type) && type.IsDefined(KnownTypes.BindingAttributeType, true))
                         .Each(type => builder.Register(type).As(KnownTypes.ModelBinderType).ContainerScoped());

            concreteTypes.Where(type => KnownTypes.ControllerType.IsAssignableFrom(type))
                         .Each(type => builder.Register(type).FactoryScoped());

            concreteTypes.Where(type => KnownTypes.FilterAttributeType.IsAssignableFrom(type))
                         .Each(type => builder.Register(type).FactoryScoped());

            concreteTypes.Where(type => KnownTypes.ViewEngineType.IsAssignableFrom(type))
                         .Each(type => builder.Register(type).As(KnownTypes.ViewEngineType).ContainerScoped());
        }

        private static void RegisterModules(ContainerBuilder builder, IEnumerable<Type> concreteTypes)
        {
            builder.RegisterModule(new CollectionModule());

            concreteTypes.Where(type => moduleType.IsAssignableFrom(type) && type.HasDefaultConstructor())
                         .Where(type => !type.Namespace.StartsWith("Autofac", StringComparison.OrdinalIgnoreCase))
                         .Each(type => builder.RegisterModule(Activator.CreateInstance(type) as IModule));
        }

        private void RegisterKnownTypes(ContainerBuilder builder)
        {
            builder.Register(RouteTable.Routes);
            builder.Register(ControllerBuilder.Current);
            builder.Register(ModelBinders.Binders);
            builder.Register(ViewEngines.Engines);
            builder.Register(BuildManager);

            builder.Register<FilterRegistry>().As<IFilterRegistry>().ContainerScoped();
            builder.Register<ModelMetadataRegistry>().As<IModelMetadataRegistry>().ContainerScoped();
            builder.Register<ExtendedControllerFactory>().As<IControllerFactory>().ContainerScoped();
            builder.Register<ExtendedControllerActionInvoker>().As<IActionInvoker>().FactoryScoped();
        }
    }
}