#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using Reflection;

    /// <summary>
    /// Represents an interface which provides access to all the referenced assemlies.
    /// </summary>
    public interface IBuildManager
    {
        /// <summary>
        /// Gets the available assemblies.
        /// </summary>
        /// <value>The assemblies.</value>
        IEnumerable<Assembly> Assemblies
        {
            get;
        }

        /// <summary>
        /// Gets the available concrete types of <see cref="Assemblies"/>.
        /// </summary>
        /// <value>The concrete types.</value>
        IEnumerable<Type> ConcreteTypes
        {
            get;
        }
    }
}