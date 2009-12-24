#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    public class DateTimeMetadataItemBuilder : ModelMetadataItemBuilderBase<DateTimeMetadataItem, DateTimeMetadataItemBuilder>
    {
        public DateTimeMetadataItemBuilder(DateTimeMetadataItem item) : base(item)
        {
        }

        public virtual DateTimeMetadataItemBuilder DisplayFormat(string value)
        {
            Item.DisplayFormat = value;

            return this;
        }

        public virtual DateTimeMetadataItemBuilder EditFormat(string value)
        {
            Item.EditFormat = value;

            return this;
        }

        public virtual DateTimeMetadataItemBuilder Format(string value)
        {
            Item.DisplayFormat = Item.EditFormat = value;

            return this;
        }

        public virtual DateTimeMetadataItemBuilder ApplyFormatInEditMode()
        {
            Item.ApplyFormatInEditMode = true;

            return this;
        }

        public virtual DateTimeMetadataItemBuilder AsDateOnly()
        {
            return Format("{0:d}");
        }

        public virtual DateTimeMetadataItemBuilder AsTimeOnly()
        {
            return Format("{0:t}");
        }
    }
}