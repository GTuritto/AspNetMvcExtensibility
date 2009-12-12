namespace System.Web.Mvc.Extensibility.Tests
{
    using Moq;
    using Xunit;

    public class RegisterControllerFactoryTests
    {
        [Fact]
        public void Should_be_able_to_register_controller_factory()
        {
            var builder = new ControllerBuilder();
            var registration = new RegisterControllerFactory(builder);

            var serviceLocator = new Mock<FakeServiceLocator>();
            var controllerFactory = new Mock<IControllerFactory>();

            serviceLocator.Setup(sl => sl.GetInstance<IControllerFactory>()).Returns(controllerFactory.Object);

            registration.Execute(serviceLocator.Object);

            Assert.Same(controllerFactory.Object, builder.GetControllerFactory());
        }
    }
}