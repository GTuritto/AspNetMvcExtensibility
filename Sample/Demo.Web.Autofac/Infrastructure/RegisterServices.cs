namespace Demo.Web.Autofac
{
    using ContainerBuilder = global::Autofac.Builder.ContainerBuilder;

    public class RegisterServices : global::Autofac.Builder.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<InMemoryDatabasae>().As<IDatabase>();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
        }
    }
}