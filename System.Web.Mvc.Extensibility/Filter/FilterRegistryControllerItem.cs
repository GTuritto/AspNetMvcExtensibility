namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;

    public class FilterRegistryControllerItem<TController> : FilterRegistryItemBase where TController : Controller
    {
        private readonly Type controllerType = typeof(TController);

        public FilterRegistryControllerItem(IEnumerable<FilterAttribute> filters)
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