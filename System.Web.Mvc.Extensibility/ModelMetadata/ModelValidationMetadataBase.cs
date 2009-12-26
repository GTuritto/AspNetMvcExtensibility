#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    /// <summary>
    /// Represents a base class to store validation metadata.
    /// </summary>
    public abstract class ModelValidationMetadataBase : IModelValidationMetadata
    {
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of the error message resource.
        /// </summary>
        /// <value>The type of the error message resource.</value>
        public Type ErrorMessageResourceType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the error message resource.
        /// </summary>
        /// <value>The name of the error message resource.</value>
        public string ErrorMessageResourceName
        {
            get;
            set;
        }

        /// <summary>
        /// Creates the validator.
        /// </summary>
        /// <param name="modelMetadata">The model metadata.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public ModelValidator CreateValidator(ExtendedModelMetadata modelMetadata, ControllerContext context)
        {
            Invariant.IsNotNull(modelMetadata, "modelMetadata");
            Invariant.IsNotNull(context, "context");

            return CreateValidatorCore(modelMetadata, context);
        }

        /// <summary>
        /// Creates the validator.
        /// </summary>
        /// <param name="modelMetadata">The model metadata.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        protected abstract ModelValidator CreateValidatorCore(ExtendedModelMetadata modelMetadata, ControllerContext context);
    }
}