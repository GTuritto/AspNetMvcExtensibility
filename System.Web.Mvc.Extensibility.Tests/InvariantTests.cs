namespace System.Web.Mvc.Extensibility.Tests
{
    using Xunit;

    public class InvariantTests
    {
        [Fact]
        public void IsNotNull_should_throw_exception_when_passing_null_value()
        {
            Assert.Throws<ArgumentNullException>(() => Invariant.IsNotNull(null, "x"));
        }

        [Fact]
        public void IsNotNull_should_not_throw_exception_when_passing_non_null_value()
        {
            Assert.DoesNotThrow(() => Invariant.IsNotNull(new object(), "x"));
        }
    }
}