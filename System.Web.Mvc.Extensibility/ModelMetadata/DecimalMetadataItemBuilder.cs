#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    public class DecimalMetadataItemBuilder : NumericMetadataItemBuilderBase<decimal, DecimalMetadataItemBuilder>
    {
        private static readonly string decimalTypeName = typeof(decimal).Name;

        public DecimalMetadataItemBuilder(NumericMetadataItem item) : base(item)
        {
            Item.TemplateName = decimalTypeName;
        }

        public virtual DecimalMetadataItemBuilder AsCurrency()
        {
            Item.DisplayFormat = Item.EditFormat = "{0:c}";

            return this;
        }
    }
}