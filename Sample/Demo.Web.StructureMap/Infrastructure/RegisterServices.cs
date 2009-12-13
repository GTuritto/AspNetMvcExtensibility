namespace Demo.Web.StructureMap
{
    using Registry = global::StructureMap.Configuration.DSL.Registry;

    public class RegisterServices : Registry
    {
        public RegisterServices()
        {
            ForRequestedType<IDatabase>().TheDefaultIsConcreteType<InMemoryDatabasae>();
            ForRequestedType(typeof(IRepository<>)).TheDefaultIsConcreteType(typeof(Repository<>));
        }
    }
}