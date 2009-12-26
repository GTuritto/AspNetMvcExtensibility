#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Tests
{
    using Xunit;

    public class EnumerableExtensionsTests
    {
        [Fact]
        public void Each_should_call_the_provided_action()
        {
            var list = new[] { 4 };
            bool isCalled = false;

            list.Each(i => isCalled = true);

            Assert.True(isCalled);
        }
    }
}