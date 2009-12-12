namespace System.Web.Mvc.Extensibility.Unity
{
    using Microsoft.Practices.Unity;

    public interface IModule
    {
        void Load(IUnityContainer container);
    }
}