#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Tests
{
    using Routing;

    using Microsoft.Practices.ServiceLocation;

    using Xunit;

    public class RegisterRoutesBaseTests
    {
        [Fact]
        public void Routes_should_be_same_which_is_passed_in_constructor()
        {
            var routes = new RouteCollection();
            var registration = new RegisterRoutesBaseTestDouble(routes);

            Assert.Same(routes, registration.PublicRoutes);
        }

        private sealed class RegisterRoutesBaseTestDouble : RegisterRoutesBase
        {
            public RegisterRoutesBaseTestDouble(RouteCollection routes) : base(routes)
            {
            }

            public RouteCollection PublicRoutes
            {
                get
                {
                    return Routes;
                }
            }

            protected override void ExecuteCore(IServiceLocator locator)
            {
                throw new NotImplementedException();
            }
        }
    }
}