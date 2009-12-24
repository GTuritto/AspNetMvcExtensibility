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

    public class ExtendedRegularExpressionValidator : ModelValidator
    {
        private readonly RegularExpressionAttribute attribute;

        public ExtendedRegularExpressionValidator(ModelMetadata metadata, ControllerContext controllerContext, ModelValidationMetadataBase validationMetadata) : base(metadata, controllerContext)
        {
            RegularExpressionValidationMetadata regularExpressionValidationMetadata = validationMetadata as RegularExpressionValidationMetadata;

            if (regularExpressionValidationMetadata == null)
            {
                throw new InvalidCastException();
            }

            attribute = new RegularExpressionAttribute(regularExpressionValidationMetadata.Pattern) { ErrorMessage = regularExpressionValidationMetadata.ErrorMessage };
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] { new ModelClientValidationRegexRule(attribute.ErrorMessage, attribute.Pattern) };
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