namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using Linq;

    using Microsoft.Practices.ServiceLocation;

    public class ExtendedControllerActionInvoker : ControllerActionInvoker
    {
        public ExtendedControllerActionInvoker(IServiceLocator locator, IFilterRegistry filterRegistry)
        {
            Invariant.IsNotNull(locator, "locator");
            Invariant.IsNotNull(filterRegistry, "filterRegistry");

            ServiceLocator = locator;
            FilterRegistry = filterRegistry;
        }

        protected IServiceLocator ServiceLocator
        {
            get;
            private set;
        }

        protected IFilterRegistry FilterRegistry
        {
            get;
            private set;
        }

        protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            Invariant.IsNotNull(controllerContext, "controllerContext");
            Invariant.IsNotNull(actionDescriptor, "actionDescriptor");

            FilterInfo decoratedFilters = base.GetFilters(controllerContext, actionDescriptor);

            Inject(decoratedFilters);

            FilterInfo registeredFilters = FilterRegistry.Matching(controllerContext, actionDescriptor);

            FilterInfo mergedFilters = new FilterInfo();

            MergeOrdered(mergedFilters, decoratedFilters, registeredFilters);
            MergeUnordered(mergedFilters, decoratedFilters, registeredFilters);

            return mergedFilters;
        }

        private static void MergeOrdered(FilterInfo mergedFilters, FilterInfo decoratedFilters, FilterInfo registeredFilters)
        {
            Merge(decoratedFilters.AuthorizationFilters, registeredFilters.AuthorizationFilters, IsFilterAttriute)
                .Cast<FilterAttribute>()
                .OrderBy(filter => filter.Order)
                .Cast<IAuthorizationFilter>()
                .Each(filter => mergedFilters.AuthorizationFilters.Add(filter));

            Merge(decoratedFilters.ActionFilters, registeredFilters.ActionFilters, IsFilterAttriute)
                .Cast<FilterAttribute>()
                .OrderBy(filter => filter.Order)
                .Cast<IActionFilter>()
                .Each(filter => mergedFilters.ActionFilters.Add(filter));

            Merge(decoratedFilters.ResultFilters, registeredFilters.ResultFilters, IsFilterAttriute)
                .Cast<FilterAttribute>()
                .OrderBy(filter => filter.Order)
                .Cast<IResultFilter>()
                .Each(filter => mergedFilters.ResultFilters.Add(filter));

            Merge(decoratedFilters.ExceptionFilters, registeredFilters.ExceptionFilters, IsFilterAttriute)
                .Cast<FilterAttribute>()
                .OrderBy(filter => filter.Order)
                .Cast<IExceptionFilter>()
                .Each(filter => mergedFilters.ExceptionFilters.Add(filter));
        }

        private static void MergeUnordered(FilterInfo mergedFilters, FilterInfo decoratedFilters, FilterInfo registeredFilters)
        {
            Merge(decoratedFilters.AuthorizationFilters, registeredFilters.AuthorizationFilters, IsNonFilterAttriute)
                .Reverse()
                .Each(filter => mergedFilters.AuthorizationFilters.Insert(0, filter));

            Merge(decoratedFilters.ActionFilters, registeredFilters.ActionFilters, IsNonFilterAttriute)
                .Reverse()
                .Each(filter => mergedFilters.ActionFilters.Insert(0, filter));

            Merge(decoratedFilters.ResultFilters, registeredFilters.ResultFilters, IsNonFilterAttriute)
                .Reverse()
                .Each(filter => mergedFilters.ResultFilters.Insert(0, filter));

            Merge(decoratedFilters.ExceptionFilters, registeredFilters.ExceptionFilters, IsNonFilterAttriute)
                .Reverse()
                .Each(filter => mergedFilters.ExceptionFilters.Insert(0, filter));
        }

        private static IEnumerable<T> Merge<T>(IEnumerable<T> source1, IEnumerable<T> source2, Func<T, bool> filter)
        {
            return source1.Where(filter).Concat(source2.Where(filter));
        }

        private static bool IsFilterAttriute<TFilter>(TFilter filter)
        {
            return KnownTypes.FilterAttributeType.IsAssignableFrom(filter.GetType());
        }

        private static bool IsNonFilterAttriute<TFilter>(TFilter filter)
        {
            return !KnownTypes.FilterAttributeType.IsAssignableFrom(filter.GetType());
        }

        private static void Inject<TFilter>(IInjection injection, ICollection<object> injectedFilters, IEnumerable<TFilter> filters) where TFilter : class
        {
            foreach (TFilter filter in filters)
            {
                // Only inject dependency for Filter Attribute
                if (IsFilterAttriute(filter))
                {
                    // Since the same filter can appear in different stages of processing
                    // we do not want a filter to get injected more than once.
                    if (!injectedFilters.Contains(filter))
                    {
                        injection.Inject(filter);
                        injectedFilters.Add(filter);
                    }
                }
            }
        }

        private void Inject(FilterInfo decoratedFilters)
        {
            IInjection injection = ServiceLocator as IInjection;

            if (injection != null)
            {
                ICollection<object> injectedFilters = new List<object>();

                Inject(injection, injectedFilters, decoratedFilters.AuthorizationFilters);
                Inject(injection, injectedFilters, decoratedFilters.ActionFilters);
                Inject(injection, injectedFilters, decoratedFilters.ResultFilters);
                Inject(injection, injectedFilters, decoratedFilters.ExceptionFilters);
            }
        }
    }
}