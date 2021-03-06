#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Tests
{
    using Moq;
    using Xunit;

    public class FilterRegistryActionItemTests
    {
        [Fact]
        public void Should_throw_exception_when_incorrect_expression_is_passed()
        {
            Assert.Throws<ArgumentException>(() => new FilterRegistryActionItem<FakeController>(c => c.HttpContext.ClearError(), new Func<FilterAttribute>[0]));
        }

        [Fact]
        public void IsMatching_should_return_true_for_same_action()
        {
            var actionItem = new FilterRegistryActionItem<FakeController>(c => c.Index(), new Func<FilterAttribute>[0]);

            var controllerContext = new ControllerContext
                                        {
                                            Controller = new FakeController()
                                        };

            var controllerDescriptor = new Mock<ControllerDescriptor>();
            controllerDescriptor.SetupGet(cd => cd.ControllerName).Returns("Fake");

            var actionDescriptor = new Mock<ActionDescriptor>();
            actionDescriptor.SetupGet(ad => ad.ControllerDescriptor).Returns(controllerDescriptor.Object);
            actionDescriptor.SetupGet(ad => ad.ActionName).Returns("Index");

            Assert.True(actionItem.IsMatching(controllerContext, actionDescriptor.Object));
        }

        [Fact]
        public void IsMatching_should_return_true_for_parameterized_actions()
        {
            var actionItem = new FilterRegistryActionItem<FakeController>(c => c.Edit(0, null), new Func<FilterAttribute>[0]);

            var controllerContext = new ControllerContext
                                        {
                                            Controller = new FakeController()
                                        };

            var controllerDescriptor = new Mock<ControllerDescriptor>();
            controllerDescriptor.SetupGet(cd => cd.ControllerName).Returns("Fake");

            var intergerParameterDescriptor = new Mock<ParameterDescriptor>();
            intergerParameterDescriptor.SetupGet(pd => pd.ParameterType).Returns(typeof(int));

            var formsParameterDescriptor = new Mock<ParameterDescriptor>();
            formsParameterDescriptor.SetupGet(pd => pd.ParameterType).Returns(typeof(FormCollection));

            var actionDescriptor = new Mock<ActionDescriptor>();
            actionDescriptor.SetupGet(ad => ad.ControllerDescriptor).Returns(controllerDescriptor.Object);
            actionDescriptor.SetupGet(ad => ad.ActionName).Returns("Edit");
            actionDescriptor.Setup(ad => ad.GetParameters()).Returns(new[] { intergerParameterDescriptor.Object, formsParameterDescriptor.Object });

            Assert.True(actionItem.IsMatching(controllerContext, actionDescriptor.Object));
        }

        [Fact]
        public void IsMatching_should_return_false_for_other_action()
        {
            var actionItem = new FilterRegistryActionItem<FakeController>(c => c.Index(), new Func<FilterAttribute>[0]);

            var controllerContext = new ControllerContext
                                        {
                                            Controller = new FakeController()
                                        };

            var controllerDescriptor = new Mock<ControllerDescriptor>();
            controllerDescriptor.SetupGet(cd => cd.ControllerName).Returns("Fake");

            var actionDescriptor = new Mock<ActionDescriptor>();
            actionDescriptor.SetupGet(ad => ad.ControllerDescriptor).Returns(controllerDescriptor.Object);
            actionDescriptor.SetupGet(ad => ad.ActionName).Returns("About");

            Assert.False(actionItem.IsMatching(controllerContext, actionDescriptor.Object));
        }

        [Fact]
        public void IsMatching_should_return_false_for_same_action_when_parameter_differs()
        {
            var actionItem = new FilterRegistryActionItem<FakeController>(c => c.Edit(0), new Func<FilterAttribute>[0]);

            var controllerContext = new ControllerContext
                                        {
                                            Controller = new FakeController()
                                        };

            var controllerDescriptor = new Mock<ControllerDescriptor>();
            controllerDescriptor.SetupGet(cd => cd.ControllerName).Returns("Fake");

            var parameterDescriptor = new Mock<ParameterDescriptor>();
            parameterDescriptor.SetupGet(pd => pd.ParameterType).Returns(typeof(FormCollection));

            var actionDescriptor = new Mock<ActionDescriptor>();
            actionDescriptor.SetupGet(ad => ad.ControllerDescriptor).Returns(controllerDescriptor.Object);
            actionDescriptor.SetupGet(ad => ad.ActionName).Returns("Edit");
            actionDescriptor.Setup(ad => ad.GetParameters()).Returns(new[] { parameterDescriptor.Object });

            Assert.False(actionItem.IsMatching(controllerContext, actionDescriptor.Object));
        }

        private sealed class FakeController : Controller
        {
            public ActionResult Index()
            {
                return View();
            }

            // ReSharper disable UnusedMember.Local
            public ActionResult About()
            // ReSharper restore UnusedMember.Local
            {
                return View();
            }

            // ReSharper disable UnusedMember.Local
            // ReSharper disable UnusedParameter.Local
            public ActionResult Edit(int id)
            // ReSharper restore UnusedParameter.Local
            // ReSharper restore UnusedMember.Local
            {
                return View();
            }

            [HttpPost]
            // ReSharper disable UnusedMember.Local
            // ReSharper disable UnusedParameter.Local
            public ActionResult Edit(int id, FormCollection forms)
            // ReSharper restore UnusedParameter.Local
            // ReSharper restore UnusedMember.Local
            {
                return View();
            }
        }
    }
}