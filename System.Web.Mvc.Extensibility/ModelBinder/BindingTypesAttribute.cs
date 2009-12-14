#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true), CLSCompliant(false)]
    public sealed class BindingTypesAttribute : Attribute
    {
        public BindingTypesAttribute(params Type[] types)
        {
            Invariant.IsNotNull(types, "types");

            Types = types;
        }

        public IEnumerable<Type> Types
        {
            get;
            private set;
        }
    }
}