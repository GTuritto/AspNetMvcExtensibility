namespace System.Web.Mvc.Extensibility
{
    using Microsoft.Practices.ServiceLocation;

    public abstract class BootstrapperTaskBase : DisposableBase, IBootstrapperTask
    {
        public void Execute(IServiceLocator serviceLocator)
        {
            Invariant.IsNotNull(serviceLocator, "serviceLocator");

            ExecuteCore(serviceLocator);
        }

        protected abstract void ExecuteCore(IServiceLocator serviceLocator);
    }
}