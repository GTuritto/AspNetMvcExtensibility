#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Linq;

    public class ValueTypeMetadataItemBuilder<TValueType> : ModelMetadataItemBuilderBase<ValueTypeMetadataItem, ValueTypeMetadataItemBuilder<TValueType>>
    {
        public ValueTypeMetadataItemBuilder(ValueTypeMetadataItem item) : base(item)
        {
        }

        public virtual ValueTypeMetadataItemBuilder<TValueType> DisplayFormat(string value)
        {
            Item.DisplayFormat = value;

            return this;
        }

        public virtual ValueTypeMetadataItemBuilder<TValueType> EditFormat(string value)
        {
            Item.EditFormat = value;

            return this;
        }

        public virtual ValueTypeMetadataItemBuilder<TValueType> ApplyFormatInEditMode()
        {
            Item.ApplyFormatInEditMode = true;

            return this;
        }

        public virtual ValueTypeMetadataItemBuilder<TValueType> Format(string value)
        {
            Item.DisplayFormat = Item.EditFormat = value;

            return this;
        }

        public virtual ValueTypeMetadataItemBuilder<TValueType> Range(TValueType minimum, TValueType maximum, string errorMessage)
        {
            RangeValidationMetadata<TValueType> rangeValidation = Item.Validations
                                                                    .OfType<RangeValidationMetadata<TValueType>>()
                                                                    .FirstOrDefault();

            if (rangeValidation == null)
            {
                rangeValidation = new RangeValidationMetadata<TValueType>();
                Item.Validations.Add(rangeValidation);
            }

            rangeValidation.Minimum = minimum;
            rangeValidation.Maximum = maximum;
            rangeValidation.ErrorMessage = errorMessage;

            return this;
        }
    }
}