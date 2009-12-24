#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Linq;

    public class NumericMetadataItemBuilderBase<TNumeric, TItemBuilder> : ModelMetadataItemBuilderBase<NumericMetadataItem, NumericMetadataItemBuilderBase<TNumeric, TItemBuilder>> where TItemBuilder : NumericMetadataItemBuilderBase<TNumeric, TItemBuilder>
    {
        public NumericMetadataItemBuilderBase(NumericMetadataItem item) : base(item)
        {
        }

        public virtual TItemBuilder DisplayFormat(string value)
        {
            Item.DisplayFormat = value;

            return this as TItemBuilder;
        }

        public virtual TItemBuilder EditFormat(string value)
        {
            Item.EditFormat = value;

            return this as TItemBuilder;
        }

        public virtual TItemBuilder ApplyFormatInEditMode()
        {
            Item.ApplyFormatInEditMode = true;

            return this as TItemBuilder;
        }

        public virtual TItemBuilder Format(string value)
        {
            Item.DisplayFormat = Item.EditFormat = value;

            return this as TItemBuilder;
        }

        public virtual TItemBuilder Range(TNumeric minimum, TNumeric maximum, string errorMessage)
        {
            RangeValidationMetadata<TNumeric> rangeValidation = Item.Validations
                                                                    .OfType<RangeValidationMetadata<TNumeric>>()
                                                                    .FirstOrDefault();

            if (rangeValidation == null)
            {
                rangeValidation = new RangeValidationMetadata<TNumeric>();
                Item.Validations.Add(rangeValidation);
            }

            rangeValidation.Minimum = minimum;
            rangeValidation.Maximum = maximum;
            rangeValidation.ErrorMessage = errorMessage;

            return this as TItemBuilder;
        }
    }
}