namespace System.Web.Mvc.Extensibility
{
    using Microsoft.Practices.ServiceLocation;

    public interface IBootstrapper : IDisposable
    {
        IServiceLocator ServiceLocator
        {
            get;
        }

        void Execute();
    }
}