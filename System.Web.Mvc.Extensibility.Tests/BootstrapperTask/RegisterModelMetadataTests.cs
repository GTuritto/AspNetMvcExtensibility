#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Tests
{
    using Collections.Generic;
    using Linq;

    using Moq;
    using Xunit;

    public class RegisterModelMetadataTests
    {
        private readonly Mock<IModelMetadataRegistry> registry;
        private readonly Mock<FakeServiceLocator> serviceLocator;

        private readonly RegisterModelMetadata registration;

        public RegisterModelMetadataTests()
        {
            registry = new Mock<IModelMetadataRegistry>();
            serviceLocator = new Mock<FakeServiceLocator>();

            serviceLocator.Setup(sl => sl.GetInstance<IModelMetadataRegistry>()).Returns(registry.Object);

            registration = new RegisterModelMetadata();
        }

        [Fact]
        public void Should_be_able_to_register_model_metadata_and_validation_provider()
        {
            var configuration1 = new Mock<IModelMetadataConfiguration>();
            var configuration2 = new Mock<IModelMetadataConfiguration>();

            serviceLocator.Setup(sl => sl.GetAllInstances<IModelMetadataConfiguration>()).Returns(new[] { configuration1.Object, configuration2.Object });
            registry.Setup(r => r.Register(It.IsAny<Type>(), It.IsAny<IDictionary<string, ModelMetadataItemBase>>()));

            registration.Execute(serviceLocator.Object);

            registry.VerifyAll();

            Assert.IsType<CompositeModelMetadataProvider>(ModelMetadataProviders.Current);
            Assert.Contains(typeof(ExtendedModelValidatorProvider), ModelValidatorProviders.Providers.Select(p => p.GetType()));
        }
    }
}