#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Ninject.Tests
{
    using Collections.Generic;

    using Moq;
    using Xunit;

    using IBindingMetadata = global::Ninject.Planning.Bindings.IBindingMetadata;
    using IKernel = global::Ninject.IKernel;
    using IParameter = global::Ninject.Parameters.IParameter;
    using IRequest = global::Ninject.Activation.IRequest;

    public class NinjectServiceLocatorTests
    {
        private readonly Mock<IKernel> kernel;
        private NinjectServiceLocator serviceLocator;

        public NinjectServiceLocatorTests()
        {
            kernel = new Mock<IKernel>();
            serviceLocator = new NinjectServiceLocator(kernel.Object);
        }

        [Fact]
        public void Dispose_should_also_dispose_container()
        {
            kernel.Setup(c => c.Dispose());

            serviceLocator.Dispose();

            kernel.VerifyAll();
        }

        [Fact]
        public void Should_finalize()
        {
            serviceLocator = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        [Fact]
        public void Should_be_able_to_inject()
        {
            var dummy = new DummyObject();

            kernel.Setup(c => c.Inject(It.IsAny<object>()));

            serviceLocator.Inject(dummy);

            kernel.VerifyAll();
        }

        [Fact]
        public void Should_be_able_to_get_instance_by_type()
        {
            SetupResolve();

            serviceLocator.GetInstance<DummyObject>();

            kernel.VerifyAll();
        }

        [Fact]
        public void Should_be_able_to_get_instance_by_type_and_key()
        {
            SetupResolve();

            serviceLocator.GetInstance<DummyObject>("foo");

            kernel.VerifyAll();
        }

        [Fact]
        public void Should_be_able_to_get_all_instances()
        {
            SetupResolve();

            serviceLocator.GetAllInstances(typeof(DummyObject));

            kernel.VerifyAll();
        }

        private void SetupResolve()
        {
            kernel.Setup(k => k.CreateRequest(It.IsAny<Type>(), It.IsAny<Func<IBindingMetadata, bool>>(), It.IsAny<IEnumerable<IParameter>>(), It.IsAny<bool>())).Returns(new Mock<IRequest>().Object);
            kernel.Setup(k => k.Resolve(It.IsAny<IRequest>())).Returns(new [] { new DummyObject() });
        }

        private class DummyObject
        {
        }
    }
}