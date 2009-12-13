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