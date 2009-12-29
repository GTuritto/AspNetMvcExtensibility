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

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPayload">The type of the payload.</typeparam>
    public class EventSubscription<TPayload> : IEventSubscription
    {
        private readonly IDelegateReference actionReference;
        private readonly IDelegateReference filterReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventSubscription&lt;TPayload&gt;"/> class.
        /// </summary>
        /// <param name="actionReference">The action reference.</param>
        /// <param name="filterReference">The filter reference.</param>
        public EventSubscription(IDelegateReference actionReference, IDelegateReference filterReference)
        {
            Invariant.IsNotNull(actionReference, "actionReference");
            Invariant.IsNotNull(filterReference, "filterReference");

            if (!(actionReference.Target is Action<TPayload>))
            {
                throw new ArgumentException(ExceptionMessages.InvalidDelegateReferenceType, "actionReference");
            }

            if (!(filterReference.Target is Predicate<TPayload>))
            {
                throw new ArgumentException(ExceptionMessages.InvalidDelegateReferenceType, "filterReference");
            }

            this.actionReference = actionReference;
            this.filterReference = filterReference;
        }

        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <value>The action.</value>
        public Action<TPayload> Action
        {
            [DebuggerStepThrough]
            get
            {
                return (Action<TPayload>) actionReference.Target;
            }
        }

        /// <summary>
        /// Gets the filter.
        /// </summary>
        /// <value>The filter.</value>
        public Predicate<TPayload> Filter
        {
            [DebuggerStepThrough]
            get
            {
                return (Predicate<TPayload>) filterReference.Target;
            }
        }

        /// <summary>
        /// Gets or sets the subscription token.
        /// </summary>
        /// <value>The subscription token.</value>
        public SubscriptionToken SubscriptionToken
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the execution strategy.
        /// </summary>
        /// <returns></returns>
        public virtual Action<object[]> GetExecutionStrategy()
        {
            Action<TPayload> action = Action;
            Predicate<TPayload> filter = Filter;

            if (action != null && filter != null)
            {
                return arguments =>
                                   {
                                       TPayload argument = default(TPayload);

                                       if (arguments != null && arguments.Length > 0 && arguments[0] != null)
                                       {
                                           argument = (TPayload) arguments[0];
                                       }

                                       if (filter(argument))
                                       {
                                           InvokeAction(action, argument);
                                       }
                                   };
            }

            return null;
        }

        /// <summary>
        /// Invokes the action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="argument">The argument.</param>
        [DebuggerStepThrough]
        protected virtual void InvokeAction(Action<TPayload> action, TPayload argument)
        {
            action(argument);
        }
    }
}