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

    using Microsoft.Practices.ServiceLocation;

    using IContainer = global::StructureMap.IContainer;

    [CLSCompliant(false)]
    public class StructureMapServiceLocator : ServiceLocatorImplBase, IInjector
    {
        public StructureMapServiceLocator(IContainer container)
        {
            Invariant.IsNotNull(container, "container");

            Container = container;
        }

        public IContainer Container
        {
            get;
            private set;
        }

        public virtual void Inject(object instance)
        {
            if (instance != null)
            {
                Container.BuildUp(instance);
            }
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return string.IsNullOrEmpty(key) ? Container.GetInstance(serviceType) : Container.GetInstance(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return Container.GetAllInstances(serviceType).Cast<object>();
        }
    }
}