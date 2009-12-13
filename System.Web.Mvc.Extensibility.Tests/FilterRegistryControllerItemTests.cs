namespace System.Web.Mvc.Extensibility.Tests
{
    using Moq;
    using Xunit;

    public class FilterRegistryControllerItemTests
    {
        private readonly FilterRegistryControllerItem<FakeController> controllerItem;

        public FilterRegistryControllerItemTests()
        {
            controllerItem = new FilterRegistryControllerItem<FakeController>(new FilterAttribute[0]);
        }

        [Fact]
        public void IsMatching_should_return_true_for_same_controller()
        {
            var controllerContext = new ControllerContext
                                        {
                                            Controller = new FakeController()
                                        };

            Assert.True(controllerItem.IsMatching(controllerContext, new Mock<ActionDescriptor>().Object));
        }

        private sealed class FakeController : Controller
        {
        }
    }
}