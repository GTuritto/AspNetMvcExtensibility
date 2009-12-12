namespace System.Web.Mvc.Extensibility.Tests
{
    using Routing;

    using Microsoft.Practices.ServiceLocation;

    using Moq;
    using Xunit;

    public class ExtendedControllerFactoryTests
    {
        [Fact]
        public void Should_be_able_to_create_controller()
        {
            var serviceLocator = new Mock<FakeServiceLocator>();
            var controllerFactory = new ExtendedControllerFactoryTestDouble(serviceLocator.Object);

            var controller = new FakeController();

            serviceLocator.Setup(sl => sl.GetInstance(typeof(FakeController))).Returns(controller);

            controllerFactory.PublicGetControllerInstance(null, typeof(FakeController));

            Assert.IsType<ExtendedControllerActionInvoker>(controller.ActionInvoker);
        }

        private sealed class ExtendedControllerFactoryTestDouble : ExtendedControllerFactory
        {
            public ExtendedControllerFactoryTestDouble(IServiceLocator locator) : base(locator)
            {
            }

            public void PublicGetControllerInstance(RequestContext requestContext, Type controllerType)
            {
                GetControllerInstance(requestContext, controllerType);
            }
        }

        private sealed class FakeController : Controller
        {
        }
    }
}