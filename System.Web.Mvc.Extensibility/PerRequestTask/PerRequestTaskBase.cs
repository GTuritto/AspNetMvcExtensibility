#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    /// <summary>
    /// Defines a base class which is executed for each request. This is similar to <seealso cref="IHttpModule"/> with only begin and end support.
    /// </summary>
    public abstract class PerRequestTaskBase : DisposableBase, IPerRequestTask
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PerRequestTaskBase"/> class.
        /// </summary>
        protected PerRequestTaskBase()
        {
            Order = int.MaxValue;
        }

        /// <summary>
        /// Gets the order that the task would execute.
        /// </summary>
        /// <value>The order.</value>
        public int Order
        {
            get;
            protected set;
        }

        /// <summary>
        /// Executes the task with the given context.
        /// </summary>
        /// <param name="executionContext">The execution context.</param>
        public void Execute(PerRequestExecutionContext executionContext)
        {
            Invariant.IsNotNull(executionContext, "executionContext");

            ExecuteCore(executionContext);
        }

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <param name="executionContext">The execution context.</param>
        protected abstract void ExecuteCore(PerRequestExecutionContext executionContext);
    }
}