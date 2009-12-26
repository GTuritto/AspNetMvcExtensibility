#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Diagnostics;
    using Linq;

    /// <summary>
    /// Defines a class to fluently configure metadata of a <seealso cref="string"/> type.
    /// </summary>
    public class StringMetadataItemBuilder : ModelMetadataItemBuilderBase<StringMetadataItem, StringMetadataItemBuilder>
    {
        private static string emailExpression = @"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$";
        private static string emailErrorMessage = ExceptionMessages.InvalidEmailAddressFormat;

        private static string urlExpression = @"(ftp|http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
        private static string urlErrorMessage = ExceptionMessages.InvalidUrlFormat;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringMetadataItemBuilder"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public StringMetadataItemBuilder(StringMetadataItem item) : base(item)
        {
        }

        /// <summary>
        /// Gets or sets the email expression.
        /// </summary>
        /// <value>The email expression.</value>
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

        /// <summary>
        /// Gets or sets the email error message.
        /// </summary>
        /// <value>The email error message.</value>
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

        /// <summary>
        /// Gets or sets the URL expression.
        /// </summary>
        /// <value>The URL expression.</value>
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

        /// <summary>
        /// Gets or sets the URL error message.
        /// </summary>
        /// <value>The URL error message.</value>
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

        /// <summary>
        /// Sets the format in display mode.
        /// </summary>
        /// <param name="format">The value.</param>
        /// <returns></returns>
        public virtual StringMetadataItemBuilder DisplayFormat(string format)
        {
            Item.DisplayFormat = format;

            return this;
        }

        /// <summary>
        /// Sets the format in edit mode.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public virtual StringMetadataItemBuilder EditFormat(string format)
        {
            Item.EditFormat = format;

            return this;
        }

        /// <summary>
        /// Sets format for both display and edit mode.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual StringMetadataItemBuilder Format(string value)
        {
            Item.DisplayFormat = Item.EditFormat = value;

            return this;
        }

        /// <summary>
        /// Indicates to apply format in edit mode.
        /// </summary>
        /// <returns></returns>
        public virtual StringMetadataItemBuilder ApplyFormatInEditMode()
        {
            Item.ApplyFormatInEditMode = true;

            return this;
        }

        /// <summary>
        /// Indicates that the value would appear as email address in display mode.
        /// </summary>
        /// <returns></returns>
        public virtual StringMetadataItemBuilder AsEmail()
        {
            Template("EmailAddress");

            if (GetExpressionValidation() != null)
            {
                throw new InvalidOperationException(ExceptionMessages.CannotApplyEmailWhenThereIsAActiveExpression);
            }

            return Expression(EmailExpression, EmailErrorMessage);
        }

        /// <summary>
        /// Indicates that the value would appear as raw html in display mode, so no encoding will be performed.
        /// </summary>
        /// <returns></returns>
        public virtual StringMetadataItemBuilder AsHtml()
        {
            return Template("Html");
        }

        /// <summary>
        /// Indicates that the value would appear as url in display mode.
        /// </summary>
        /// <returns></returns>
        public virtual StringMetadataItemBuilder AsUrl()
        {
            Template("Url");

            if (GetExpressionValidation() != null)
            {
                throw new InvalidOperationException(ExceptionMessages.CannotApplyUrlWhenThereIsAActiveExpression);
            }

            return Expression(UrlExpression, UrlErrorMessage);
        }

        /// <summary>
        /// Marks the value to render as textarea element in edit mode.
        /// </summary>
        /// <returns></returns>
        public virtual StringMetadataItemBuilder AsMultilineText()
        {
            return Template("MultilineText");
        }

        /// <summary>
        /// Marks the value to render as password element in edit mode.
        /// </summary>
        /// <returns></returns>
        public virtual StringMetadataItemBuilder AsPassword()
        {
            return Template("Password");
        }

        /// <summary>
        /// Sets the regular expression that the value must match, this comes into action when is <code>Required</code> is <code>true</code>.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Sets the maximum length of the value, this comes into action when is <code>Required</code> is <code>true</code>.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns></returns>
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