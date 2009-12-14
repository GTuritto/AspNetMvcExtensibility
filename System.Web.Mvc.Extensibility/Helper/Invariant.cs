#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Diagnostics;
    using Globalization;

    public static class Invariant
    {
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