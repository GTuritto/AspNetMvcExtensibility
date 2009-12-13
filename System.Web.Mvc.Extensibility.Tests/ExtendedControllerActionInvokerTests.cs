namespace System.Web.Mvc.Extensibility.Tests
{
    using Moq;

    public class ExtendedControllerActionInvokerTests
    {
        private readonly Mock<FakeServiceLocator> serviceLocator;
        private readonly Mock<IFilterRegistry> filterRegistry;
        private readonly ExtendedControllerActionInvoker controllerActionInvoker;

        public ExtendedControllerActionInvokerTests()
        {
            serviceLocator = new Mock<FakeServiceLocator>();
            filterRegistry = new Mock<IFilterRegistry>();

            controllerActionInvoker = new ExtendedControllerActionInvoker(serviceLocator.Object, filterRegistry.Object);
        }
    }
}