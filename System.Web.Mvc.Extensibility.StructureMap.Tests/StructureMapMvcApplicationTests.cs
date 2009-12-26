#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.StructureMap.Tests
{
    using Xunit;

    public class StructureMapMvcApplicationTests
    {
        [Fact]
        public void Should_be_able_to_create_bootstrapper()
        {
            var application = new StructureMapMvcApplication();

            Assert.NotNull(application.Bootstrapper);
            Assert.IsType<StructureMapBootstrapper>(application.Bootstrapper);
        }
    }
}