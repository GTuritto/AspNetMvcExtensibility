#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;

    /// <summary>
    /// Defines a base class to store the <see cref="FilterAttribute"/> factories.
    /// </summary>
    public abstract class FilterRegistryItemBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterRegistryItemBase"/> class.
        /// </summary>
        /// <param name="filters">The filters.</param>
        protected FilterRegistryItemBase(IEnumerable<Func<FilterAttribute>> filters)
        {
            Invariant.IsNotNull(filters, "filters");

            Filters = filters;
        }

        /// <summary>
        /// Gets or sets the filter factoriess.
        /// </summary>
        /// <value>The filters.</value>
        public IEnumerable<Func<FilterAttribute>> Filters
        {
            get;
            protected set;
        }

        /// <summary>
        /// Determines whether the specified controller context is matching.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns>
        /// <c>true</c> if the specified controller context is matching; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsMatching(ControllerContext controllerContext, ActionDescriptor actionDescriptor);
    }
}