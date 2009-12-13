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

        protected IServiceLocator ServiceLocator
        {
            get;
            private set;
        }

        protected virtual IList<FilterRegistryItemBase> Items
        {
            [DebuggerStepThrough]
            get
            {
                return items;
            }
        }

        public IFilterRegistry Register<TController, TFilter>() where TController : Controller where TFilter : FilterAttribute
        {
            return Register<TController, FilterAttribute>(ServiceLocator.GetInstance<TFilter>());
        }

        public IFilterRegistry Register<TController, TFilter1, TFilter2>() where TController : Controller where TFilter1 : FilterAttribute where TFilter2 : FilterAttribute
        {
            return Register<TController, FilterAttribute>(ServiceLocator.GetInstance<TFilter1>(), ServiceLocator.GetInstance<TFilter2>());
        }

        public IFilterRegistry Register<TController, TFilter1, TFilter2, TFilter3>() where TController : Controller where TFilter1 : FilterAttribute where TFilter2 : FilterAttribute where TFilter3 : FilterAttribute
        {
            return Register<TController, FilterAttribute>(ServiceLocator.GetInstance<TFilter1>(), ServiceLocator.GetInstance<TFilter2>(), ServiceLocator.GetInstance<TFilter3>());
        }

        public IFilterRegistry Register<TController, TFilter1, TFilter2, TFilter3, TFilter4>() where TController : Controller where TFilter1 : FilterAttribute where TFilter2 : FilterAttribute where TFilter3 : FilterAttribute where TFilter4 : FilterAttribute
        {
            return Register<TController, FilterAttribute>(ServiceLocator.GetInstance<TFilter1>(), ServiceLocator.GetInstance<TFilter2>(), ServiceLocator.GetInstance<TFilter3>(), ServiceLocator.GetInstance<TFilter4>());
        }

        public virtual IFilterRegistry Register<TController, TFilter>(params TFilter[] filters) where TController : Controller where TFilter : FilterAttribute
        {
            Invariant.IsNotNull(filters, "filters");

            if (filters.Any())
            {
                Items.Add(new FilterRegistryControllerItem<TController>(filters));
            }

            return this;
        }

        public IFilterRegistry Register<TController, TFilter>(Expression<Action<TController>> action) where TController : Controller where TFilter : FilterAttribute
        {
            return Register<TController, FilterAttribute>(action, ServiceLocator.GetInstance<TFilter>());
        }

        public IFilterRegistry Register<TController, TFilter1, TFilter2>(Expression<Action<TController>> action) where TController : Controller where TFilter1 : FilterAttribute where TFilter2 : FilterAttribute
        {
            return Register<TController, FilterAttribute>(action, ServiceLocator.GetInstance<TFilter1>(), ServiceLocator.GetInstance<TFilter2>());
        }

        public IFilterRegistry Register<TController, TFilter1, TFilter2, TFilter3>(Expression<Action<TController>> action) where TController : Controller where TFilter1 : FilterAttribute where TFilter2 : FilterAttribute where TFilter3 : FilterAttribute
        {
            return Register<TController, FilterAttribute>(action, ServiceLocator.GetInstance<TFilter1>(), ServiceLocator.GetInstance<TFilter2>(), ServiceLocator.GetInstance<TFilter3>());
        }

        public IFilterRegistry Register<TController, TFilter1, TFilter2, TFilter3, TFilter4>(Expression<Action<TController>> action) where TController : Controller where TFilter1 : FilterAttribute where TFilter2 : FilterAttribute where TFilter3 : FilterAttribute where TFilter4 : FilterAttribute
        {
            return Register<TController, FilterAttribute>(action, ServiceLocator.GetInstance<TFilter1>(), ServiceLocator.GetInstance<TFilter2>(), ServiceLocator.GetInstance<TFilter3>(), ServiceLocator.GetInstance<TFilter4>());
        }

        public virtual IFilterRegistry Register<TController, TFilter>(Expression<Action<TController>> action, params TFilter[] filters) where TController : Controller where TFilter : FilterAttribute
        {
            Invariant.IsNotNull(action, "action");
            Invariant.IsNotNull(filters, "filters");

            if (filters.Any())
            {
                Items.Add(new FilterRegistryActionItem<TController>(action, filters));
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
                    item.Filters.OfType<IAuthorizationFilter>()
                        .Cast<FilterAttribute>()
                        .Each(authorizationFilters.Add);

                    item.Filters.OfType<IActionFilter>()
                        .Cast<FilterAttribute>()
                        .Each(actionFilters.Add);

                    item.Filters.OfType<IResultFilter>()
                        .Cast<FilterAttribute>()
                        .Each(resultFilters.Add);

                    item.Filters.OfType<IExceptionFilter>()
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

            resultFilters.OrderBy(filter => filter.Order)
                         .Cast<IExceptionFilter>()
                         .Each(filter => filterInfo.ExceptionFilters.Add(filter));

            return filterInfo;
        }
    }
}