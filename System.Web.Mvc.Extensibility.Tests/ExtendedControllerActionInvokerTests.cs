namespace System.Web.Mvc.Extensibility.Tests
{
    using Routing;

    using Microsoft.Practices.ServiceLocation;

    using Moq;
    using Xunit;

    public class ExtendedControllerActionInvokerTests
    {
        private readonly Mock<FakeServiceLocator> serviceLocator;
        private readonly Mock<IFilterRegistry> filterRegistry;
        private readonly ExtendedControllerActionInvokerTestDouble controllerActionInvoker;

        public ExtendedControllerActionInvokerTests()
        {
            serviceLocator = new Mock<FakeServiceLocator>();
            filterRegistry = new Mock<IFilterRegistry>();

            controllerActionInvoker = new ExtendedControllerActionInvokerTestDouble(serviceLocator.Object, filterRegistry.Object);
        }

        [Fact]
        public void GetFilters_should_merge_filters()
        {
            var decoratedActionFilter = new Mock<IActionFilter>();
            var decoratedResultFilter = new Mock<IResultFilter>();
            var decoratedFilters = new FilterInfo();

            decoratedFilters.ActionFilters.Add(decoratedActionFilter.Object);
            decoratedFilters.ResultFilters.Add(decoratedResultFilter.Object);

            var actionDescriptor = new Mock<ActionDescriptor>();
            actionDescriptor.Setup(ad => ad.GetFilters()).Returns(decoratedFilters);

            var registeredActionFilter = new Mock<IActionFilter>();
            var registeredResultFilter = new Mock<IResultFilter>();
            var registeredFilters = new FilterInfo();

            registeredFilters.ActionFilters.Add(registeredActionFilter.Object);
            registeredFilters.ResultFilters.Add(registeredResultFilter.Object);

            filterRegistry.Setup(fr => fr.Matching(It.IsAny<ControllerContext>(), It.IsAny<ActionDescriptor>())).Returns(registeredFilters);

            var controllerContext = new ControllerContext(new RequestContext(new Mock<HttpContextBase>().Object, new RouteData()), new Mock<ControllerBase>().Object);

            var mergedFilters = controllerActionInvoker.PublicGetFilters(controllerContext, actionDescriptor.Object);

            Assert.Contains(decoratedActionFilter.Object, mergedFilters.ActionFilters);
            Assert.Contains(registeredActionFilter.Object, mergedFilters.ActionFilters);
            Assert.Contains(decoratedResultFilter.Object, mergedFilters.ResultFilters);
            Assert.Contains(registeredResultFilter.Object, mergedFilters.ResultFilters);
        }

        private sealed class ExtendedControllerActionInvokerTestDouble : ExtendedControllerActionInvoker
        {
            public ExtendedControllerActionInvokerTestDouble(IServiceLocator locator, IFilterRegistry filterRegistry) : base(locator, filterRegistry)
            {
            }

            public FilterInfo PublicGetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
            {
                return GetFilters(controllerContext, actionDescriptor);
            }
        }
    }
}