#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Windsor
{
    /// <summary>
    /// Defines a <see cref="HttpApplication"/> which is backed by Windsor Container.
    /// </summary>
    public class WindsorMvcApplication : ExtendedMvcApplicationBase
    {
        /// <summary>
        /// Creates the bootstrapper.
        /// </summary>
        /// <returns></returns>
        protected override IBootstrapper CreateBootstrapper()
        {
            return new WindsorBootstrapper(BuildManagerWrapper.Current);
        }
    }
}