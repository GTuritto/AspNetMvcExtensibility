#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using Diagnostics;
    using Linq;
    using Reflection;
    using Compilation;

    public class BuildManagerWrapper : IBuildManager
    {
        private static readonly IBuildManager current = new BuildManagerWrapper();
        private IEnumerable<Assembly> referencedAssemblies;

        public static IBuildManager Current
        {
            [DebuggerStepThrough]
            get
            {
                return current;
            }
        }

        public IEnumerable<Assembly> Assemblies
        {
            [DebuggerStepThrough]
            get
            {
                return referencedAssemblies ?? (referencedAssemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>());
            }
        }

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