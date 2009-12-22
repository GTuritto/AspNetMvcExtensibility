#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    public abstract class PerRequestTaskBase : DisposableBase, IPerRequestTask
    {
        protected PerRequestTaskBase()
        {
            Order = -1;
        }

        public int Order
        {
            get;
            protected set;
        }

        public void Execute(PerRequestExecutionContext executionContext)
        {
            Invariant.IsNotNull(executionContext, "executionContext");

            ExecuteCore(executionContext);
        }

        public abstract void ExecuteCore(PerRequestExecutionContext executionContext);
    }
}