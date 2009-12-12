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

            registration.Execute(new Mock<FakeServiceLocator>().Object);

            Assert.IsType<ExtendedControllerFactory>(builder.GetControllerFactory());
        }
    }
}