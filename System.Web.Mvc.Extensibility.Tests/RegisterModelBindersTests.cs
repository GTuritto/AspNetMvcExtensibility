namespace System.Web.Mvc.Extensibility.Tests
{
    using Moq;
    using Xunit;

    public class RegisterModelBindersTests
    {
        [Fact]
        public void Should_be_able_to_register_controller_factory()
        {
            var modelBinders = new ModelBinderDictionary();
            var registration = new RegisterModelBinders(modelBinders);

            var modelBinder = new FakeModelBinder();

            var serviceLocator = new Mock<FakeServiceLocator>();
            serviceLocator.Setup(sl => sl.GetAllInstances<IModelBinder>()).Returns(new[] { modelBinder });

            registration.Execute(serviceLocator.Object);

            Assert.Same(modelBinders[typeof(object)], modelBinder);
        }

        [BindingTypes(typeof(object))]
        private sealed class FakeModelBinder : IModelBinder
        {
            public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
                throw new NotImplementedException();
            }
        }
    }
}