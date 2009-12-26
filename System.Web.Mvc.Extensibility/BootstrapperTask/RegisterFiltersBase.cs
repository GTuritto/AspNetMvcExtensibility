#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    /// <summary>
    /// Defines a class to configure <seealso cref="FilterAttribute"/>s for <see cref="Controller"/> or action methods.
    /// </summary>
    public abstract class RegisterFiltersBase : BootstrapperTaskBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterFiltersBase"/> class.
        /// </summary>
        /// <param name="filterRegistry">The filter registry.</param>
        protected RegisterFiltersBase(IFilterRegistry filterRegistry)
        {
            Invariant.IsNotNull(filterRegistry, "filterRegistry");

            FilterRegistry = filterRegistry;
        }

        /// <summary>
        /// Gets or sets the filter registry.
        /// </summary>
        /// <value>The filter registry.</value>
        protected IFilterRegistry FilterRegistry
        {
            get;
            private set;
        }
    }
}