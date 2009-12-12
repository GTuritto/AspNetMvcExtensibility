namespace System.Web.Mvc.Extensibility.Tests
{
    using Microsoft.Practices.ServiceLocation;

    using Moq;
    using Xunit;

    public class BootstrapperTaskBaseTests
    {
        [Fact]
        public void Should_be_able_to_execute()
        {
            var task = new BootstrapperTaskBaseTestDouble();

            task.Execute(new Mock<IServiceLocator>().Object);

            Assert.True(task.IsExecuted);
        }

        private sealed class BootstrapperTaskBaseTestDouble : BootstrapperTaskBase
        {
            public bool IsExecuted;

            protected override void ExecuteCore(IServiceLocator locator)
            {
                IsExecuted = true;
            }
        }
    }
}