#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Tests
{
    using Moq;
    using Xunit;

    public class ExtendedMvcApplicationBaseTests : IDisposable
    {
        private readonly Mock<IBootstrapper> bootstrapper;
        private readonly ExtendedMvcApplicationBase httpApplication;

        public ExtendedMvcApplicationBaseTests()
        {
            bootstrapper = new Mock<IBootstrapper>();
            httpApplication = new ExtendedMvcApplicationBaseTestDouble(bootstrapper.Object);
        }

        public void Dispose()
        {
            bootstrapper.VerifyAll();
        }

        [Fact]
        public void Application_start_should_execute_bootstrapper()
        {
            bootstrapper.Setup(bs => bs.Execute());

            httpApplication.Application_Start();
        }

        [Fact]
        public void Application_end_should_dispose_bootstrapper()
        {
            bootstrapper.Setup(bs => bs.Dispose());

            httpApplication.Application_End();
        }

        private sealed class ExtendedMvcApplicationBaseTestDouble : ExtendedMvcApplicationBase
        {
            private readonly IBootstrapper bootstrapper;

            public ExtendedMvcApplicationBaseTestDouble(IBootstrapper bootstrapper)
            {
                this.bootstrapper = bootstrapper;
            }

            protected override IBootstrapper CreateBootstrapper()
            {
                return bootstrapper;
            }
        }
    }
}