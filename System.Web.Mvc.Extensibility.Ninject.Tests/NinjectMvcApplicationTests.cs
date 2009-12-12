namespace System.Web.Mvc.Extensibility.Ninject.Tests
{
    using Xunit;

    public class NinjectMvcApplicationTests
    {
        [Fact]
        public void Should_be_able_to_create_bootstrapper()
        {
            var application = new NinjectMvcApplication();

            Assert.NotNull(application.Bootstrapper);
            Assert.IsType<NinjectBootstrapper>(application.Bootstrapper);
        }
    }
}