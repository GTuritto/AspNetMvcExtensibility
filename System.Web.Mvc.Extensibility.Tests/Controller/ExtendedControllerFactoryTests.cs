#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

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

            var actionInvoker = new Mock<IActionInvoker>();
            var controller = new Mock<Controller>();

            serviceLocator.Setup(sl => sl.GetInstance(It.Is<Type>(type => typeof(Controller).IsAssignableFrom(type)))).Returns(controller.Object);
            serviceLocator.Setup(sl => sl.GetInstance<IActionInvoker>()).Returns(actionInvoker.Object);

            controllerFactory.PublicGetControllerInstance(null, controller.Object.GetType());

            Assert.Same(actionInvoker.Object, controller.Object.ActionInvoker);
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
    }
}