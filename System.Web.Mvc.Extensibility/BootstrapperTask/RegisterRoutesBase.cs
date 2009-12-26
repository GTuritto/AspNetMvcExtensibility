#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Routing;

    /// <summary>
    /// Defines a base class to configure <seealso cref="RouteTable"/>.
    /// </summary>
    public abstract class RegisterRoutesBase : BootstrapperTaskBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterRoutesBase"/> class.
        /// </summary>
        /// <param name="routes">The routes.</param>
        protected RegisterRoutesBase(RouteCollection routes)
        {
            Invariant.IsNotNull(routes, "routes");

            Routes = routes;
        }

        /// <summary>
        /// Gets or sets the routes.
        /// </summary>
        /// <value>The routes.</value>
        protected RouteCollection Routes
        {
            get;
            private set;
        }
    }
}