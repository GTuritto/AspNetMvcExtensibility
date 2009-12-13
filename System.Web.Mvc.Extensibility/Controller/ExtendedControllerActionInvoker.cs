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
            FilterInfo decoratedFilters = base.GetFilters(controllerContext, actionDescriptor);

            IInjection injection = ServiceLocator as IInjection;

            if (injection != null)
            {
                ICollection<object> injectedFilters = new List<object>();

                Inject(injection, injectedFilters, decoratedFilters.AuthorizationFilters);
                Inject(injection, injectedFilters, decoratedFilters.ActionFilters);
                Inject(injection, injectedFilters, decoratedFilters.ResultFilters);
                Inject(injection, injectedFilters, decoratedFilters.ExceptionFilters);
            }

            FilterInfo registeredFilters = FilterRegistry.Matching(controllerContext, actionDescriptor);

            FilterInfo mergedFilters = new FilterInfo();

            decoratedFilters.AuthorizationFilters
                            .Where(filter => !KnownTypes.ControllerType.IsAssignableFrom(filter.GetType()))
                            .Concat(registeredFilters.AuthorizationFilters)
                            .Cast<FilterAttribute>()
                            .OrderBy(filter => filter.Order)
                            .Cast<IAuthorizationFilter>()
                            .Each(filter => mergedFilters.AuthorizationFilters.Add(filter));

            decoratedFilters.ActionFilters
                            .Where(filter => !KnownTypes.ControllerType.IsAssignableFrom(filter.GetType()))
                            .Concat(registeredFilters.ActionFilters)
                            .Cast<FilterAttribute>()
                            .OrderBy(filter => filter.Order)
                            .Cast<IActionFilter>()
                            .Each(filter => mergedFilters.ActionFilters.Add(filter));

            decoratedFilters.ResultFilters
                            .Where(filter => !KnownTypes.ControllerType.IsAssignableFrom(filter.GetType()))
                            .Concat(registeredFilters.ResultFilters)
                            .Cast<FilterAttribute>()
                            .OrderBy(filter => filter.Order)
                            .Cast<IResultFilter>()
                            .Each(filter => mergedFilters.ResultFilters.Add(filter));

            decoratedFilters.ExceptionFilters
                            .Where(filter => !KnownTypes.ControllerType.IsAssignableFrom(filter.GetType()))
                            .Concat(registeredFilters.ExceptionFilters)
                            .Cast<FilterAttribute>()
                            .OrderBy(filter => filter.Order)
                            .Cast<IExceptionFilter>()
                            .Each(filter => mergedFilters.ExceptionFilters.Add(filter));

            decoratedFilters.AuthorizationFilters
                            .OfType<Controller>()
                            .Reverse()
                            .Each(filter => mergedFilters.AuthorizationFilters.Insert(0, filter));

            decoratedFilters.ActionFilters
                            .OfType<Controller>()
                            .Reverse()
                            .Each(filter => mergedFilters.ActionFilters.Insert(0, filter));

            decoratedFilters.ResultFilters
                            .OfType<Controller>()
                            .Reverse()
                            .Each(filter => mergedFilters.ResultFilters.Insert(0, filter));

            decoratedFilters.ExceptionFilters
                            .OfType<Controller>()
                            .Reverse()
                            .Each(filter => mergedFilters.ExceptionFilters.Insert(0, filter));

            return mergedFilters;
        }

        private static void Inject<TFilter>(IInjection injection, ICollection<object> injectedFilters, IEnumerable<TFilter> filters) where TFilter : class
        {
            foreach (TFilter filter in filters)
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
}