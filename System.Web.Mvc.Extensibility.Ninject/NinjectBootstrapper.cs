#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Ninject
{
    using Linq;

    using Microsoft.Practices.ServiceLocation;

    using IKernel = global::Ninject.IKernel;
    using IModule = global::Ninject.Modules.IModule;
    using Kernel = global::Ninject.StandardKernel;

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
            return new NinjectAdapter(new Kernel());
        }

        /// <summary>
        /// Loads the container specific modules.
        /// </summary>
        protected override void LoadModules()
        {
            IKernel kernel = ((NinjectAdapter) ServiceLocator).Kernel;

            BuildManager.ConcreteTypes
                        .Where(type => moduleType.IsAssignableFrom(type) && type.HasDefaultConstructor())
                        .Each(type => kernel.LoadModule(Activator.CreateInstance(type) as IModule));
        }
    }
}