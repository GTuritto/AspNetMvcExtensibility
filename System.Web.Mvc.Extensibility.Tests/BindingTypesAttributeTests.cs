namespace System.Web.Mvc.Extensibility.Tests
{
    using Xunit;

    public class BindingTypesAttributeTests
    {
        [Fact]
        public void BindableTypes_should_be_same_which_passed_in_constructor()
        {
            var attribute = new BindingTypesAttribute(new[] { typeof (string), typeof (int) });

            Assert.Contains(typeof(int), attribute.Types);
            Assert.Contains(typeof(string), attribute.Types);
            Assert.DoesNotContain(typeof(object), attribute.Types);
        }
    }
}