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

    public class RegisterModelBindersTests
    {
        private readonly ModelBinderDictionary modelBinders = new ModelBinderDictionary();
        private readonly RegisterModelBinders registration;
        private readonly IModelBinder modelBinder;
        private readonly Mock<FakeServiceLocator> serviceLocator;

        public RegisterModelBindersTests()
        {
            modelBinders = new ModelBinderDictionary();
            modelBinder = new FakeModelBinder();
            serviceLocator = new Mock<FakeServiceLocator>();

            registration = new RegisterModelBinders(modelBinders);
        }

        [Fact]
        public void Should_be_able_to_register_model_binders()
        {
            serviceLocator.Setup(sl => sl.GetAllInstances<IModelBinder>()).Returns(new[] { modelBinder });

            registration.Execute(serviceLocator.Object);

            Assert.Same(modelBinders[typeof(object)], modelBinder);
        }

        [Fact]
        public void Should_not_be_able_to_register_model_binder_for_the_same_type_more_than_once()
        {
            serviceLocator.Setup(sl => sl.GetAllInstances<IModelBinder>()).Returns(new[] { modelBinder, modelBinder });

            registration.Execute(serviceLocator.Object);

            Assert.Equal(1, modelBinders.Count);
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