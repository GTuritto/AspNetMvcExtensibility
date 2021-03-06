#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Autofac.Tests
{
    using Moq;
    using Xunit;

    using IContainer = global::Autofac.IContainer;
    using Parameter = global::Autofac.Parameter;

    public class AutofacAdapterTests
    {
        private readonly Mock<IContainer> container;
        private AutofacAdapter adapter;

        public AutofacAdapterTests()
        {
            container = new Mock<IContainer>();
            adapter = new AutofacAdapter(container.Object);
        }

        [Fact]
        public void Dispose_should_also_dispose_container()
        {
            container.Setup(c => c.Dispose());

            adapter.Dispose();

            container.VerifyAll();
        }

        [Fact]
        public void Should_finalize()
        {
            adapter = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        [Fact]
        public void Should_be_able_to_inject()
        {
            var dummy = new DummyObject();

            container.Setup(c => c.InjectProperties(It.IsAny<object>()));

            adapter.Inject(dummy);

            container.VerifyAll();
        }

        [Fact]
        public void Should_be_able_to_get_instance_by_type()
        {
            container.Setup(c => c.Resolve(It.IsAny<Type>()));

            adapter.GetInstance<DummyObject>();

            container.VerifyAll();
        }

        [Fact]
        public void Should_be_able_to_get_instance_by_type_and_key()
        {
            container.Setup(c => c.Resolve(It.IsAny<string>()));

            adapter.GetInstance<DummyObject>("foo");

            container.VerifyAll();
        }

        [Fact]
        public void Should_be_able_to_get_all_instances()
        {
            object instances;

            container.Setup(c => c.TryResolve(It.IsAny<Type>(), out instances, It.IsAny<Parameter[]>())).Returns(false);

            adapter.GetAllInstances(typeof(DummyObject));

            container.VerifyAll();
        }

        private class DummyObject
        {
        }
    }
}