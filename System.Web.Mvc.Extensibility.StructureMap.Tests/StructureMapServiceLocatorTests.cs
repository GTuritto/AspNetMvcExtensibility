namespace System.Web.Mvc.Extensibility.StructureMap.Tests
{
    using Moq;
    using Xunit;

    using IContainer = global::StructureMap.IContainer;

    public class StructureMapServiceLocatorTests
    {
        private readonly Mock<IContainer> container;
        private readonly StructureMapServiceLocator serviceLocator;

        public StructureMapServiceLocatorTests()
        {
            container = new Mock<IContainer>();
            serviceLocator = new StructureMapServiceLocator(container.Object);
        }

        [Fact]
        public void Should_be_able_to_inject()
        {
            var dummy = new DummyObject();

            container.Setup(c => c.BuildUp(It.IsAny<object>()));

            serviceLocator.Inject(dummy);

            container.VerifyAll();
        }

        [Fact]
        public void Should_be_able_to_get_instance_by_type()
        {
            container.Setup(c => c.GetInstance(It.IsAny<Type>()));

            serviceLocator.GetInstance<DummyObject>();

            container.VerifyAll();
        }

        [Fact]
        public void Should_be_able_to_get_instance_by_type_and_key()
        {
            container.Setup(c => c.GetInstance(It.IsAny<Type>(), It.IsAny<string>()));

            serviceLocator.GetInstance<DummyObject>("foo");

            container.VerifyAll();
        }

        [Fact]
        public void Should_be_able_to_get_all_instances()
        {
            container.Setup(c => c.GetAllInstances(It.IsAny<Type>())).Returns(new [] { new DummyObject() });

            serviceLocator.GetAllInstances(typeof(DummyObject));

            container.VerifyAll();
        }

        private class DummyObject
        {
        }
    }
}