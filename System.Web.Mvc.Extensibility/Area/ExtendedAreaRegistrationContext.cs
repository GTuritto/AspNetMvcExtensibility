#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Routing;

    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// Defines a class which provides additional information when register/unregister area.
    /// </summary>
    public class ExtendedAreaRegistrationContext : AreaRegistrationContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedAreaRegistrationContext"/> class.
        /// </summary>
        /// <param name="areaName">The name of the area to register.</param>
        /// <param name="serviceLocator">The service locator.</param>
        public ExtendedAreaRegistrationContext(string areaName, IServiceLocator serviceLocator) : base(areaName, serviceLocator.GetInstance<RouteCollection>(), null)
        {
            ServiceLocator = serviceLocator;
        }

        /// <summary>
        /// Gets the service locator.
        /// </summary>
        /// <value>The service locator.</value>
        public IServiceLocator ServiceLocator
        {
            get;
            private set;
        }
    }
}