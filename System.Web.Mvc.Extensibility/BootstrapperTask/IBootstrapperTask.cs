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
    /// Represents an interface which is executed when <see cref="ExtendedMvcApplicationBase"/> starts.
    /// </summary>
    public interface IBootstrapperTask : IDisposable
    {
        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <param name="serviceLocator">The service locator.</param>
        void Execute(IServiceLocator serviceLocator);
    }
}