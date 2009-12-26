#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using ComponentModel;
    using Linq.Expressions;

    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// Represents an interface which is maintain fluently registered <seealso cref="FilterAttribute"/>s.
    /// </summary>
    public interface IFilterRegistry : IFluentSyntax
    {
        /// <summary>
        /// Gets the service locator.
        /// </summary>
        /// <value>The service locator.</value>
        IServiceLocator ServiceLocator
        {
            [EditorBrowsable(EditorBrowsableState.Never)]
            get;
        }

        /// <summary>
        /// Gets the registered items.
        /// </summary>
        /// <value>The items.</value>
        IList<FilterRegistryItemBase> Items
        {
            [EditorBrowsable(EditorBrowsableState.Never)]
            get;
        }

        /// <summary>
        /// Registers the specified filters for the given controller.
        /// </summary>
        /// <typeparam name="TController">The type of the controller.</typeparam>
        /// <typeparam name="TFilter">The type of the filter.</typeparam>
        /// <param name="filters">The filters.</param>
        /// <returns></returns>
        IFilterRegistry Register<TController, TFilter>(IEnumerable<Func<TFilter>> filters)
            where TController : Controller
            where TFilter : FilterAttribute;

        /// <summary>
        /// Registers the specified filters for the given controller action.
        /// </summary>
        /// <typeparam name="TController">The type of the controller.</typeparam>
        /// <typeparam name="TFilter">The type of the filter.</typeparam>
        /// <param name="action">The controller action method.</param>
        /// <param name="filters">The filters.</param>
        /// <returns></returns>
        IFilterRegistry Register<TController, TFilter>(Expression<Action<TController>> action, IEnumerable<Func<TFilter>> filters)
            where TController : Controller
            where TFilter : FilterAttribute;

        /// <summary>
        /// Returns the matching filters for the given controller action.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        FilterInfo Matching(ControllerContext controllerContext, ActionDescriptor actionDescriptor);
    }
}