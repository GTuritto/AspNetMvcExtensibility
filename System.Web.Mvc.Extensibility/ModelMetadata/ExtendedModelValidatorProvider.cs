#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using Linq;

    public class ExtendedModelValidatorProvider : ModelValidatorProvider
    {
        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
        {
            Invariant.IsNotNull(metadata, "metadata");
            Invariant.IsNotNull(context, "context");

            ExtendedModelMetadata extendedModelMetadata = metadata as ExtendedModelMetadata;

            if (extendedModelMetadata == null)
            {
                throw new InvalidCastException();
            }

            return (extendedModelMetadata.Metadata != null) ?
                   extendedModelMetadata.Metadata.Validations.Select(validationMeta => validationMeta.CreateValidator(extendedModelMetadata, context)) :
                   Enumerable.Empty<ModelValidator>();
        }
    }
}