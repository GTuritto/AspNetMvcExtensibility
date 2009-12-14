#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using Diagnostics;

    public static class EnumerableExtensions
    {
        [DebuggerHidden]
        public static void Each<T>(this IEnumerable<T> instance, Action<T> action)
        {
            if (instance != null)
            {
                foreach (T item in instance)
                {
                    action(item);
                }
            }
        }
    }
}