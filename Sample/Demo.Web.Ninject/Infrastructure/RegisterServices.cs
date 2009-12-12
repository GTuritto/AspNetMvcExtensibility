namespace Demo.Web.Ninject
{
    public class RegisterServices : global::Ninject.Modules.Module
    {
        public override void Load()
        {
            Bind<IDatabase>().To<InMemoryDatabasae>().InSingletonScope();
            Bind(typeof(IRepository<>)).To(typeof (Repository<>)).InRequestScope();
        }
    }
}