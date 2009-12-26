#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    /// <summary>
    /// Represents an interface which is executed for each request. This is similar to <seealso cref="IHttpModule"/> with only begin and end support.
    /// </summary>
    public interface IPerRequestTask : IDisposable
    {
        /// <summary>
        /// Gets the order that the task would execute.
        /// </summary>
        /// <value>The order.</value>
        int Order
        {
            get;
        }

        /// <summary>
        /// Executes the task with the given context.
        /// </summary>
        /// <param name="executionContext">The execution context.</param>
        void Execute(PerRequestExecutionContext executionContext);
    }
}