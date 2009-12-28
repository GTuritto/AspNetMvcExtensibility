#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Ninject
{
    using Collections.Generic;
    using Linq;
    using Routing;

    using Microsoft.Practices.ServiceLocation;

    using IKernel = global::Ninject.IKernel;
    using IModule = global::Ninject.Modules.IModule;
    using Kernel = global::Ninject.StandardKernel;
    using Module = global::Ninject.Modules.Module;

    /// <summary>
    /// Defines a <seealso cref="BootstrapperBase">Bootstrapper</seealso> which is backed by Ninject <seealso cref="IKernel">Kernel</seealso>.
    /// </summary>
    public class NinjectBootstrapper : BootstrapperBase
    {
        private static readonly Type moduleType = typeof(IModule);

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectBootstrapper"/> class.
        /// </summary>
        /// <param name="buildManager">The build manager.</param>
        public NinjectBootstrapper(IBuildManager buildManager) : base(buildManager)
        {
        }

        /// <summary>
        /// Creates the service locator.
        /// </summary>
        /// <returns></returns>
        protected override IServiceLocator CreateServiceLocator()
        {
            IKernel kernal = new Kernel(new DefaualtModule(BuildManager));
            NinjectServiceLocator serviceLocator = new NinjectServiceLocator(kernal);

            kernal.Bind<IServiceLocator>().ToConstant(serviceLocator);
            kernal.Bind<IInjector>().ToConstant(serviceLocator);

            BuildManager.ConcreteTypes
                        .Where(type => moduleType.IsAssignableFrom(type) && type.HasDefaultConstructor())
                        .Each(type => kernal.LoadModule(Activator.CreateInstance(type) as IModule));

            return serviceLocator;
        }

        private sealed class DefaualtModule : Module
        {
            private readonly IBuildManager buildManager;

            public DefaualtModule(IBuildManager buildManager)
            {
                this.buildManager = buildManager;
            }

            public override void Load()
            {
                LoadKnownTypes();
                LoadDynamicTypes();
            }

            private void LoadKnownTypes()
            {
                Bind<IBuildManager>().ToConstant(buildManager);
                Bind<RouteCollection>().ToConstant(RouteTable.Routes);
                Bind<ControllerBuilder>().ToConstant(ControllerBuilder.Current);
                Bind<ModelBinderDictionary>().ToConstant(ModelBinders.Binders);
                Bind<ViewEngineCollection>().ToConstant(ViewEngines.Engines);

                Bind<IFilterRegistry>().To<FilterRegistry>().InSingletonScope();
                Bind<IControllerFactory>().To<ExtendedControllerFactory>().InSingletonScope();
                Bind<IActionInvoker>().To<ExtendedControllerActionInvoker>().InTransientScope();

                #if (!MVC1)

                Bind<IModelMetadataRegistry>().To<ModelMetadataRegistry>().InSingletonScope();
                Bind<IAreaManager>().To<AreaManager>().InSingletonScope();

                #endif
            }

            private void LoadDynamicTypes()
            {
                IEnumerable<Type> concreteTypes = buildManager.ConcreteTypes;

                concreteTypes.Where(type => KnownTypes.BootstrapperTaskType.IsAssignableFrom(type))
                             .Each(type => Bind(KnownTypes.BootstrapperTaskType).To(type).InSingletonScope());

                concreteTypes.Where(type => KnownTypes.PerRequestTaskType.IsAssignableFrom(type))
                             .Each(type => Bind(KnownTypes.PerRequestTaskType).To(type).InSingletonScope());

                concreteTypes.Where(type => KnownTypes.ModelBinderType.IsAssignableFrom(type) && type.IsDefined(KnownTypes.BindingAttributeType, true))
                             .Each(type => Bind(KnownTypes.ModelBinderType).To(type).InSingletonScope());

                concreteTypes.Where(type => KnownTypes.ControllerType.IsAssignableFrom(type))
                             .Each(type => Bind(type).ToSelf().InTransientScope());

                concreteTypes.Where(type => KnownTypes.FilterAttributeType.IsAssignableFrom(type))
                             .Each(type => Bind(type).ToSelf().InTransientScope());

                concreteTypes.Where(type => KnownTypes.ViewEngineType.IsAssignableFrom(type))
                             .Each(type => Bind(KnownTypes.ViewEngineType).To(type).InSingletonScope());

                #if (!MVC1)

                concreteTypes.Where(type => KnownTypes.ModelMetadataConfigurationType.IsAssignableFrom(type))
                             .Each(type => Bind(KnownTypes.ModelMetadataConfigurationType).To(type).InTransientScope());

                concreteTypes.Where(type => KnownTypes.ExtendedModelMetadataProviderType.IsAssignableFrom(type))
                             .Each(type => Bind(KnownTypes.ExtendedModelMetadataProviderType).To(type).InSingletonScope());

                concreteTypes.Where(type => KnownTypes.AreaType.IsAssignableFrom(type))
                             .Each(type => Bind(KnownTypes.AreaType).To(type).InSingletonScope());

                #endif
            }
        }
    }
}