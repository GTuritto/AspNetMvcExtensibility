namespace Demo.Web.Windsor
{
    using System.Web.Mvc.Extensibility.Windsor;

    using Castle.Windsor;

    public class RegisterServices : IModule
    {
        public void Load(IWindsorContainer container)
        {
            container.AddComponent<IDatabase, InMemoryDatabasae>(typeof(IDatabase).FullName)
                     .AddComponent(typeof(IRepository<>).FullName, typeof(IRepository<>), typeof(Repository<>));
        }
    }
}