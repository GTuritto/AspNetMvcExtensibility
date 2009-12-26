#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// Defines a base class which is executed when <see cref="ExtendedMvcApplicationBase"/> starts.
    /// </summary>
    public abstract class BootstrapperTaskBase : DisposableBase, IBootstrapperTask
    {
        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <param name="serviceLocator">The service locator.</param>
        public void Execute(IServiceLocator serviceLocator)
        {
            Invariant.IsNotNull(serviceLocator, "serviceLocator");

            ExecuteCore(serviceLocator);
        }

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <param name="serviceLocator">The service locator.</param>
        protected abstract void ExecuteCore(IServiceLocator serviceLocator);
    }
}