#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;

    /// <summary>
    /// Defines an attribute class to provides type information for which the <seealso cref="IModelBinder"/> is registered.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true), CLSCompliant(false)]
    public sealed class BindingTypesAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BindingTypesAttribute"/> class.
        /// </summary>
        /// <param name="types">The types.</param>
        public BindingTypesAttribute(params Type[] types)
        {
            Invariant.IsNotNull(types, "types");

            Types = types;
        }

        /// <summary>
        /// Gets the types that the <seealso cref="IModelBinder"/> is registered for.
        /// </summary>
        /// <value>The types.</value>
        public IEnumerable<Type> Types
        {
            get;
            private set;
        }
    }
}