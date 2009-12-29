namespace Demo.Web.Ninject
{
    public class RegisterServices : global::Ninject.Modules.Module
    {
        public override void Load()
        {
            Bind<IDatabase>().To<InMemoryDatabasae>();
            Bind(typeof(IRepository<>)).To(typeof(Repository<>));
        }
    }
}