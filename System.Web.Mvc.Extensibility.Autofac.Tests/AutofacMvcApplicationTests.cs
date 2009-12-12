namespace System.Web.Mvc.Extensibility.Autofac.Tests
{
    using Xunit;

    public class AutofacMvcApplicationTests
    {
        [Fact]
        public void Should_be_able_to_create_bootstrapper()
        {
            var application = new AutofacMvcApplication();

            Assert.NotNull(application.Bootstrapper);
            Assert.IsType<AutofacBootstrapper>(application.Bootstrapper);
        }
    }
}