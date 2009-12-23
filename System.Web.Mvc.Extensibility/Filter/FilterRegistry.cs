#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using Diagnostics;
    using Linq;
    using Linq.Expressions;

    using Microsoft.Practices.ServiceLocation;

    public class FilterRegistry : IFilterRegistry
    {
        private readonly IList<FilterRegistryItemBase> items;

        public FilterRegistry(IServiceLocator serviceLocator)
        {
            Invariant.IsNotNull(serviceLocator, "serviceLocator");

            ServiceLocator = serviceLocator;
            items = new List<FilterRegistryItemBase>();
        }

        public IServiceLocator ServiceLocator
        {
            get;
            private set;
        }

        public virtual IList<FilterRegistryItemBase> Items
        {
            [DebuggerStepThrough]
            get
            {
                return items;
            }
        }

        public virtual IFilterRegistry Register<TController, TFilter>(IEnumerable<Func<TFilter>> filters)
            where TController : Controller where TFilter : FilterAttribute
        {
            Invariant.IsNotNull(filters, "filters");

            if (filters.Any())
            {
                Items.Add(new FilterRegistryControllerItem<TController>(ConvertFilters(filters)));
            }

            return this;
        }

        public virtual IFilterRegistry Register<TController, TFilter>(Expression<Action<TController>> action, IEnumerable<Func<TFilter>> filters)
            where TController : Controller
            where TFilter : FilterAttribute
        {
            Invariant.IsNotNull(action, "action");
            Invariant.IsNotNull(filters, "filters");

            if (filters.Any())
            {
                Items.Add(new FilterRegistryActionItem<TController>(action, ConvertFilters(filters)));
            }

            return this;
        }

        public virtual FilterInfo Matching(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            Invariant.IsNotNull(controllerContext, "controllerContext");
            Invariant.IsNotNull(actionDescriptor, "actionDescriptor");

            IList<FilterAttribute> authorizationFilters = new List<FilterAttribute>();
            IList<FilterAttribute> actionFilters = new List<FilterAttribute>();
            IList<FilterAttribute> resultFilters = new List<FilterAttribute>();
            IList<FilterAttribute> exceptionFiltes = new List<FilterAttribute>();

            foreach (FilterRegistryItemBase item in Items)
            {
                if (item.IsMatching(controllerContext, actionDescriptor))
                {
                    IEnumerable<FilterAttribute> filters = item.Filters.Select(filter => filter());

                    filters.OfType<IAuthorizationFilter>()
                           .Cast<FilterAttribute>()
                           .Each(authorizationFilters.Add);

                    filters.OfType<IActionFilter>()
                           .Cast<FilterAttribute>()
                           .Each(actionFilters.Add);

                    filters.OfType<IResultFilter>()
                           .Cast<FilterAttribute>()
                           .Each(resultFilters.Add);

                    filters.OfType<IExceptionFilter>()
                           .Cast<FilterAttribute>()
                           .Each(exceptionFiltes.Add);
                }
            }

            FilterInfo filterInfo = new FilterInfo();

            authorizationFilters.OrderBy(filter => filter.Order)
                                .Cast<IAuthorizationFilter>()
                                .Each(filter => filterInfo.AuthorizationFilters.Add(filter));

            actionFilters.OrderBy(filter => filter.Order)
                         .Cast<IActionFilter>()
                         .Each(filter => filterInfo.ActionFilters.Add(filter));

            resultFilters.OrderBy(filter => filter.Order)
                         .Cast<IResultFilter>()
                         .Each(filter => filterInfo.ResultFilters.Add(filter));

            exceptionFiltes.OrderBy(filter => filter.Order)
                           .Cast<IExceptionFilter>()
                           .Each(filter => filterInfo.ExceptionFilters.Add(filter));

            return filterInfo;
        }

        private static IEnumerable<Func<FilterAttribute>> ConvertFilters<TFilter>(IEnumerable<Func<TFilter>> filters)
            where TFilter : FilterAttribute
        {
            return filters.Select(filter => new Func<FilterAttribute>(() => filter()));
        }
    }
}