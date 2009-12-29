#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.StructureMap.Tests
{
    using Moq;
    using Xunit;

    using IContainer = global::StructureMap.IContainer;

    public class StructureMapAdapterTests
    {
        private readonly Mock<IContainer> container;
        private readonly StructureMapAdapter adapter;

        public StructureMapAdapterTests()
        {
            container = new Mock<IContainer>();
            adapter = new StructureMapAdapter(container.Object);
        }

        [Fact]
        public void Should_be_able_to_inject()
        {
            var dummy = new DummyObject();

            container.Setup(c => c.BuildUp(It.IsAny<object>()));

            adapter.Inject(dummy);

            container.VerifyAll();
        }

        [Fact]
        public void Should_be_able_to_get_instance_by_type()
        {
            container.Setup(c => c.GetInstance(It.IsAny<Type>()));

            adapter.GetInstance<DummyObject>();

            container.VerifyAll();
        }

        [Fact]
        public void Should_be_able_to_get_instance_by_type_and_key()
        {
            container.Setup(c => c.GetInstance(It.IsAny<Type>(), It.IsAny<string>()));

            adapter.GetInstance<DummyObject>("foo");

            container.VerifyAll();
        }

        [Fact]
        public void Should_be_able_to_get_all_instances()
        {
            container.Setup(c => c.GetAllInstances(It.IsAny<Type>())).Returns(new [] { new DummyObject() });

            adapter.GetAllInstances(typeof(DummyObject));

            container.VerifyAll();
        }

        private class DummyObject
        {
        }
    }
}