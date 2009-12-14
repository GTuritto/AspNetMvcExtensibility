#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Linq.Expressions;

    public interface IFilterRegistry
    {
        IFilterRegistry Register<TController, TFilter>()
            where TController : Controller
            where TFilter : FilterAttribute;

        IFilterRegistry Register<TController, TFilter>(Action<TFilter> configureFilter)
            where TController : Controller
            where TFilter : FilterAttribute;

        IFilterRegistry Register<TController, TFilter1, TFilter2>()
            where TController : Controller
            where TFilter1 : FilterAttribute
            where TFilter2 : FilterAttribute;

        IFilterRegistry Register<TController, TFilter1, TFilter2, TFilter3>()
            where TController : Controller
            where TFilter1 : FilterAttribute
            where TFilter2 : FilterAttribute
            where TFilter3 : FilterAttribute;

        IFilterRegistry Register<TController, TFilter1, TFilter2, TFilter3, TFilter4>()
            where TController : Controller
            where TFilter1 : FilterAttribute
            where TFilter2 : FilterAttribute
            where TFilter3 : FilterAttribute
            where TFilter4 : FilterAttribute;

        IFilterRegistry Register<TController, TFilter>(params TFilter[] filters)
            where TController : Controller
            where TFilter : FilterAttribute;

        IFilterRegistry Register<TController, TFilter>(Expression<Action<TController>> action)
            where TController : Controller
            where TFilter : FilterAttribute;

        IFilterRegistry Register<TController, TFilter>(Expression<Action<TController>> action, Action<TFilter> configureFilter)
            where TController : Controller
            where TFilter : FilterAttribute;

        IFilterRegistry Register<TController, TFilter1, TFilter2>(Expression<Action<TController>> action)
            where TController : Controller
            where TFilter1 : FilterAttribute
            where TFilter2 : FilterAttribute;

        IFilterRegistry Register<TController, TFilter1, TFilter2, TFilter3>(Expression<Action<TController>> action)
            where TController : Controller
            where TFilter1 : FilterAttribute
            where TFilter2 : FilterAttribute
            where TFilter3 : FilterAttribute;

        IFilterRegistry Register<TController, TFilter1, TFilter2, TFilter3, TFilter4>(Expression<Action<TController>> action)
            where TController : Controller
            where TFilter1 : FilterAttribute
            where TFilter2 : FilterAttribute
            where TFilter3 : FilterAttribute
            where TFilter4 : FilterAttribute;

        IFilterRegistry Register<TController, TFilter>(Expression<Action<TController>> action, params TFilter[] filters)
            where TController : Controller
            where TFilter : FilterAttribute;

        FilterInfo Matching(ControllerContext controllerContext, ActionDescriptor actionDescriptor);
    }
}