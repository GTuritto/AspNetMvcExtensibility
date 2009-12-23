#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using ComponentModel;
    using Linq;
    using Reflection;

    public class TypeCatalogBuilder : IFluentSyntax
    {
        public TypeCatalogBuilder() : this(new TypeCatalog())
        {
        }

        protected TypeCatalogBuilder(TypeCatalog typeCatalog)
        {
            Invariant.IsNotNull(typeCatalog, "typeCatalog");

            TypeCatalog = typeCatalog;
        }

        public TypeCatalog TypeCatalog
        {
            [EditorBrowsable(EditorBrowsableState.Never)]
            get;
            private set;
        }

        public static implicit operator TypeCatalog(TypeCatalogBuilder builder)
        {
            return ToTypeCatalog(builder);
        }

        public TypeCatalogBuilder Add(params Assembly[] assemblies)
        {
            Invariant.IsNotNull(assemblies, "assemblies");

            if (assemblies.Any())
            {
                foreach (Assembly assembly in assemblies)
                {
                    if (!TypeCatalog.Assemblies.Contains(assembly))
                    {
                        TypeCatalog.Assemblies.Add(assembly);
                    }
                }
            }

            return this;
        }

        public TypeCatalogBuilder Add(params string[] assemblyNames)
        {
            Invariant.IsNotNull(assemblyNames, "assemblyNames");

            if (assemblyNames.Any())
            {
                Add(assemblyNames.Select(assemblyName => Assembly.Load(assemblyName)).ToArray());
            }

            return this;
        }

        public TypeCatalogBuilder Include(params Type[] types)
        {
            Invariant.IsNotNull(types, "types");

            if (types.Any())
            {
                types.Each(type => Include(scannedType => scannedType == type));
            }

            return this;
        }

        public TypeCatalogBuilder Include(params string[] typeNames)
        {
            Invariant.IsNotNull(typeNames, "typeNames");

            if (typeNames.Any())
            {
                Include(typeNames.Select(name => Type.GetType(name)).ToArray());
            }

            return this;
        }

        public TypeCatalogBuilder Include(Predicate<Type> filter)
        {
            Invariant.IsNotNull(filter, "filter");

            TypeCatalog.IncludeFilters.Add(filter);

            return this;
        }

        public TypeCatalogBuilder Exclude(params Type[] types)
        {
            Invariant.IsNotNull(types, "types");

            if (types.Any())
            {
                types.Each(type => Exclude(scannedType => scannedType == type));
            }

            return this;
        }

        public TypeCatalogBuilder Exclude(params string[] typeNames)
        {
            Invariant.IsNotNull(typeNames, "typeNames");

            if (typeNames.Any())
            {
                Exclude(typeNames.Select(name => Type.GetType(name)).ToArray());
            }

            return this;
        }

        public TypeCatalogBuilder Exclude(Predicate<Type> filter)
        {
            Invariant.IsNotNull(filter, "filter");

            TypeCatalog.ExcludeFilters.Add(filter);

            return this;
        }

        private static TypeCatalog ToTypeCatalog(TypeCatalogBuilder builder)
        {
            Invariant.IsNotNull(builder, "builder");

            return builder.TypeCatalog;
        }
    }
}