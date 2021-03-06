#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Tests
{
    using Xunit;

    public class BuildManagerWrapperTests
    {
        [Fact]
        public void Current_should_not_be_null()
        {
            Assert.NotNull(BuildManagerWrapper.Current);
        }

        [Fact]
        public void Assemblies_should_throw_exception_when_not_running_in_web_server()
        {
            Assert.Throws<HttpException>(() => Assert.Empty(BuildManagerWrapper.Current.Assemblies));
        }

        [Fact]
        public void ConcreteTypes_should_throw_exception_when_not_running_in_web_server()
        {
            Assert.Throws<HttpException>(() => Assert.Empty(BuildManagerWrapper.Current.ConcreteTypes));
        }
    }
}