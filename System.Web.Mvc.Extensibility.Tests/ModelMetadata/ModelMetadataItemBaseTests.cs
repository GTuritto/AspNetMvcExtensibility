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

    public class ModelMetadataItemBaseTests
    {
        private readonly Mock<ModelMetadataItemBase> metadataItem;

        public ModelMetadataItemBaseTests()
        {
            metadataItem = new Mock<ModelMetadataItemBase>();
        }

        [Fact]
        public void ShowForDisplay_should_be_true_when_new_instance_is_created()
        {
            Assert.True(metadataItem.Object.ShowForDisplay);
        }

        [Fact]
        public void Validations_should_be_empty()
        {
            Assert.Empty(metadataItem.Object.Validations);
        }

        [Fact]
        public void AdditionalSettings_should_be_empty()
        {
            Assert.Empty(metadataItem.Object.AdditionalSettings);
        }
    }
}