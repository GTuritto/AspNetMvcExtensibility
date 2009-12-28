#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Routing;

    /// <summary>
    /// Defines an static class which contains extension methods of <see cref="AreaRegistrationContext"/>.
    /// </summary>
    public static class AreaRegistrationContextExtensions
    {
        /// <summary>
        /// Unmaps the give route which was registered previously.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="routeName">Name of the route.</param>
        /// <returns></returns>
        public static AreaRegistrationContext UnmapRoute(this AreaRegistrationContext instance, string routeName)
        {
            Invariant.IsNotNull(instance, "instance");
            Invariant.IsNotNull(routeName, "routeName");

            RouteBase route = instance.Routes[routeName];
            
            if (route != null)
            {
                instance.Routes.Remove(route);
            }

            return instance;
        }
    }
}