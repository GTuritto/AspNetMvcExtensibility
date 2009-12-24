#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using ComponentModel;
    using Linq;

    public abstract class ModelMetadataItemBuilderBase<TItem, TItemBuilder> : IFluentSyntax where TItem : ModelMetadataItemBase where TItemBuilder : ModelMetadataItemBuilderBase<TItem, TItemBuilder>
    {
        protected ModelMetadataItemBuilderBase(TItem item)
        {
            Invariant.IsNotNull(item, "item");

            Item = item;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public TItem Item
        {
            get;
            private set;
        }

        public TItemBuilder DisplayName(string value)
        {
            Item.DisplayName = value;

            return this as TItemBuilder;
        }

        public TItemBuilder ShortDisplayName(string value)
        {
            Item.ShortDisplayName = value;

            return this as TItemBuilder;
        }

        public TItemBuilder Template(string value)
        {
            Item.DisplayName = value;

            return this as TItemBuilder;
        }

        public TItemBuilder Description(string value)
        {
            Item.Description = value;

            return this as TItemBuilder;
        }

        public TItemBuilder ReadOnly()
        {
            Item.IsReadOnly = true;

            return this as TItemBuilder;
        }

        public TItemBuilder Required(string errorMessage)
        {
            Item.IsRequired = true;

            RequiredValidationMetadata required = Item.Validations
                                                      .OfType<RequiredValidationMetadata>()
                                                      .FirstOrDefault();

            if (required == null)
            {
                required = new RequiredValidationMetadata();
                Item.Validations.Add(required);
            }

            required.ErrorMessage = errorMessage;

            return this as TItemBuilder;
        }

        public TItemBuilder AsHidden()
        {
            Item.TemplateName = "HiddenInput";

            return this as TItemBuilder;
        }

        public TItemBuilder HideSurroundingHtml()
        {
            Item.HideSurroundingHtml = true;

            return this as TItemBuilder;
        }

        public TItemBuilder ShowForDisplay()
        {
            Item.ShowForDisplay = true;

            return this as TItemBuilder;
        }

        public TItemBuilder HideForDisplay()
        {
            Item.ShowForDisplay = false;

            return this as TItemBuilder;
        }

        public TItemBuilder ShowForEdit()
        {
            Item.ShowForEdit = true;

            return this as TItemBuilder;
        }

        public TItemBuilder HideForEdit()
        {
            Item.ShowForEdit = false;

            return this as TItemBuilder;
        }

        public TItemBuilder Hide()
        {
            Item.ShowForDisplay = false;
            Item.ShowForEdit = false;

            return this as TItemBuilder;
        }

        public TItemBuilder NullDisplayText(string value)
        {
            Item.NullDisplayText = value;

            return this as TItemBuilder;
        }

        public TItemBuilder Watermark(string value)
        {
            Item.Watermark = value;

            return this as TItemBuilder;
        }
    }
}