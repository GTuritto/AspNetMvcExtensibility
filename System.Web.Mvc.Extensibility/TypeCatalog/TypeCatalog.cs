#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections;
    using ComponentModel;
    using Collections.Generic;
    using Linq;
    using Reflection;

    public class TypeCatalog : IEnumerable<Type>
    {
        public TypeCatalog() : this(new List<Assembly>(), new List<Predicate<Type>>(), new List<Predicate<Type>>())
        {
        }

        protected TypeCatalog(IList<Assembly> assemblies, IList<Predicate<Type>> includeFilters, IList<Predicate<Type>> excludeFilters)
        {
            Invariant.IsNotNull(assemblies, "assemblies");
            Invariant.IsNotNull(assemblies, "includeFilters");
            Invariant.IsNotNull(assemblies, "excludeFilters");

            Assemblies = assemblies;
            IncludeFilters = includeFilters;
            ExcludeFilters = excludeFilters;
        }

        public IList<Assembly> Assemblies
        {
            [EditorBrowsable(EditorBrowsableState.Never)]
            get;
            private set;
        }

        public IList<Predicate<Type>> IncludeFilters
        {
            [EditorBrowsable(EditorBrowsableState.Never)]
            get;
            private set;
        }

        public IList<Predicate<Type>> ExcludeFilters
        {
            [EditorBrowsable(EditorBrowsableState.Never)]
            get;
            private set;
        }

        public IEnumerator<Type> GetEnumerator()
        {
            bool hasIncludeFilters = (IncludeFilters.Count > 0);

            IEnumerable<Type> filteredTypes = Assemblies.SelectMany(assembly => assembly.GetExportedTypes())
                                                        .Where(type => !hasIncludeFilters || IncludeFilters.Any(filter => filter(type)))
                                                        .Where(type => !ExcludeFilters.Any(filter => filter(type)));

            foreach (Type type in filteredTypes)
            {
                yield return type;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}