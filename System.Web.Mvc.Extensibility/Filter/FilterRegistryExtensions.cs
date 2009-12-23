#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using Linq.Expressions;
    using Linq;

    public static class FilterRegistryExtensions
    {
        private static readonly Type genericControllerItemType = typeof(FilterRegistryControllerItem<>);

        public static IFilterRegistry Register<TFilter>(this IFilterRegistry instance, TypeCatalog typeCatalog)
            where TFilter : FilterAttribute
        {
            return Register<TFilter>(instance, typeCatalog, filter => { });
        }

        public static IFilterRegistry Register<TFilter>(this IFilterRegistry instance, TypeCatalog typeCatalog, Action<TFilter> configureFilter)
            where TFilter : FilterAttribute
        {
            Invariant.IsNotNull(instance, "instance");
            Invariant.IsNotNull(typeCatalog, "typeCatalog");
            Invariant.IsNotNull(configureFilter, "configureFilter");

            IList<Type> controllerTypes = typeCatalog.ToList();

            EnsureControllerTypes(controllerTypes);

            foreach (Type controllerType in controllerTypes)
            {
                Type itemType = genericControllerItemType.MakeGenericType(controllerType);

                instance.Items.Add(Activator.CreateInstance(itemType, new object[] { instance.CreateAndConfigureFilterFactory(configureFilter) }) as FilterRegistryItemBase);
            }

            return instance;
        }

        public static IFilterRegistry Register<TFilter1, TFilter2>(this IFilterRegistry instance, TypeCatalog typeCatalog)
            where TFilter1 : FilterAttribute
            where TFilter2 : FilterAttribute
        {
            Invariant.IsNotNull(instance, "instance");
            Invariant.IsNotNull(typeCatalog, "typeCatalog");

            return Register(instance, typeCatalog, typeof(TFilter1), typeof(TFilter2));
        }

        public static IFilterRegistry Register<TFilter1, TFilter2, TFilter3>(this IFilterRegistry instance, TypeCatalog typeCatalog)
            where TFilter1 : FilterAttribute
            where TFilter2 : FilterAttribute
            where TFilter3 : FilterAttribute
        {
            Invariant.IsNotNull(instance, "instance");
            Invariant.IsNotNull(typeCatalog, "typeCatalog");

            return Register(instance, typeCatalog, typeof(TFilter1), typeof(TFilter2), typeof(TFilter3));
        }

        public static IFilterRegistry Register<TFilter1, TFilter2, TFilter3, TFilter4>(this IFilterRegistry instance, TypeCatalog typeCatalog)
            where TFilter1 : FilterAttribute
            where TFilter2 : FilterAttribute
            where TFilter3 : FilterAttribute
            where TFilter4 : FilterAttribute
        {
            Invariant.IsNotNull(instance, "instance");
            Invariant.IsNotNull(typeCatalog, "assembly");

            return Register(instance, typeCatalog, typeof(TFilter1), typeof(TFilter2), typeof(TFilter3), typeof(TFilter4));
        }

        public static IFilterRegistry Register<TController, TFilter>(this IFilterRegistry instance)
            where TController : Controller
            where TFilter : FilterAttribute
        {
            return Register<TController, TFilter>(instance, (TFilter filter) => { });
        }

        public static IFilterRegistry Register<TController, TFilter>(this IFilterRegistry instance, Action<TFilter> configureFilter)
            where TController : Controller
            where TFilter : FilterAttribute
        {
            Invariant.IsNotNull(instance, "instance");
            Invariant.IsNotNull(configureFilter, "configureFilter");

            instance.Register<TController, FilterAttribute>(CreateAndConfigureFilterFactory(instance, configureFilter));

            return instance;
        }

        public static IFilterRegistry Register<TController, TFilter1, TFilter2>(this IFilterRegistry instance)
            where TController : Controller
            where TFilter1 : FilterAttribute
            where TFilter2 : FilterAttribute
        {
            Invariant.IsNotNull(instance, "instance");

            return instance.Register<TController, FilterAttribute>(instance.CreateFilterFactories(typeof(TFilter1), typeof(TFilter2)).ToArray());
        }

        public static IFilterRegistry Register<TController, TFilter1, TFilter2, TFilter3>(this IFilterRegistry instance)
            where TController : Controller
            where TFilter1 : FilterAttribute
            where TFilter2 : FilterAttribute
            where TFilter3 : FilterAttribute
        {
            Invariant.IsNotNull(instance, "instance");

            return instance.Register<TController, FilterAttribute>(instance.CreateFilterFactories(typeof(TFilter1), typeof(TFilter2), typeof(TFilter3)).ToArray());
        }

        public static IFilterRegistry Register<TController, TFilter1, TFilter2, TFilter3, TFilter4>(this IFilterRegistry instance)
            where TController : Controller
            where TFilter1 : FilterAttribute
            where TFilter2 : FilterAttribute
            where TFilter3 : FilterAttribute
            where TFilter4 : FilterAttribute
        {
            Invariant.IsNotNull(instance, "instance");

            return instance.Register<TController, FilterAttribute>(instance.CreateFilterFactories(typeof(TFilter1), typeof(TFilter2), typeof(TFilter3), typeof(TFilter4)).ToArray());
        }

        public static IFilterRegistry Register<TController, TFilter>(this IFilterRegistry instance, Expression<Action<TController>> action)
            where TController : Controller
            where TFilter : FilterAttribute
        {
            Invariant.IsNotNull(instance, "instance");

            return Register<TController, TFilter>(instance, action, filter => { });
        }

        public static IFilterRegistry Register<TController, TFilter>(this IFilterRegistry instance, Expression<Action<TController>> action, Action<TFilter> configureFilter)
            where TController : Controller
            where TFilter : FilterAttribute
        {
            Invariant.IsNotNull(instance, "instance");
            Invariant.IsNotNull(action, "action");
            Invariant.IsNotNull(configureFilter, "configureFilter");

            return instance.Register(action, instance.CreateAndConfigureFilterFactory(configureFilter));
        }

        public static IFilterRegistry Register<TController, TFilter1, TFilter2>(this IFilterRegistry instance, Expression<Action<TController>> action)
            where TController : Controller
            where TFilter1 : FilterAttribute
            where TFilter2 : FilterAttribute
        {
            Invariant.IsNotNull(instance, "instance");

            return instance.Register(action, instance.CreateFilterFactories(typeof(TFilter1), typeof(TFilter2)).ToArray());
        }

        public static IFilterRegistry Register<TController, TFilter1, TFilter2, TFilter3>(this IFilterRegistry instance, Expression<Action<TController>> action)
            where TController : Controller
            where TFilter1 : FilterAttribute
            where TFilter2 : FilterAttribute
            where TFilter3 : FilterAttribute
        {
            Invariant.IsNotNull(instance, "instance");

            return instance.Register(action, instance.CreateFilterFactories(typeof(TFilter1), typeof(TFilter2), typeof(TFilter3)).ToArray());
        }

        public static IFilterRegistry Register<TController, TFilter1, TFilter2, TFilter3, TFilter4>(this IFilterRegistry instance, Expression<Action<TController>> action)
            where TController : Controller
            where TFilter1 : FilterAttribute
            where TFilter2 : FilterAttribute
            where TFilter3 : FilterAttribute
            where TFilter4 : FilterAttribute
        {
            Invariant.IsNotNull(instance, "instance");

            return instance.Register(action, instance.CreateFilterFactories(typeof(TFilter1), typeof(TFilter2), typeof(TFilter3), typeof(TFilter4)).ToArray());
        }

        private static IFilterRegistry Register(IFilterRegistry instance, IEnumerable<Type> typeCatalog, params Type[] filterTypes)
        {
            IList<Type> controllerTypes = typeCatalog.ToList();

            EnsureControllerTypes(controllerTypes);

            foreach (Type controllerType in controllerTypes)
            {
                Type itemType = genericControllerItemType.MakeGenericType(controllerType);

                IEnumerable<Func<FilterAttribute>> filters = instance.CreateFilterFactories(filterTypes);

                instance.Items.Add(Activator.CreateInstance(itemType, new object[] { filters }) as FilterRegistryItemBase);
            }

            return instance;
        }

        private static void EnsureControllerTypes(IEnumerable<Type> typeCatalog)
        {
            foreach (Type controllerType in typeCatalog)
            {
                if (!KnownTypes.ControllerType.IsAssignableFrom(controllerType))
                {
                    throw new ArgumentException(string.Format(Globalization.CultureInfo.CurrentCulture, "\"{0}\" is not a \"{1}\" type.", controllerType.FullName, KnownTypes.ControllerType.FullName));
                }
            }
        }

        private static IEnumerable<Func<FilterAttribute>> CreateFilterFactories(this IFilterRegistry instance, params Type[] filterTypes)
        {
            IList<Func<FilterAttribute>> filters = new List<Func<FilterAttribute>>();

            foreach (Type filterType in filterTypes)
            {
                Type type = filterType;

                filters.Add(() => instance.ServiceLocator.GetInstance(type) as FilterAttribute);
            }

            return filters;
        }

        private static IEnumerable<Func<FilterAttribute>> CreateAndConfigureFilterFactory<TFilter>(this IFilterRegistry instance, Action<TFilter> configureFilter) where TFilter : FilterAttribute
        {
            return new List<Func<FilterAttribute>>
                       {
                           () =>
                           {
                               TFilter filter = instance.ServiceLocator.GetInstance<TFilter>();

                               configureFilter(filter);

                               return filter;
                           }
                       };
        }
    }
}