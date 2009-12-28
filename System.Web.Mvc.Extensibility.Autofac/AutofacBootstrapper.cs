#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

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

    /// <summary>
    /// Defines a <seealso cref="BootstrapperBase">Bootstrapper</seealso> which is backed by Autofac <seealso cref="IContainer">Container</seealso>.
    /// </summary>
    public class AutofacBootstrapper : BootstrapperBase
    {
        private static readonly Type moduleType = typeof(IModule);

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacBootstrapper"/> class.
        /// </summary>
        /// <param name="buildManager">The build manager.</param>
        public AutofacBootstrapper(IBuildManager buildManager) : base(buildManager)
        {
        }

        /// <summary>
        /// Creates the service locator.
        /// </summary>
        /// <returns></returns>
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

            #if (!MVC1)

            concreteTypes.Where(type => KnownTypes.ModelMetadataConfigurationType.IsAssignableFrom(type))
                         .Each(type => builder.Register(type).As(KnownTypes.ModelMetadataConfigurationType).FactoryScoped());

            concreteTypes.Where(type => KnownTypes.ExtendedModelMetadataProviderType.IsAssignableFrom(type))
                         .Each(type => builder.Register(type).As(KnownTypes.ExtendedModelMetadataProviderType).ContainerScoped());

            concreteTypes.Where(type => KnownTypes.ModelValidatorProviderType.IsAssignableFrom(type))
                         .Each(type => builder.Register(type).As(KnownTypes.ModelValidatorProviderType).ContainerScoped());

            concreteTypes.Where(type => KnownTypes.AreaType.IsAssignableFrom(type))
                         .Each(type => builder.Register(type).As(KnownTypes.AreaType).ContainerScoped());

            #endif
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
            builder.Register<ExtendedControllerFactory>().As<IControllerFactory>().ContainerScoped();
            builder.Register<ExtendedControllerActionInvoker>().As<IActionInvoker>().FactoryScoped();

            #if (!MVC1)

            builder.Register<CompositeModelMetadataProvider>().ContainerScoped();
            builder.Register<ModelMetadataRegistry>().As<IModelMetadataRegistry>().ContainerScoped();
            builder.Register<AreaManager>().As<IAreaManager>().ContainerScoped();

            #endif
        }
    }
}