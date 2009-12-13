namespace Demo.Web.Unity
{
    using System.Web.Mvc.Extensibility.Unity;

    using Microsoft.Practices.Unity;

    public class RegisterServices : IModule
    {
        public void Load(IUnityContainer container)
        {
            container.RegisterType<IDatabase, InMemoryDatabasae>()
                     .RegisterType(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}