#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Ninject.Tests
{
    using Collections.Generic;
    using Reflection;

    using Microsoft.Practices.ServiceLocation;

    using Xunit;

    public class NinjectBootstrapperTests
    {
        [Fact]
        public void Should_be_able_to_create_service_locator()
        {
            var serviceLocator = new NinjectBootstrapperTestDouble().PublicCreateServiceLocator();

            Assert.NotNull(serviceLocator);
            Assert.IsType<NinjectServiceLocator>(serviceLocator);
        }

        private class NinjectBootstrapperTestDouble : NinjectBootstrapper
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