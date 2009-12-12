namespace System.Web.Mvc.Extensibility
{
    using Microsoft.Practices.ServiceLocation;

    public interface IBootstrapperTask : IDisposable
    {
        void Execute(IServiceLocator locator);
    }
}