#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.StructureMap
{
    using Collections.Generic;
    using Linq;
    using Reflection;

    using Microsoft.Practices.ServiceLocation;

    using ConfiguredInstance = global::StructureMap.Pipeline.ConfiguredInstance;
    using ConfigurrationExpression = global::StructureMap.ConfigurationExpression;
    using IContainer = global::StructureMap.IContainer;
    using Instance = global::StructureMap.Pipeline.Instance;
    using InstanceScope = global::StructureMap.Attributes.InstanceScope;

    /// <summary>
    /// Defines an adapter class which with backed by StructureMap <seealso cref="IContainer">Container</seealso>.
    /// </summary>
    [CLSCompliant(false)]
    public class StructureMapAdapter : ServiceLocatorImplBase, IRegistrar, IInjector
    {
        private static readonly MethodInfo forRequestTypeMethod = typeof(ConfigurrationExpression).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                                                                                  .Where(method => method.Name.Equals("ForRequestedType", StringComparison.Ordinal) && method.GetGenericArguments().Length == 1)
                                                                                                  .FirstOrDefault();

        private static readonly Type instanceType = typeof(Instance);

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureMapAdapter"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public StructureMapAdapter(IContainer container)
        {
            Invariant.IsNotNull(container, "container");

            Container = container;
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>The container.</value>
        public IContainer Container
        {
            get;
            private set;
        }

        /// <summary>
        /// Registers the type.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        /// <param name="asSingleton">if set to <c>true</c> [as singleton].</param>
        /// <returns></returns>
        public virtual IRegistrar RegisterType(string key, Type serviceType, Type implementationType, bool asSingleton)
        {
            Invariant.IsNotNull(serviceType, "serviceType");
            Invariant.IsNotNull(implementationType, "implementationType");

            Container.Configure(x =>
                                {
                                    ConfiguredInstance expression;

                                    if (asSingleton)
                                    {
                                        expression = x.ForRequestedType(serviceType)
                                                          .CacheBy(InstanceScope.Singleton)
                                                          .TheDefaultIsConcreteType(implementationType);
                                    }
                                    else
                                    {
                                        expression = x.ForRequestedType(serviceType)
                                                      .TheDefaultIsConcreteType(implementationType);
                                    }

                                    if (!string.IsNullOrEmpty(key))
                                    {
                                        expression.WithName(key);
                                    }
                                });

            return this;
        }

        /// <summary>
        /// Registers the instance.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public virtual IRegistrar RegisterInstance(string key, Type serviceType, object instance)
        {
            Invariant.IsNotNull(serviceType, "serviceType");
            Invariant.IsNotNull(instance, "instance");

            Container.Configure(x =>
                                {
                                    MethodInfo genericForRequestType = forRequestTypeMethod.MakeGenericMethod(serviceType);

                                    object expression = genericForRequestType.Invoke(x, null);

                                    object theDefault = expression.GetType()
                                                                  .GetProperty("TheDefault", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty)
                                                                  .GetGetMethod()
                                                                  .Invoke(expression, null);

                                    object isThis = theDefault.GetType()
                                                              .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                                                              .Where(method => method.Name.Equals("IsThis") && method.GetParameters().Length == 1 && method.GetParameters()[0].ParameterType != instanceType)
                                                              .FirstOrDefault()
                                                              .Invoke(theDefault, new[] { instance });

                                    if (!string.IsNullOrEmpty(key))
                                    {
                                        isThis.GetType().GetMethod("WithName", BindingFlags.Public | BindingFlags.Instance).Invoke(isThis, new object[] { key });
                                    }
                                });

            return this;
        }

        /// <summary>
        /// Injects the matching dependences.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public virtual void Inject(object instance)
        {
            if (instance != null)
            {
                Container.BuildUp(instance);
            }
        }

        /// <summary>
        /// Gets the matching instance for the given type and key.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected override object DoGetInstance(Type serviceType, string key)
        {
            return string.IsNullOrEmpty(key) ? Container.GetInstance(serviceType) : Container.GetInstance(serviceType, key);
        }

        /// <summary>
        /// Gets all the instances for the given type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return Container.GetAllInstances(serviceType).Cast<object>();
        }
    }
}