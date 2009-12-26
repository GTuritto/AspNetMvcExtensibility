#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// Defines a class which is used to register the default <seealso cref="IControllerFactory"/>.
    /// </summary>
    public class RegisterControllerFactory : BootstrapperTaskBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterControllerFactory"/> class.
        /// </summary>
        /// <param name="controllerBuilder">The controller builder.</param>
        public RegisterControllerFactory(ControllerBuilder controllerBuilder)
        {
            Invariant.IsNotNull(controllerBuilder, "controllerBuilder");

            ControllerBuilder = controllerBuilder;
        }

        /// <summary>
        /// Gets or sets the controller builder.
        /// </summary>
        /// <value>The controller builder.</value>
        protected ControllerBuilder ControllerBuilder
        {
            get;
            private set;
        }

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <param name="serviceLocator">The service locator.</param>
        protected override void ExecuteCore(IServiceLocator serviceLocator)
        {
            ControllerBuilder.SetControllerFactory(serviceLocator.GetInstance<IControllerFactory>());
        }
    }
}