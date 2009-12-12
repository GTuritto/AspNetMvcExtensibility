namespace System.Web.Mvc.Extensibility.Windsor
{
    using Castle.Windsor;

    public interface IModule
    {
        void Load(IWindsorContainer container);
    }
}