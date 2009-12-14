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

    public class RegisterViewEnginesTests
    {
        private readonly Mock<IViewEngine> viewEngine;
        private readonly Mock<FakeServiceLocator> serviceLocator;
        private readonly ViewEngineCollection viewEngines;
        private readonly RegisterViewEngines registration;

        public RegisterViewEnginesTests()
        {
            viewEngine = new Mock<IViewEngine>();
            serviceLocator = new Mock<FakeServiceLocator>();

            viewEngines = new ViewEngineCollection();
            registration = new RegisterViewEngines(viewEngines);
        }

        [Fact]
        public void Should_be_able_to_register_view_engines()
        {
            serviceLocator.Setup(sl => sl.GetAllInstances<IViewEngine>()).Returns(new[] { viewEngine.Object });

            registration.Execute(serviceLocator.Object);

            Assert.Contains(viewEngine.Object, viewEngines);
        }

        [Fact]
        public void Should_not_be_able_to_register_same_view_engine_more_than_once()
        {
            serviceLocator.Setup(sl => sl.GetAllInstances<IViewEngine>()).Returns(new[] { viewEngine.Object, viewEngine.Object });

            registration.Execute(serviceLocator.Object);

            Assert.Equal(1, viewEngines.Count);
        }
    }
}