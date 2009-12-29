#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public interface IEventSubscription
    {
        /// <summary>
        /// Gets or sets the subscription token.
        /// </summary>
        /// <value>The subscription token.</value>
        SubscriptionToken SubscriptionToken
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the execution strategy.
        /// </summary>
        /// <returns></returns>
        Action<object[]> GetExecutionStrategy();
    }
}