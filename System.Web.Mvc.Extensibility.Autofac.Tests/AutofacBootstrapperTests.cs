#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Autofac.Tests
{
    using Moq;
    using Xunit;

    public class AutofacBootstrapperTests
    {
        [Fact]
        public void Should_be_able_to_create_service_locator()
        {
            var buildManager = new Mock<IBuildManager>();
            buildManager.SetupGet(bm => bm.Assemblies).Returns(new[] { GetType().Assembly });

            var bootstrapper = new AutofacBootstrapper(buildManager.Object);

            Assert.IsType<AutofacAdapter>(bootstrapper.ServiceLocator);
        }
    }
}