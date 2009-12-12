namespace Demo.Web.StructureMap
{
    using System;

    using Registry = global::StructureMap.Configuration.DSL.Registry;

    public class RegisterServices : Registry
    {
        private static readonly Type repositoryType = typeof(IRepository<>);

        public RegisterServices()
        {
            ForRequestedType<IDatabase>().TheDefaultIsConcreteType<InMemoryDatabasae>();
            ForRequestedType(repositoryType).TheDefaultIsConcreteType(typeof(Repository<>));
            SetAllProperties(policy => policy.TypeMatches(type => type.IsGenericType && type.GetGenericTypeDefinition() == repositoryType));
        }
    }
}