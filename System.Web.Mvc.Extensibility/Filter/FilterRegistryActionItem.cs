namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using Linq.Expressions;

    public class FilterRegistryActionItem<TController> : FilterRegistryItemBase where TController : Controller
    {
        private readonly ReflectedActionDescriptor reflectedActionDescriptor;

        public FilterRegistryActionItem(Expression<Action<TController>> action, IEnumerable<FilterAttribute> filters)
        {
            Invariant.IsNotNull(action, "action");
            Invariant.IsNotNull(filters, "filters");

            MethodCallExpression methodCall = action.Body as MethodCallExpression;

            if (methodCall == null)
            {
                throw new InvalidOperationException("The expression must be a valid method call.");
            }

            reflectedActionDescriptor = new ReflectedActionDescriptor(methodCall.Method, methodCall.Method.Name, new ReflectedControllerDescriptor(methodCall.Object.Type));
            Filters = filters;
        }

        public override bool IsMatching(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            Invariant.IsNotNull(controllerContext, "controllerContext");
            Invariant.IsNotNull(actionDescriptor, "actionDescriptor");

            ReflectedActionDescriptor matchingDescriptor = actionDescriptor as ReflectedActionDescriptor;

            return (matchingDescriptor != null) ?
                   reflectedActionDescriptor.MethodInfo == matchingDescriptor.MethodInfo :
                   reflectedActionDescriptor.ControllerDescriptor.FindAction(controllerContext, actionDescriptor.ActionName) != null;
        }
    }
}