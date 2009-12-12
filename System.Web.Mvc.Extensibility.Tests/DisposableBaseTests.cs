namespace System.Web.Mvc.Extensibility.Tests
{
    using System;

    using Xunit;

    public class DisposableBaseTests
    {
        private DisposableObjectBaseTestDouble disposable;

        public DisposableBaseTests()
        {
            disposable = new DisposableObjectBaseTestDouble();
        }

        [Fact]
        public void Should_be_able_to_dispose()
        {
            Assert.DoesNotThrow(() => disposable.Dispose());
        }

        [Fact]
        public void Should_finalize()
        {
            disposable = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private sealed class DisposableObjectBaseTestDouble : DisposableBase
        {
        }
    }
}