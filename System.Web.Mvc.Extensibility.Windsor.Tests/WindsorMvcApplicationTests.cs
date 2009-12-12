namespace System.Web.Mvc.Extensibility.Windsor.Tests
{
    using Xunit;

    public class WindsorMvcApplicationTests
    {
        [Fact]
        public void Should_be_able_to_create_bootstrapper()
        {
            var application = new WindsorMvcApplication();

            Assert.NotNull(application.Bootstrapper);
            Assert.IsType<WindsorBootstrapper>(application.Bootstrapper);
        }
    }
}