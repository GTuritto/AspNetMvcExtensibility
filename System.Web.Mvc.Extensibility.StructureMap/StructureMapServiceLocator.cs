#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.StructureMap
{
    using Collections.Generic;
    using Linq;

    using Microsoft.Practices.ServiceLocation;

    using IContainer = global::StructureMap.IContainer;

    /// <summary>
    /// Defines a <seealso cref="IServiceLocator">service locator</seealso> which with backed by StructureMap <seealso cref="IContainer">Container</seealso>.
    /// </summary>
    [CLSCompliant(false)]
    public class StructureMapServiceLocator : ServiceLocatorImplBase, IInjector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StructureMapServiceLocator"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public StructureMapServiceLocator(IContainer container)
        {
            Invariant.IsNotNull(container, "container");

            Container = container;
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>The container.</value>
        public IContainer Container
        {
            get;
            private set;
        }

        /// <summary>
        /// Injects the matching dependences.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public virtual void Inject(object instance)
        {
            if (instance != null)
            {
                Container.BuildUp(instance);
            }
        }

        /// <summary>
        /// Gets the matching instance for the given type and key.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected override object DoGetInstance(Type serviceType, string key)
        {
            return string.IsNullOrEmpty(key) ? Container.GetInstance(serviceType) : Container.GetInstance(serviceType, key);
        }

        /// <summary>
        /// Gets all the instances for the given type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return Container.GetAllInstances(serviceType).Cast<object>();
        }
    }
}