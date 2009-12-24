#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using ComponentModel.DataAnnotations;

    public class ExtendedStringLengthValidator : ModelValidator
    {
        private readonly StringLengthAttribute attribute;

        public ExtendedStringLengthValidator(ModelMetadata metadata, ControllerContext controllerContext, ModelValidationMetadataBase validationMetadata) : base(metadata, controllerContext)
        {
            StringLengthValidationMetadata stringLengthValidationMetadata = validationMetadata as StringLengthValidationMetadata;

            if (stringLengthValidationMetadata == null)
            {
                throw new InvalidCastException();
            }

            attribute = new StringLengthAttribute(stringLengthValidationMetadata.Maximum) { ErrorMessage = stringLengthValidationMetadata.ErrorMessage };
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] { new ModelClientValidationStringLengthRule(attribute.ErrorMessage, 0, attribute.MaximumLength) };
        }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            if (!attribute.IsValid(Metadata.Model))
            {
                yield return new ModelValidationResult
                {
                    Message = attribute.ErrorMessage
                };
            }
        }
    }
}