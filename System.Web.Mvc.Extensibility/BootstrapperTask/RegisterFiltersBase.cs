namespace System.Web.Mvc.Extensibility
{
    public abstract class RegisterFiltersBase : BootstrapperTaskBase
    {
        protected RegisterFiltersBase(IFilterRegistry filterRegistry)
        {
            Invariant.IsNotNull(filterRegistry, "filterRegistry");

            FilterRegistry = filterRegistry;
        }

        protected IFilterRegistry FilterRegistry
        {
            get;
            private set;
        }
    }
}