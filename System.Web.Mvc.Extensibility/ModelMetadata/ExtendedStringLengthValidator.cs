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
    /// Defines a class that is used to validate string length.
    /// </summary>
    public class ExtendedStringLengthValidator : ExtendedValidatorBase<StringLengthAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedStringLengthValidator"/> class.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="validationMetadata">The validation metadata.</param>
        public ExtendedStringLengthValidator(ModelMetadata metadata, ControllerContext controllerContext, ModelValidationMetadataBase validationMetadata) : base(metadata, controllerContext)
        {
            StringLengthValidationMetadata stringLengthValidationMetadata = validationMetadata as StringLengthValidationMetadata;

            if (stringLengthValidationMetadata == null)
            {
                throw new InvalidCastException();
            }

            if (!string.IsNullOrEmpty(stringLengthValidationMetadata.ErrorMessage))
            {
                Attribute = new StringLengthAttribute(stringLengthValidationMetadata.Maximum) { ErrorMessage = stringLengthValidationMetadata.ErrorMessage };
            }
            else if ((stringLengthValidationMetadata.ErrorMessageResourceType != null) && (!string.IsNullOrEmpty(stringLengthValidationMetadata.ErrorMessageResourceName)))
            {
                Attribute = new StringLengthAttribute(stringLengthValidationMetadata.Maximum) { ErrorMessageResourceType = stringLengthValidationMetadata.ErrorMessageResourceType, ErrorMessageResourceName = stringLengthValidationMetadata.ErrorMessageResourceName };
            }
            else
            {
                Attribute = new StringLengthAttribute(stringLengthValidationMetadata.Maximum);
            }
        }

        /// <summary>
        /// Gets metadata for client validation.
        /// </summary>
        /// <returns>The metadata for client validation.</returns>
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] { new ModelClientValidationStringLengthRule(ErrorMessage, 0, Attribute.MaximumLength) };
        }
    }
}