#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Tests
{
    using Xunit;

    public class BindingTypesAttributeTests
    {
        [Fact]
        public void BindableTypes_should_be_same_which_passed_in_constructor()
        {
            var attribute = new BindingTypesAttribute(new[] { typeof (string), typeof (int) });

            Assert.Contains(typeof(int), attribute.Types);
            Assert.Contains(typeof(string), attribute.Types);
            Assert.DoesNotContain(typeof(object), attribute.Types);
        }
    }
}