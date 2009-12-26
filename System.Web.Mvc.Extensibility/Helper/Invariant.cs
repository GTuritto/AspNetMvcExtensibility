#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Diagnostics;
    using Globalization;

    /// <summary>
    /// Defines a utility class to validate method arguments.
    /// </summary>
    public static class Invariant
    {
        /// <summary>
        /// Determines whether the given argument is not null.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        [DebuggerHidden]
        public static void IsNotNull(object value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName, string.Format(CultureInfo.CurrentUICulture, ExceptionMessages.CannotBeNull, parameterName));
            }
        }
    }
}