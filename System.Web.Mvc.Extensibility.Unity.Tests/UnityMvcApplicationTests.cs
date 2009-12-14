#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

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