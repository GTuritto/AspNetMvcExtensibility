#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

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