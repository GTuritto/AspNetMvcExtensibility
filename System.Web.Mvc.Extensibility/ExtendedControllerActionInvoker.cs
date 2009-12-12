namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;

    using Microsoft.Practices.ServiceLocation;

    public class ExtendedControllerActionInvoker : ControllerActionInvoker
    {
        private readonly IList<object> injectedFilters = new List<object>();

        public ExtendedControllerActionInvoker(IServiceLocator locator)
        {
            Invariant.IsNotNull(locator, "locator");

            ServiceLocator = locator;
        }

        protected IServiceLocator ServiceLocator
        {
            get;
            private set;
        }

        protected override AuthorizationContext InvokeAuthorizationFilters(ControllerContext controllerContext, IList<IAuthorizationFilter> filters, ActionDescriptor actionDescriptor)
        {
            Inject(filters);

            return base.InvokeAuthorizationFilters(controllerContext, filters, actionDescriptor);
        }

        protected override ActionExecutedContext InvokeActionMethodWithFilters(ControllerContext controllerContext, IList<IActionFilter> filters, ActionDescriptor actionDescriptor, IDictionary<string, object> parameters)
        {
            Inject(filters);

            return base.InvokeActionMethodWithFilters(controllerContext, filters, actionDescriptor, parameters);
        }

        protected override ResultExecutedContext InvokeActionResultWithFilters(ControllerContext controllerContext, IList<IResultFilter> filters, ActionResult actionResult)
        {
            Inject(filters);

            return base.InvokeActionResultWithFilters(controllerContext, filters, actionResult);
        }

        protected override ExceptionContext InvokeExceptionFilters(ControllerContext controllerContext, IList<IExceptionFilter> filters, Exception exception)
        {
            Inject(filters);

            return base.InvokeExceptionFilters(controllerContext, filters, exception);
        }

        private void Inject<TFilter>(IEnumerable<TFilter> filters) where TFilter : class
        {
            IInjection injection = ServiceLocator as IInjection;

            if (injection != null)
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
}