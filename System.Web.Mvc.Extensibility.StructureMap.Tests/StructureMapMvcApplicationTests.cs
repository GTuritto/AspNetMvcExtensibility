namespace System.Web.Mvc.Extensibility.StructureMap.Tests
{
    using Xunit;

    public class StructureMapMvcApplicationTests
    {
        [Fact]
        public void Should_be_able_to_create_bootstrapper()
        {
            var application = new StructureMapMvcApplication();

            Assert.NotNull(application.Bootstrapper);
            Assert.IsType<StructureMapBootstrapper>(application.Bootstrapper);
        }
    }
}