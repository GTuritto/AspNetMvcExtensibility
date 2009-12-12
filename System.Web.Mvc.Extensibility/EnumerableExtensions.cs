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