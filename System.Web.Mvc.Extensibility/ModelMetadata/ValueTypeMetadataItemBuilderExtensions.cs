#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    public static class ValueTypeMetadataItemBuilderExtensions
    {
        public static ValueTypeMetadataItemBuilder<decimal> AsCurrency(this ValueTypeMetadataItemBuilder<decimal> instance)
        {
            return instance.Format("{0:c}");
        }

        public static ValueTypeMetadataItemBuilder<decimal?> AsCurrency(this ValueTypeMetadataItemBuilder<decimal?> instance)
        {
            return instance.Format("{0:c}");
        }

        public static ValueTypeMetadataItemBuilder<DateTime> AsDateOnly(this ValueTypeMetadataItemBuilder<DateTime> instance)
        {
            return instance.Format("{0:d}");
        }

        public static ValueTypeMetadataItemBuilder<DateTime?> AsDateOnly(this ValueTypeMetadataItemBuilder<DateTime?> instance)
        {
            return instance.Format("{0:d}");
        }

        public static ValueTypeMetadataItemBuilder<DateTime> AsTimeOnly(this ValueTypeMetadataItemBuilder<DateTime> instance)
        {
            return instance.Format("{0:t}");
        }

        public static ValueTypeMetadataItemBuilder<DateTime?> AsTimeOnly(this ValueTypeMetadataItemBuilder<DateTime?> instance)
        {
            return instance.Format("{0:t}");
        }
    }
}