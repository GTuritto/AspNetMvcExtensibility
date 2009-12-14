#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Tests
{
    using Collections.Generic;
    using Reflection;

    using Xunit;

    public class TypeExtensionsTests
    {
        [Fact]
        public void ConceretTypes_should_not_be_empty_for_valid_assemblies()
        {
            Assert.NotEmpty(new[] { GetType().Assembly }.ConcreteTypes());
        }

        [Fact]
        public void ConceretTypes_should_be_empty_for_null_assemblies()
        {
            Assert.Empty(((IEnumerable<Assembly>) null).ConcreteTypes());
        }

        [Fact]
        public void HasDefaultConstructor_should_return_true_for_default_constructor_type()
        {
            Assert.True(typeof(TypeExtensionsTests).HasDefaultConstructor());
        }

        [Fact]
        public void HasDefaultConstructor_should_return_false_for_non_default_constructor_type()
        {
            Assert.False(typeof(ObjectWithArgument).HasDefaultConstructor());
        }

        private sealed class ObjectWithArgument
        {
            // ReSharper disable UnusedParameter.Local
            public ObjectWithArgument(object argumet)
            // ReSharper restore UnusedParameter.Local
            {
            }
        }
    }
}