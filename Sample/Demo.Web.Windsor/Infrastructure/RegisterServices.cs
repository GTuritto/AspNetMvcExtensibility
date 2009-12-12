namespace Demo.Web.Windsor
{
    using Castle.Core;
    using Castle.Windsor;

    public class RegisterServices : System.Web.Mvc.Extensibility.Windsor.IModule
    {
        public void Load(IWindsorContainer container)
        {
            container.AddComponentLifeStyle<IDatabase, InMemoryDatabasae>(typeof(IDatabase).FullName, LifestyleType.Singleton)
                     .AddComponent(typeof(IRepository<>).FullName, typeof(IRepository<>), typeof(Repository<>));
        }
    }
}