#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Unity
{
    using Collections.Generic;
    using Microsoft.Practices.Unity;

    internal sealed class InternalUnityContainer : UnityContainer
    {
        public override IEnumerable<object> ResolveAll(Type t)
        {
            Invariant.IsNotNull(t, "t");

            IList<object> instances = new List<object>();

            if (InternalUnityExtension.IsRegistered(this, t))
            {
                instances.Add(Resolve(t));
            }

            foreach (object instance in base.ResolveAll(t))
            {
                instances.Add(instance);
            }

            return instances;
        }
    }
}