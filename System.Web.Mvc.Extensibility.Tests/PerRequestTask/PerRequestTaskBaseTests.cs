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

    using Microsoft.Practices.ServiceLocation;

    public class PerRequestTaskBaseTests
    {
        [Fact]
        public void Should_be_able_to_execute()
        {
            var task = new PerRequestTaskBaseTestDouble();

            task.Execute(new PerRequestExecutionContext(new Mock<HttpContextBase>().Object, new Mock<IServiceLocator>().Object));

            Assert.True(task.IsExecuted);
        }

        private sealed class PerRequestTaskBaseTestDouble : PerRequestTaskBase
        {
            public bool IsExecuted;

            protected override void ExecuteCore(PerRequestExecutionContext executionContext)
            {
                IsExecuted = true;
            }
        }
    }
}