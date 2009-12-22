#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Microsoft.Practices.ServiceLocation;

    public class PerRequestExecutionContext
    {
        public PerRequestExecutionContext(HttpContextBase httpContext, IServiceLocator serviceLocator)
        {
            Invariant.IsNotNull(httpContext, "httpContext");
            Invariant.IsNotNull(serviceLocator, "serviceLocator");

            HttpContext = httpContext;
            ServiceLocator = serviceLocator;
        }

        public HttpContextBase HttpContext
        {
            get;
            private set;
        }

        public IServiceLocator ServiceLocator
        {
            get;
            private set;
        }
    }
}