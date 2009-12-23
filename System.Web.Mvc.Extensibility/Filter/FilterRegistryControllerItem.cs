#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;

    public class FilterRegistryControllerItem<TController> : FilterRegistryItemBase where TController : Controller
    {
        private readonly Type controllerType = typeof(TController);

        public FilterRegistryControllerItem(IEnumerable<Func<FilterAttribute>> filters)
        {
            Invariant.IsNotNull(filters, "filters");

            Filters = filters;
        }

        public override bool IsMatching(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            Invariant.IsNotNull(controllerContext, "controllerContext");

            return controllerType.IsAssignableFrom(controllerContext.Controller.GetType());
        }
    }
}