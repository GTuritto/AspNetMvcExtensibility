#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using Diagnostics;
    using Linq;
    using Reflection;
    using Compilation;

    /// <summary>
    /// Defines a wrapper class which provides access to the internal <seealso cref="BuildManager"/>.
    /// </summary>
    public class BuildManagerWrapper : IBuildManager
    {
        private static readonly IBuildManager current = new BuildManagerWrapper();
        private IEnumerable<Assembly> referencedAssemblies;

        private BuildManagerWrapper()
        {
        }

        /// <summary>
        /// Gets the current <see cref="IBuildManager"/>.
        /// </summary>
        /// <value>The current.</value>
        public static IBuildManager Current
        {
            [DebuggerStepThrough]
            get
            {
                return current;
            }
        }

        /// <summary>
        /// Gets the assemblies.
        /// </summary>
        /// <value>The assemblies.</value>
        public virtual IEnumerable<Assembly> Assemblies
        {
            [DebuggerStepThrough]
            get
            {
                return referencedAssemblies ?? (referencedAssemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>());
            }
        }

        /// <summary>
        /// Gets the available concrete types of <see cref="Assemblies"/>.
        /// </summary>
        /// <value>The concrete types.</value>
        public IEnumerable<Type> ConcreteTypes
        {
            [DebuggerStepThrough]
            get
            {
                return Assemblies.ConcreteTypes();
            }
        }
    }
}