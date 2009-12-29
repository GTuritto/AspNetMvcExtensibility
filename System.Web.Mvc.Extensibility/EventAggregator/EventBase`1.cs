#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using System;
    using Linq;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPayload">The type of the payload.</typeparam>
    public abstract class EventBase<TPayload> : EventBase
    {
        /// <summary>
        /// Subscribes the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public virtual SubscriptionToken Subscribe(Action<TPayload> action)
        {
            return Subscribe(action, false);
        }

        /// <summary>
        /// Subscribes the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="keepSubscriberReferenceAlive">if set to <c>true</c> [keep subscriber reference alive].</param>
        /// <returns></returns>
        public virtual SubscriptionToken Subscribe(Action<TPayload> action, bool keepSubscriberReferenceAlive)
        {
            return Subscribe(action, keepSubscriberReferenceAlive, payload => true);
        }

        /// <summary>
        /// Subscribes the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="keepSubscriberReferenceAlive">if set to <c>true</c> [keep subscriber reference alive].</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public virtual SubscriptionToken Subscribe(Action<TPayload> action, bool keepSubscriberReferenceAlive, Predicate<TPayload> filter)
        {
            IDelegateReference actionReference = new DelegateReference(action, keepSubscriberReferenceAlive);
            IDelegateReference filterReference = new DelegateReference(filter, keepSubscriberReferenceAlive);

            EventSubscription<TPayload> subscription = new EventSubscription<TPayload>(actionReference, filterReference);

            return base.Subscribe(subscription);
        }

        /// <summary>
        /// Publishes the specified payload.
        /// </summary>
        /// <param name="payload">The payload.</param>
        public virtual void Publish(TPayload payload)
        {
            base.Publish(payload);
        }

        /// <summary>
        /// Unsubscribes the specified subscriber.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        public virtual void Unsubscribe(Action<TPayload> subscriber)
        {
            lock (Subscriptions)
            {
                IEventSubscription eventSubscription = Subscriptions.Cast<EventSubscription<TPayload>>()
                                                                    .FirstOrDefault(evt => evt.Action == subscriber);

                if (eventSubscription != null)
                {
                    Subscriptions.Remove(eventSubscription);
                }
            }
        }

        /// <summary>
        /// Determines whether [contains] [the specified subscriber].
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        /// <returns>
        /// <c>true</c> if [contains] [the specified subscriber]; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool Contains(Action<TPayload> subscriber)
        {
            lock (Subscriptions)
            {
                return Subscriptions.Cast<EventSubscription<TPayload>>()
                                    .Any(evt => evt.Action == subscriber);
            }
        }
    }
}