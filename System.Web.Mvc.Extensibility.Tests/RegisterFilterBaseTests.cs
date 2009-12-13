namespace System.Web.Mvc.Extensibility.Tests
{
    using Microsoft.Practices.ServiceLocation;

    using Moq;
    using Xunit;

    public class RegisterFilterBaseTests
    {
        [Fact]
        public void FilterRegistry_should_be_same_which_is_passed_in_constructor()
        {
            var registry = new Mock<IFilterRegistry>();
            var registration = new RegisterFiltersBaseTestDouble(registry.Object);

            Assert.Same(registry.Object, registration.PublicFilterRegistry);
        }

        private sealed class RegisterFiltersBaseTestDouble : RegisterFiltersBase
        {
            public RegisterFiltersBaseTestDouble(IFilterRegistry filterRegistry) : base(filterRegistry)
            {
            }

            public IFilterRegistry PublicFilterRegistry
            {
                get
                {
                    return FilterRegistry;
                }
            }

            protected override void ExecuteCore(IServiceLocator serviceLocator)
            {
                throw new NotImplementedException();
            }
        }
    }
}