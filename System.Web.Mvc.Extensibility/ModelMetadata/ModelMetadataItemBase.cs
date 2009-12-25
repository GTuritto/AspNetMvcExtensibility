#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;

    public abstract class ModelMetadataItemBase
    {
        protected ModelMetadataItemBase()
        {
            ShowForDisplay = true;
            Validations = new List<IModelValidationMetadata>();
            AdditionalSettings = new List<IModelMetadataAdditionalSetting>();
        }

        public string DisplayName
        {
            get;
            set;
        }

        public string ShortDisplayName
        {
            get;
            set;
        }

        public string TemplateName
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public bool? IsReadOnly
        {
            get;
            set;
        }

        public bool? IsRequired
        {
            get;
            set;
        }

        public bool? HideSurroundingHtml
        {
            get;
            set;
        }

        public bool ShowForDisplay
        {
            get;
            set;
        }

        public bool? ShowForEdit
        {
            get;
            set;
        }

        public string NullDisplayText
        {
            get;
            set;
        }

        public string Watermark
        {
            get;
            set;
        }

        public IList<IModelValidationMetadata> Validations
        {
            get;
            private set;
        }

        public IList<IModelMetadataAdditionalSetting> AdditionalSettings
        {
            get;
            private set;
        }
    }
}