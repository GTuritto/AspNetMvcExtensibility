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

    public class NinjectBootstrapper : BootstrapperBase
    {
        private static readonly Type moduleType = typeof(IModule);

        protected override IServiceLocator CreateServiceLocator()
        {
            IEnumerable<Type> concreteTypes = ReferencedAssemblies.ConcreteTypes();

            IKernel kernal = new Kernel(new DefaualtModule(concreteTypes));
            IServiceLocator serviceLocator = new NinjectServiceLocator(kernal);

            kernal.Bind<IServiceLocator>().ToConstant(serviceLocator).InSingletonScope();

            concreteTypes.Where(type => moduleType.IsAssignableFrom(type) && type.HasDefaultConstructor())
                         .Each(type => kernal.LoadModule(Activator.CreateInstance(type) as IModule));

            return serviceLocator;
        }

        private sealed class DefaualtModule : Module
        {
            private readonly IEnumerable<Type> concreteTypes;

            public DefaualtModule(IEnumerable<Type> concreteTypes)
            {
                this.concreteTypes = concreteTypes;
            }

            public override void Load()
            {
                Bind<RouteCollection>().ToConstant(RouteTable.Routes);
                Bind<ControllerBuilder>().ToConstant(ControllerBuilder.Current);
                Bind<ModelBinderDictionary>().ToConstant(ModelBinders.Binders);
                Bind<ViewEngineCollection>().ToConstant(ViewEngines.Engines);

                Bind<IControllerFactory>().To<ExtendedControllerFactory>().InTransientScope();
                Bind<IActionInvoker>().To<ExtendedControllerActionInvoker>().InTransientScope();

                concreteTypes.Where(type => KnownTypes.BootstrapperTaskType.IsAssignableFrom(type))
                             .Each(type => Bind(KnownTypes.BootstrapperTaskType).To(type).InTransientScope());

                concreteTypes.Where(type => KnownTypes.ModelBinderType.IsAssignableFrom(type) && type.IsDefined(KnownTypes.BindingAttributeType, true))
                             .Each(type => Bind(KnownTypes.ModelBinderType).To(type).InSingletonScope());

                concreteTypes.Where(type => KnownTypes.ControllerType.IsAssignableFrom(type))
                             .Each(type => Bind(type).ToSelf().InTransientScope());

                concreteTypes.Where(type => KnownTypes.ViewEngineType.IsAssignableFrom(type))
                             .Each(type => Bind(KnownTypes.ViewEngineType).To(type).InSingletonScope());
            }
        }
    }
}