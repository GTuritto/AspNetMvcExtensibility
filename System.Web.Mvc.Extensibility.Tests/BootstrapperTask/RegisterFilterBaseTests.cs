#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

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