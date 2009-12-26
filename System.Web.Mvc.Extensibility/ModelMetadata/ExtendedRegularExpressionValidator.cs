#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines a class that is used to validate regular expression.
    /// </summary>
    public class ExtendedRegularExpressionValidator : ExtendedValidatorBase<RegularExpressionAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedRegularExpressionValidator"/> class.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="validationMetadata">The validation metadata.</param>
        public ExtendedRegularExpressionValidator(ModelMetadata metadata, ControllerContext controllerContext, ModelValidationMetadataBase validationMetadata) : base(metadata, controllerContext)
        {
            RegularExpressionValidationMetadata regularExpressionValidationMetadata = validationMetadata as RegularExpressionValidationMetadata;

            if (regularExpressionValidationMetadata == null)
            {
                throw new InvalidCastException();
            }

            if (!string.IsNullOrEmpty(regularExpressionValidationMetadata.ErrorMessage))
            {
                Attribute = new RegularExpressionAttribute(regularExpressionValidationMetadata.Pattern) { ErrorMessage = regularExpressionValidationMetadata.ErrorMessage };
            }
            else if ((regularExpressionValidationMetadata.ErrorMessageResourceType != null) && (!string.IsNullOrEmpty(regularExpressionValidationMetadata.ErrorMessageResourceName)))
            {
                Attribute = new RegularExpressionAttribute(regularExpressionValidationMetadata.Pattern) { ErrorMessageResourceType = regularExpressionValidationMetadata.ErrorMessageResourceType, ErrorMessageResourceName = regularExpressionValidationMetadata.ErrorMessageResourceName };
            }
            else
            {
                Attribute = new RegularExpressionAttribute(regularExpressionValidationMetadata.Pattern);
            }
        }

        /// <summary>
        /// Gets metadata for client validation.
        /// </summary>
        /// <returns>The metadata for client validation.</returns>
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] { new ModelClientValidationRegexRule(ErrorMessage, Attribute.Pattern) };
        }
    }
}