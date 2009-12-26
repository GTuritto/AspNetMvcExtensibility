#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    /// <summary>
    /// Represents an interface to store validation metadata.
    /// </summary>
    public interface IModelValidationMetadata
    {
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        string ErrorMessage
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
        ModelValidator CreateValidator(ExtendedModelMetadata modelMetadata, ControllerContext context);
    }
}