#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using System;
    using Mvc;
    #if (!MVC1)
    using Routing;
    #endif

    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// The Default IoC backed <seealso cref="IControllerFactory"/>.
    /// </summary>
    public class ExtendedControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedControllerFactory"/> class.
        /// </summary>
        /// <param name="locator">The locator.</param>
        public ExtendedControllerFactory(IServiceLocator locator)
        {
            Invariant.IsNotNull(locator, "locator");

            ServiceLocator = locator;
        }

        /// <summary>
        /// Gets or sets the service locator.
        /// </summary>
        /// <value>The service locator.</value>
        protected IServiceLocator ServiceLocator
        {
            get;
            private set;
        }

        #if (MVC1)

        /// <summary>
        /// Gets the controller instance.
        /// </summary>
        /// <param name="controllerType">Type of the controller.</param>
        /// <returns>A reference to the controller.</returns>
        protected override IController GetControllerInstance(Type controllerType)
        {
            return CreateController(controllerType) ?? base.GetControllerInstance(controllerType);
        }

        #else

        /// <summary>
        /// Retrieves the controller instance for the specified request context and controller type.
        /// </summary>
        /// <param name="requestContext">The context of the HTTP request, which includes the HTTP context and route data.</param>
        /// <param name="controllerType">The type of the controller.</param>
        /// <returns>The controller instance.</returns>
        /// <exception cref="T:System.Web.HttpException">
        /// <paramref name="controllerType"/> is null.</exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="controllerType"/> cannot be assigned.</exception>
        /// <exception cref="T:System.InvalidOperationException">An instance of <paramref name="controllerType"/> cannot be created.</exception>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return CreateController(controllerType) ?? base.GetControllerInstance(requestContext, controllerType);
        }

        #endif

        private Controller CreateController(Type controllerType)
        {
            Controller controller = null;

            if (controllerType != null)
            {
                controller = ServiceLocator.GetInstance(controllerType) as Controller;

                if (controller != null)
                {
                    controller.ActionInvoker = ServiceLocator.GetInstance<IActionInvoker>();
                }
            }

            return controller;
        }
    }
}