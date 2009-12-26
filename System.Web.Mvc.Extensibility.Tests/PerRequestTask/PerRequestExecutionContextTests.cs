#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Tests
{
    using Moq;
    using Xunit;

    using Microsoft.Practices.ServiceLocation;

    public class PerRequestExecutionContextTests
    {
        private readonly Mock<HttpContextBase> httpContext;
        private readonly Mock<IServiceLocator> serviceLocator;

        private readonly PerRequestExecutionContext executionContext;

        public PerRequestExecutionContextTests()
        {
            httpContext = new Mock<HttpContextBase>();
            serviceLocator = new Mock<IServiceLocator>();

            executionContext = new PerRequestExecutionContext(httpContext.Object, serviceLocator.Object);
        }

        [Fact]
        public void HttpContext_should_be_same_which_is_passed_in_constructor()
        {
            Assert.Same(httpContext.Object, executionContext.HttpContext);
        }

        [Fact]
        public void ServiceLocator_should_be_same_which_is_passed_in_constructor()
        {
            Assert.Same(serviceLocator.Object, executionContext.ServiceLocator);
        }
    }
}