namespace System.Web.Mvc.Extensibility.Unity.Tests
{
    using Xunit;

    public class UnityMvcApplicationTests
    {
        [Fact]
        public void Should_be_able_to_create_bootstrapper()
        {
            var application = new UnityMvcApplicationTestDouble();

            Assert.NotNull(application.PublicBootstrapper);
            Assert.IsType<UnityBootstrapper>(application.PublicBootstrapper);
        }

        private sealed class UnityMvcApplicationTestDouble : UnityMvcApplication
        {
            public IBootstrapper PublicBootstrapper
            {
                get
                {
                    return Bootstrapper;
                }
            }
        }
    }
}