#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Autofac.Tests
{
    using Collections.Generic;
    using Reflection;

    using Microsoft.Practices.ServiceLocation;

    using Xunit;

    public class AutofacBootstrapperTests
    {
        [Fact]
        public void Should_be_able_to_create_service_locator()
        {
            var serviceLocator = new AutofacBootstrapperTestDouble().PublicCreateServiceLocator();

            Assert.NotNull(serviceLocator);
            Assert.IsType<AutofacServiceLocator>(serviceLocator);
        }

        private class AutofacBootstrapperTestDouble : AutofacBootstrapper
        {
            protected override IEnumerable<Assembly> ReferencedAssemblies
            {
                get
                {
                    yield return GetType().Assembly;
                }
            }

            public IServiceLocator PublicCreateServiceLocator()
            {
                return CreateServiceLocator();
            }
        }
    }
}