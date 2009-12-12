namespace System.Web.Mvc.Extensibility.Tests
{
    using Collections.Generic;
    using Microsoft.Practices.ServiceLocation;

    using Moq;
    using Xunit;

    public class ExtendedControllerActionInvokerTests
    {
        private readonly Mock<FakeServiceLocator> serviceLocator;
        private readonly ExtendedControllerActionInvokerTestDouble controllerActionInvoker;

        public ExtendedControllerActionInvokerTests()
        {
            serviceLocator = new Mock<FakeServiceLocator>();
            controllerActionInvoker = new ExtendedControllerActionInvokerTestDouble(serviceLocator.Object);
        }

        [Fact]
        public void InvokeAuthorizationFilters_should_inject_dependency_in_filters()
        {
            serviceLocator.Setup(c => c.Inject(It.IsAny<IAuthorizationFilter>()));

            controllerActionInvoker.PublicInvokeAuthorizationFilters(new ControllerContext(), new List<IAuthorizationFilter> { new Mock<IAuthorizationFilter>().Object }, new Mock<ActionDescriptor>().Object);

            serviceLocator.VerifyAll();
        }

        [Fact]
        public void InvokeActionMethodWithFilters_should_inject_dependency_in_filters()
        {
            serviceLocator.Setup(c => c.Inject(It.IsAny<IActionFilter>()));

            controllerActionInvoker.PublicInvokeActionMethodWithFilters(new ControllerContext(), new List<IActionFilter> { new Mock<IActionFilter>().Object }, new Mock<ActionDescriptor>().Object, new Dictionary<string, object>());

            serviceLocator.VerifyAll();
        }

        [Fact]
        public void InvokeActionResultWithFilters_should_inject_dependency_in_filters()
        {
            serviceLocator.Setup(c => c.Inject(It.IsAny<IResultFilter>()));

            controllerActionInvoker.PublicInvokeActionResultWithFilters(new ControllerContext(), new List<IResultFilter> { new Mock<IResultFilter>().Object }, new Mock<ActionResult>().Object);

            serviceLocator.VerifyAll();
        }

        [Fact]
        public void InvokeExceptionFilters_should_inject_dependency_in_filters()
        {
            serviceLocator.Setup(c => c.Inject(It.IsAny<IExceptionFilter>()));

            controllerActionInvoker.PublicInvokeExceptionFilters(new ControllerContext(), new List<IExceptionFilter> { new Mock<IExceptionFilter>().Object }, new Exception());

            serviceLocator.VerifyAll();
        }

        [Fact]
        public void Should_not_be_able_to_inject_dependencies_in_filter_more_than_once()
        {
            var filter = new Mock<IActionFilter>();

            serviceLocator.Setup(c => c.Inject(It.IsAny<IActionFilter>())).Verifiable();

            controllerActionInvoker.PublicInvokeActionMethodWithFilters(new ControllerContext(), new List<IActionFilter> { filter.Object }, new Mock<ActionDescriptor>().Object, new Dictionary<string, object>());
            controllerActionInvoker.PublicInvokeActionMethodWithFilters(new ControllerContext(), new List<IActionFilter> { filter.Object }, new Mock<ActionDescriptor>().Object, new Dictionary<string, object>());

            serviceLocator.Verify(c => c.Inject(It.IsAny<IActionFilter>()), Times.AtMostOnce());
        }

        private sealed class ExtendedControllerActionInvokerTestDouble : ExtendedControllerActionInvoker
        {
            public ExtendedControllerActionInvokerTestDouble(IServiceLocator locator) : base(locator)
            {
            }

            public void PublicInvokeAuthorizationFilters(ControllerContext controllerContext, IList<IAuthorizationFilter> filters, ActionDescriptor actionDescriptor)
            {
                InvokeAuthorizationFilters(controllerContext, filters, actionDescriptor);
            }

            public void PublicInvokeActionMethodWithFilters(ControllerContext controllerContext, IList<IActionFilter> filters, ActionDescriptor actionDescriptor, IDictionary<string, object> parameters)
            {
                InvokeActionMethodWithFilters(controllerContext, filters, actionDescriptor, parameters);
            }

            public void PublicInvokeActionResultWithFilters(ControllerContext controllerContext, IList<IResultFilter> filters, ActionResult actionResult)
            {
                InvokeActionResultWithFilters(controllerContext, filters, actionResult);
            }

            public void PublicInvokeExceptionFilters(ControllerContext controllerContext, IList<IExceptionFilter> filters, Exception exception)
            {
                InvokeExceptionFilters(controllerContext, filters, exception);
            }
        }
    }
}