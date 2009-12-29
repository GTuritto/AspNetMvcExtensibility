#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using System;
    using Diagnostics;
    using Reflection;

    /// <summary>
    /// 
    /// </summary>
    public class DelegateReference : IDelegateReference
    {
        private readonly Delegate @delegate;
        private readonly WeakReference weakReference;
        private readonly MethodInfo method;
        private readonly Type delegateType;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateReference"/> class.
        /// </summary>
        /// <param name="delegate">The @delegate.</param>
        /// <param name="keepReferenceAlive">if set to <c>true</c> [keep reference alive].</param>
        public DelegateReference(Delegate @delegate, bool keepReferenceAlive)
        {
            Invariant.IsNotNull(@delegate, "@delegate");

            if (keepReferenceAlive)
            {
                this.@delegate = @delegate;
            }
            else
            {
                weakReference = new WeakReference(@delegate.Target);
                method = @delegate.Method;
                delegateType = @delegate.GetType();
            }
        }

        /// <summary>
        /// Gets the target.
        /// </summary>
        /// <value>The target.</value>
        public Delegate Target
        {
            [DebuggerStepThrough]
            get
            {
                return @delegate ?? TryGetDelegate();
            }
        }

        private Delegate TryGetDelegate()
        {
            if (method.IsStatic)
            {
                return Delegate.CreateDelegate(delegateType, null, method);
            }

            object target = weakReference.Target;

            return (target != null) ? Delegate.CreateDelegate(delegateType, target, method) : null;
        }
    }
}