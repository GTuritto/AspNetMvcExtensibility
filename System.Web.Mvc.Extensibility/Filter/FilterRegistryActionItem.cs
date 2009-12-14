#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

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

            if ((methodCall == null) || !KnownTypes.ActionResultType.IsAssignableFrom(methodCall.Method.ReturnType))
            {
                throw new ArgumentException(ExceptionMessages.TheExpressionMustBeAValidControllerAction, "action");
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
                   IsSameAction(reflectedActionDescriptor, actionDescriptor);
        }

        private static bool IsSameAction(ActionDescriptor descriptor1, ActionDescriptor descriptor2)
        {
            ParameterDescriptor[] parameters1 = descriptor1.GetParameters();
            ParameterDescriptor[] parameters2 = descriptor2.GetParameters();

            bool isSame = descriptor1.ControllerDescriptor.ControllerName.Equals(descriptor2.ControllerDescriptor.ControllerName, StringComparison.OrdinalIgnoreCase) &&
                          descriptor1.ActionName.Equals(descriptor2.ActionName, StringComparison.OrdinalIgnoreCase) && 
                          (parameters1.Length == parameters2.Length);

            if (isSame)
            {
                for (int i = parameters1.Length - 1; i >= 0; i--)
                {
                    if (parameters1[i].ParameterType != parameters2[i].ParameterType)
                    {
                        isSame = false;
                        break;
                    }
                }
            }

            return isSame;
        }
    }
}