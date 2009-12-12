namespace System.Web.Mvc.Extensibility.Tests
{
    using Xunit;

    public class EnumerableExtensionsTests
    {
        [Fact]
        public void Each_should_call_the_provided_action()
        {
            var list = new[] { 4 };
            bool isCalled = false;

            list.Each(i => isCalled = true);

            Assert.True(isCalled);
        }
    }
}