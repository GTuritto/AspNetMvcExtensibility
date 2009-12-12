namespace System.Web.Mvc.Extensibility
{
    using Microsoft.Practices.ServiceLocation;

    public abstract class BootstrapperTaskBase : DisposableBase, IBootstrapperTask
    {
        public void Execute(IServiceLocator locator)
        {
            Invariant.IsNotNull(locator, "locator");

            ExecuteCore(locator);
        }

        protected abstract void ExecuteCore(IServiceLocator locator);
    }
}