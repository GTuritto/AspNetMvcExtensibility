#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using Linq;

    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// The default <seealso cref="IActionInvoker"/> which supports the fluent filter registration.
    /// </summary>
    public class ExtendedControllerActionInvoker : ControllerActionInvoker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedControllerActionInvoker"/> class.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <param name="filterRegistry">The filter registry.</param>
        public ExtendedControllerActionInvoker(IServiceLocator locator, IFilterRegistry filterRegistry)
        {
            Invariant.IsNotNull(locator, "locator");
            Invariant.IsNotNull(filterRegistry, "filterRegistry");

            ServiceLocator = locator;
            FilterRegistry = filterRegistry;
        }

        /// <summary>
        /// Gets or sets the service locator.
        /// </summary>
        /// <value>The service locator.</value>
        protected IServiceLocator ServiceLocator
        {
            get;
            private set;
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

        /// <summary>
        /// Retrieves information about the action filters.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns>Information about the action filters.</returns>
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

        private static void Inject<TFilter>(IInjector injector, ICollection<object> injectedFilters, IEnumerable<TFilter> filters) where TFilter : class
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
                        injector.Inject(filter);
                        injectedFilters.Add(filter);
                    }
                }
            }
        }

        private void Inject(FilterInfo decoratedFilters)
        {
            IInjector injector = ServiceLocator as IInjector;

            if (injector != null)
            {
                ICollection<object> injectedFilters = new List<object>();

                Inject(injector, injectedFilters, decoratedFilters.AuthorizationFilters);
                Inject(injector, injectedFilters, decoratedFilters.ActionFilters);
                Inject(injector, injectedFilters, decoratedFilters.ResultFilters);
                Inject(injector, injectedFilters, decoratedFilters.ExceptionFilters);
            }
        }
    }
}