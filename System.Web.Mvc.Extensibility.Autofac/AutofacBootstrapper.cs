#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Autofac
{
    using Linq;

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
            builder.RegisterModule(new CollectionModule());

            AutofacAdapter adapter = new AutofacAdapter(builder.Build());

            return adapter;
        }

        /// <summary>
        /// Loads the container specific modules.
        /// </summary>
        protected override void LoadModules()
        {
            ContainerBuilder builder = new ContainerBuilder();

            BuildManager.ConcreteTypes
                        .Where(type => moduleType.IsAssignableFrom(type) && type.HasDefaultConstructor())
                        .Where(type => !type.Namespace.StartsWith("Autofac", StringComparison.OrdinalIgnoreCase))
                        .Each(type => builder.RegisterModule(Activator.CreateInstance(type) as IModule));

            IContainer container = ((AutofacAdapter) ServiceLocator).Container;

            builder.Build(container);
        }
    }
}