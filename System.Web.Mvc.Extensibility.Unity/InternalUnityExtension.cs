#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Unity
{
    using Collections.Generic;

    using Microsoft.Practices.ObjectBuilder2;
    using Microsoft.Practices.Unity;

    internal sealed class InternalUnityExtension : UnityContainerExtension
    {
        private readonly Dictionary<Type, HashSet<string>> registeredTypes = new Dictionary<Type, HashSet<string>>();

        public static bool IsRegistered(IUnityContainer container, Type type)
        {
            InternalUnityExtension extension = container.Configure<InternalUnityExtension>();
            IBuildKeyMappingPolicy policy = null;

            if (extension != null)
            {
                policy = extension.Context.Policies.Get<IBuildKeyMappingPolicy>(new NamedTypeBuildKey(type));
            }

            return policy != null;
        }

        public override void Remove()
        {
            Context.RegisteringInstance -= OnRegisteringInstance;
            Context.Registering -= OnRegistering;

            base.Remove();
        }

        protected override void Initialize()
        {
            Context.Container.Configure<UnityDefaultBehaviorExtension>().Remove();
            Context.Container.Configure<InjectedMembers>().Remove();

            Context.RegisteringInstance += OnRegisteringInstance;
            Context.Registering += OnRegistering;

            Context.Container.Configure<UnityDefaultBehaviorExtension>().InitializeExtension(Context);
            Context.Container.Configure<InjectedMembers>().InitializeExtension(Context);
        }

        private void OnRegisteringInstance(object sender, RegisterInstanceEventArgs e)
        {
            string returnedName = EnsureAndReturnName(e.Name, e.RegisteredType);

            if (!string.IsNullOrEmpty(returnedName))
            {
                e.Name = returnedName;
            }
        }

        private void OnRegistering(object sender, RegisterEventArgs e)
        {
            string returnedName = EnsureAndReturnName(e.Name, e.TypeFrom);

            if (!string.IsNullOrEmpty(returnedName))
            {
                e.Name = returnedName;
            }
        }

        private string EnsureAndReturnName(string name, Type type)
        {
            HashSet<string> names;

            name = string.IsNullOrEmpty(name) ? string.Empty : name;

            if (!registeredTypes.TryGetValue(type, out names))
            {
                registeredTypes.Add(type, new HashSet<string> { name });
            }
            else
            {
                if (string.IsNullOrEmpty(name) && names.Contains(name))
                {
                    name = Guid.NewGuid().ToString();
                }

                names.Add(name);
            }

            return name;
        }
    }
}