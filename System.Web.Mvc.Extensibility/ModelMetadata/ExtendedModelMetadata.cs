#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    public class ExtendedModelMetadata : ModelMetadata
    {
        public ExtendedModelMetadata(ModelMetadataProvider provider, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName, ModelMetadataItemBase metadata) : base(provider, containerType, modelAccessor, modelType, propertyName)
        {
            Metadata = metadata;
        }

        public ModelMetadataItemBase Metadata
        {
            get;
            private set;
        }
    }
}