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
    using Collections.Generic;
    using Linq;

    /// <summary>
    /// 
    /// </summary>
    public abstract class EventBase
    {
        private readonly List<IEventSubscription> subscriptions = new List<IEventSubscription>();
        private readonly object syncLock = new object();

        /// <summary>
        /// Unsubscribes the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        public virtual void Unsubscribe(SubscriptionToken token)
        {
            lock (syncLock)
            {
                IEventSubscription subscription = subscriptions.FirstOrDefault(evt => evt.SubscriptionToken == token);

                if (subscription != null)
                {
                    subscriptions.Remove(subscription);
                }
            }
        }

        /// <summary>
        /// Gets the subscriptions.
        /// </summary>
        /// <value>The subscriptions.</value>
        protected ICollection<IEventSubscription> Subscriptions
        {
            [DebuggerStepThrough]
            get
            {
                return subscriptions;
            }
        }

        /// <summary>
        /// Subscribes the specified event subscription.
        /// </summary>
        /// <param name="eventSubscription">The event subscription.</param>
        /// <returns></returns>
        protected virtual SubscriptionToken Subscribe(IEventSubscription eventSubscription)
        {
            eventSubscription.SubscriptionToken = new SubscriptionToken();

            lock (syncLock)
            {
                subscriptions.Add(eventSubscription);
            }

            return eventSubscription.SubscriptionToken;
        }

        /// <summary>
        /// Publishes the specified arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        protected virtual void Publish(params object[] arguments)
        {
            PruneAndReturnStrategies().Each(executionStrategy => executionStrategy(arguments));
        }

        /// <summary>
        /// Determines whether [contains] [the specified token].
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        /// <c>true</c> if [contains] [the specified token]; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool Contains(SubscriptionToken token)
        {
            lock (syncLock)
            {
                return subscriptions.Any(evt => evt.SubscriptionToken == token);
            }
        }

        private List<Action<object[]>> PruneAndReturnStrategies()
        {
            List<Action<object[]>> returnList = new List<Action<object[]>>();

            lock (syncLock)
            {
                for (int i = subscriptions.Count - 1; i >= 0; i--)
                {
                    Action<object[]> subscriptionAction = subscriptions[i].GetExecutionStrategy();

                    if (subscriptionAction == null)
                    {
                        subscriptions.RemoveAt(i);
                    }
                    else
                    {
                        returnList.Add(subscriptionAction);
                    }
                }
            }

            return returnList;
        }
    }
}