#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    public abstract class ModelValidationMetadataBase : IModelValidationMetadata
    {
        public string ErrorMessage
        {
            get;
            set;
        }

        public ModelValidator CreateValidator(ExtendedModelMetadata modelMetadata, ControllerContext context)
        {
            Invariant.IsNotNull(modelMetadata, "modelMetadata");
            Invariant.IsNotNull(context, "context");

            return CreateValidatorCore(modelMetadata, context);
        }

        protected abstract ModelValidator CreateValidatorCore(ExtendedModelMetadata modelMetadata, ControllerContext context);
    }
}