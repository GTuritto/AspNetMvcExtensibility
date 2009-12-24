#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Linq;

    public class StringMetadataItemBuilder : ModelMetadataItemBuilderBase<StringMetadataItem, StringMetadataItemBuilder>
    {
        public StringMetadataItemBuilder(StringMetadataItem item) : base(item)
        {
        }

        public virtual StringMetadataItemBuilder DisplayFormat(string value)
        {
            Item.DisplayFormat = value;

            return this;
        }

        public virtual StringMetadataItemBuilder EditFormat(string value)
        {
            Item.EditFormat = value;

            return this;
        }

        public virtual StringMetadataItemBuilder Format(string value)
        {
            Item.DisplayFormat = Item.EditFormat = value;

            return this;
        }

        public virtual StringMetadataItemBuilder ApplyFormatInEditMode()
        {
            Item.ApplyFormatInEditMode = true;

            return this;
        }

        public virtual StringMetadataItemBuilder AsEmail()
        {
            return Template("EmailAddress");
        }

        public virtual StringMetadataItemBuilder AsHtml()
        {
            return Template("Html");
        }

        public virtual StringMetadataItemBuilder AsUrl()
        {
            return Template("Url");
        }

        public virtual StringMetadataItemBuilder AsMultilineText()
        {
            return Template("MultilineText");
        }

        public virtual StringMetadataItemBuilder AsPassword()
        {
            return Template("Password");
        }

        public virtual StringMetadataItemBuilder Expression(string pattern, string errorMessage)
        {
            RegularExpressionValidationMetadata regularExpressionValidation = Item.Validations
                                                                                  .OfType<RegularExpressionValidationMetadata>()
                                                                                  .FirstOrDefault();

            if (regularExpressionValidation == null)
            {
                regularExpressionValidation = new RegularExpressionValidationMetadata();
                Item.Validations.Add(regularExpressionValidation);
            }

            regularExpressionValidation.Pattern = pattern;
            regularExpressionValidation.ErrorMessage = errorMessage;

            return this;
        }

        public virtual StringMetadataItemBuilder MaximumLength(int length, string errorMessage)
        {
            StringLengthValidationMetadata stringLengthValidation = Item.Validations
                                                                        .OfType<StringLengthValidationMetadata>()
                                                                        .FirstOrDefault();

            if (stringLengthValidation == null)
            {
                stringLengthValidation = new StringLengthValidationMetadata();
                Item.Validations.Add(stringLengthValidation);
            }

            stringLengthValidation.Maximum = length;
            stringLengthValidation.ErrorMessage = errorMessage;

            return this;
        }
    }
}