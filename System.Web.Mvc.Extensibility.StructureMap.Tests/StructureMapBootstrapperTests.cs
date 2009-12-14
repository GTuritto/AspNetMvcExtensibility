#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.StructureMap.Tests
{
    using Collections.Generic;
    using Reflection;

    using Microsoft.Practices.ServiceLocation;

    using Xunit;

    public class StructureMapBootstrapperTests
    {
        [Fact]
        public void Should_be_able_to_create_service_locator()
        {
            var serviceLocator = new StructureMapBootstrapperTestDouble().PublicCreateServiceLocator();

            Assert.NotNull(serviceLocator);
            Assert.IsType<StructureMapServiceLocator>(serviceLocator);
        }

        private class StructureMapBootstrapperTestDouble : StructureMapBootstrapper
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