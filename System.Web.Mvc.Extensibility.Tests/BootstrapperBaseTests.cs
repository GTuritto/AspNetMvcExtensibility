#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Tests
{
    using Microsoft.Practices.ServiceLocation;

    using Moq;
    using Xunit;

    public class BootstrapperBaseTests
    {
        private readonly Mock<FakeServiceLocator> serviceLocator;
        private readonly BootstrapperBase bootstrapper;

        public BootstrapperBaseTests()
        {
            serviceLocator = new Mock<FakeServiceLocator>();
            bootstrapper = new BootstrapperBaseTestDouble(serviceLocator);
        }

        [Fact]
        public void Should_be_able_to_execute()
        {
            var task = new Mock<IBootstrapperTask>();
            task.Setup(t => t.Execute(serviceLocator.Object)).Verifiable();

            serviceLocator.Setup(sl => sl.GetAllInstances<IBootstrapperTask>()).Returns(new[] { task.Object }).Verifiable();

            bootstrapper.Execute();

            task.VerifyAll();
            serviceLocator.VerifyAll();
        }

        [Fact]
        public void Should_be_able_to_dispose()
        {
            var task = new Mock<IBootstrapperTask>();
            task.Setup(t => t.Dispose()).Verifiable();

            serviceLocator.Setup(sl => sl.GetAllInstances<IBootstrapperTask>()).Returns(new[] { task.Object }).Verifiable();

            Assert.NotNull(bootstrapper.ServiceLocator);

            bootstrapper.Dispose();

            task.VerifyAll();
            serviceLocator.VerifyAll();
        }

        [Fact]
        public void ServiceLocator_should_be_set()
        {
            Assert.Same(serviceLocator.Object, bootstrapper.ServiceLocator);
            Assert.Same(serviceLocator.Object, ServiceLocator.Current);
        }

        private sealed class BootstrapperBaseTestDouble : BootstrapperBase
        {
            private readonly Mock<FakeServiceLocator> serviceLocator;

            public BootstrapperBaseTestDouble(Mock<FakeServiceLocator> serviceLocator) : base(new Mock<IBuildManager>().Object)
            {
                this.serviceLocator = serviceLocator;
            }

            protected override IServiceLocator CreateServiceLocator()
            {
                return serviceLocator.Object;
            }
        }
    }
}