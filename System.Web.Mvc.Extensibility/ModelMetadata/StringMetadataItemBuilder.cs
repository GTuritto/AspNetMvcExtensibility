#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Diagnostics;
    using Linq;

    public class StringMetadataItemBuilder : ModelMetadataItemBuilderBase<StringMetadataItem, StringMetadataItemBuilder>
    {
        private static string emailExpression = @"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$";
        private static string emailErrorMessage = ExceptionMessages.InvalidEmailAddressFormat;

        private static string urlExpression = @"(ftp|http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
        private static string urlErrorMessage = ExceptionMessages.InvalidUrlFormat;

        public StringMetadataItemBuilder(StringMetadataItem item) : base(item)
        {
        }

        public static string EmailExpression
        {
            [DebuggerStepThrough]
            get
            {
                return emailExpression;
            }

            [DebuggerStepThrough]
            set
            {
                emailExpression = value;
            }
        }

        public static string EmailErrorMessage
        {
            [DebuggerStepThrough]
            get
            {
                return emailErrorMessage;
            }

            [DebuggerStepThrough]
            set
            {
                emailErrorMessage = value;
            }
        }

        public static string UrlExpression
        {
            [DebuggerStepThrough]
            get
            {
                return urlExpression;
            }

            [DebuggerStepThrough]
            set
            {
                urlExpression = value;
            }
        }

        public static string UrlErrorMessage
        {
            [DebuggerStepThrough]
            get
            {
                return urlErrorMessage;
            }

            [DebuggerStepThrough]
            set
            {
                urlErrorMessage = value;
            }
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
            Template("EmailAddress");

            if (GetExpressionValidation() != null)
            {
                throw new InvalidOperationException(ExceptionMessages.CannotApplyEmailWhenThereIsAActiveExpression);
            }

            return Expression(EmailExpression, EmailErrorMessage);
        }

        public virtual StringMetadataItemBuilder AsHtml()
        {
            return Template("Html");
        }

        public virtual StringMetadataItemBuilder AsUrl()
        {
            Template("Url");

            if (GetExpressionValidation() != null)
            {
                throw new InvalidOperationException(ExceptionMessages.CannotApplyUrlWhenThereIsAActiveExpression);
            }

            return Expression(UrlExpression, UrlErrorMessage);
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
            RegularExpressionValidationMetadata regularExpressionValidation = GetExpressionValidation();

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

        private RegularExpressionValidationMetadata GetExpressionValidation()
        {
            return Item.Validations.OfType<RegularExpressionValidationMetadata>().SingleOrDefault();
        }
    }
}