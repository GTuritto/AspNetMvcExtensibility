#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using System;
    using Collections.Generic;
    using Linq;
    using Threading;

    /// <summary>
    /// 
    /// </summary>
    public class EventAggregator : DisposableBase, IEventAggregator
    {
        private readonly List<EventBase> events = new List<EventBase>();
        private readonly ReaderWriterLockSlim syncLock = new ReaderWriterLockSlim();

        /// <summary>
        /// Gets the event.
        /// </summary>
        /// <typeparam name="TEventType">The type of the event type.</typeparam>
        /// <returns></returns>
        public TEventType GetEvent<TEventType>() where TEventType : EventBase
        {
            syncLock.EnterUpgradeableReadLock();

            try
            {
                TEventType eventInstance = events.SingleOrDefault(evt => evt.GetType() == typeof(TEventType)) as TEventType;

                if (eventInstance == null)
                {
                    syncLock.EnterWriteLock();

                    try
                    {
                        eventInstance = events.SingleOrDefault(evt => evt.GetType() == typeof(TEventType)) as TEventType;

                        if (eventInstance == null)
                        {
                            eventInstance = Activator.CreateInstance<TEventType>();
                            events.Add(eventInstance);
                        }
                    }
                    finally
                    {
                        syncLock.ExitWriteLock();
                    }
                }

                return eventInstance;
            }
            finally
            {
                syncLock.ExitUpgradeableReadLock();
            }
        }

        /// <summary>
        /// Disposes the resources.
        /// </summary>
        protected override void DisposeCore()
        {
            syncLock.Dispose();
        }
    }
}