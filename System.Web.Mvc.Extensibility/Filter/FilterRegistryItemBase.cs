namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;

    public abstract class FilterRegistryItemBase
    {
        public IEnumerable<FilterAttribute> Filters
        {
            get;
            protected set;
        }

        public abstract bool IsMatching(ControllerContext controllerContext, ActionDescriptor actionDescriptor);
    }
}