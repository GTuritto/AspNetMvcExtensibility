#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;

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

        public override IEnumerable<ModelValidator> GetValidators(ControllerContext context)
        {
            if (Metadata == null)
            {
                yield break;
            }

            Invariant.IsNotNull(context, "context");

            foreach (IModelValidationMetadata validationMeta in Metadata.Validations)
            {
                yield return validationMeta.CreateValidator(this, context);
            }
        }
    }
}