namespace System.Web.Mvc.Extensibility.Unity.Tests
{
    using Moq;
    using Xunit;

    using Microsoft.Practices.Unity;

    public class UnityServiceLocatorTests
    {
        private readonly Mock<IUnityContainer> container;
        private UnityServiceLocator serviceLocator;

        public UnityServiceLocatorTests()
        {
            container = new Mock<IUnityContainer>();
            serviceLocator = new UnityServiceLocator(container.Object);
        }

        [Fact]
        public void Dispose_should_also_dispose_container()
        {
            container.Setup(c => c.Dispose());

            serviceLocator.Dispose();

            container.VerifyAll();
        }

        [Fact]
        public void Should_finalize()
        {
            serviceLocator = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        [Fact]
        public void Should_be_able_to_inject()
        {
            var dummy = new DummyObject();

            container.Setup(c => c.BuildUp(It.IsAny<Type>(), It.IsAny<DummyObject>()));

            serviceLocator.Inject(dummy);

            container.VerifyAll();
        }

        [Fact]
        public void Should_be_able_to_get_instance_by_type()
        {
            container.Setup(c => c.Resolve(It.IsAny<Type>()));

            serviceLocator.GetInstance<DummyObject>();

            container.VerifyAll();
        }

        [Fact]
        public void Should_be_able_to_get_instance_by_type_and_key()
        {
            container.Setup(c => c.Resolve(It.IsAny<Type>(), It.IsAny<string>()));

            serviceLocator.GetInstance<DummyObject>("foo");

            container.VerifyAll();
        }

        [Fact]
        public void Should_be_able_to_get_all_instances()
        {
            container.Setup(c => c.ResolveAll(It.IsAny<Type>())).Returns(new DummyObject[] { });
            container.Setup(c => c.Resolve(It.IsAny<Type>())).Throws(new ResolutionFailedException(typeof(DummyObject), null, null));

            serviceLocator.GetAllInstances(typeof(DummyObject));

            container.VerifyAll();
        }

        private class DummyObject
        {
        }
    }
}