namespace Demo.Web.Unity
{
    using Microsoft.Practices.Unity;

    public class RegisterServices : System.Web.Mvc.Extensibility.Unity.IModule
    {
        public void Load(IUnityContainer container)
        {
            container.RegisterType<IDatabase, InMemoryDatabasae>(new ContainerControlledLifetimeManager())
                     .RegisterType(typeof(IRepository<>), typeof(Repository<>), new ContainerControlledLifetimeManager());
        }
    }
}