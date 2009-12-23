#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using ComponentModel;
    using Linq.Expressions;

    using Microsoft.Practices.ServiceLocation;

    public interface IFilterRegistry : IFluentSyntax
    {
        IServiceLocator ServiceLocator
        {
            [EditorBrowsable(EditorBrowsableState.Never)]
            get;
        }

        IList<FilterRegistryItemBase> Items
        {
            [EditorBrowsable(EditorBrowsableState.Never)]
            get;
        }

        IFilterRegistry Register<TController, TFilter>(IEnumerable<Func<TFilter>> filters)
            where TController : Controller
            where TFilter : FilterAttribute;

        IFilterRegistry Register<TController, TFilter>(Expression<Action<TController>> action, IEnumerable<Func<TFilter>> filters)
            where TController : Controller
            where TFilter : FilterAttribute;

        [EditorBrowsable(EditorBrowsableState.Never)]
        FilterInfo Matching(ControllerContext controllerContext, ActionDescriptor actionDescriptor);
    }
}