#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Unity.Tests
{
    using Collections.Generic;
    using Reflection;

    using Microsoft.Practices.ServiceLocation;

    using Xunit;

    public class UnityBootstrapperTests
    {
        [Fact]
        public void Should_be_able_to_create_service_locator()
        {
            var serviceLocator = new UnityBootstrapperTestDouble().PublicCreateServiceLocator();

            Assert.NotNull(serviceLocator);
            Assert.IsType<UnityServiceLocator>(serviceLocator);
        }

        private class UnityBootstrapperTestDouble : UnityBootstrapper
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