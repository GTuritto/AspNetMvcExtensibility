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

    public class ExtendedRangeValidator<TNumeric> : ModelValidator
    {
        private readonly RangeAttribute attribute;

        public ExtendedRangeValidator(ModelMetadata metadata, ControllerContext controllerContext, ModelValidationMetadataBase validationMetadata) : base(metadata, controllerContext)
        {
            RangeValidationMetadata<TNumeric> rangeValidationMetadata = validationMetadata as RangeValidationMetadata<TNumeric>;

            if (rangeValidationMetadata == null)
            {
                throw new InvalidCastException();
            }

            attribute = new RangeAttribute(typeof(TNumeric), rangeValidationMetadata.Minimum.ToString(), rangeValidationMetadata.Maximum.ToString()) { ErrorMessage = rangeValidationMetadata.ErrorMessage };
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] { new ModelClientValidationRangeRule(attribute.ErrorMessage, attribute.Minimum, attribute.Maximum) };
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