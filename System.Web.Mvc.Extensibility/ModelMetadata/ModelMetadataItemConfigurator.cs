#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using Linq.Expressions;

    public class ModelMetadataItemConfigurator<TModel> : IFluentSyntax
    {
        private readonly IDictionary<string, ModelMetadataItemBase> items;

        public ModelMetadataItemConfigurator(IDictionary<string, ModelMetadataItemBase> items)
        {
            Invariant.IsNotNull(items, "items");

            this.items = items;
        }

        public virtual StringMetadataItemBuilder Configure(Expression<Func<TModel, string>> expression)
        {
            return new StringMetadataItemBuilder(Append<StringMetadataItem, string>(expression));
        }

        public virtual DateTimeMetadataItemBuilder Configure(Expression<Func<TModel, DateTime>> expression)
        {
            return new DateTimeMetadataItemBuilder(Append<DateTimeMetadataItem, DateTime>(expression));
        }

        public virtual DateTimeMetadataItemBuilder Configure(Expression<Func<TModel, DateTime?>> expression)
        {
            return new DateTimeMetadataItemBuilder(Append<DateTimeMetadataItem, DateTime?>(expression));
        }

        public virtual DecimalMetadataItemBuilder Configure(Expression<Func<TModel, decimal>> expression)
        {
            return new DecimalMetadataItemBuilder(Append<NumericMetadataItem, decimal>(expression));
        }

        public virtual DecimalMetadataItemBuilder Configure(Expression<Func<TModel, decimal?>> expression)
        {
            return new DecimalMetadataItemBuilder(Append<NumericMetadataItem, decimal?>(expression));
        }

        public virtual NumericMetadataItemBuilder<byte> Configure(Expression<Func<TModel, byte>> expression)
        {
            return new NumericMetadataItemBuilder<byte>(Append<NumericMetadataItem, byte>(expression));
        }

        public virtual NumericMetadataItemBuilder<byte?> Configure(Expression<Func<TModel, byte?>> expression)
        {
            return new NumericMetadataItemBuilder<byte?>(Append<NumericMetadataItem, byte?>(expression));
        }

        public virtual NumericMetadataItemBuilder<short> Configure(Expression<Func<TModel, short>> expression)
        {
            return new NumericMetadataItemBuilder<short>(Append<NumericMetadataItem, short>(expression));
        }

        public virtual NumericMetadataItemBuilder<short?> Configure(Expression<Func<TModel, short?>> expression)
        {
            return new NumericMetadataItemBuilder<short?>(Append<NumericMetadataItem, short?>(expression));
        }

        public virtual NumericMetadataItemBuilder<int> Configure(Expression<Func<TModel, int>> expression)
        {
            return new NumericMetadataItemBuilder<int>(Append<NumericMetadataItem, int>(expression));
        }

        public virtual NumericMetadataItemBuilder<int?> Configure(Expression<Func<TModel, int?>> expression)
        {
            return new NumericMetadataItemBuilder<int?>(Append<NumericMetadataItem, int?>(expression));
        }

        public virtual NumericMetadataItemBuilder<long> Configure(Expression<Func<TModel, long>> expression)
        {
            return new NumericMetadataItemBuilder<long>(Append<NumericMetadataItem, long>(expression));
        }

        public virtual NumericMetadataItemBuilder<long?> Configure(Expression<Func<TModel, long?>> expression)
        {
            return new NumericMetadataItemBuilder<long?>(Append<NumericMetadataItem, long?>(expression));
        }

        public virtual NumericMetadataItemBuilder<float> Configure(Expression<Func<TModel, float>> expression)
        {
            return new NumericMetadataItemBuilder<float>(Append<NumericMetadataItem, float>(expression));
        }

        public virtual NumericMetadataItemBuilder<float?> Configure(Expression<Func<TModel, float?>> expression)
        {
            return new NumericMetadataItemBuilder<float?>(Append<NumericMetadataItem, float?>(expression));
        }

        public virtual NumericMetadataItemBuilder<double> Configure(Expression<Func<TModel, double>> expression)
        {
            return new NumericMetadataItemBuilder<double>(Append<NumericMetadataItem, double>(expression));
        }

        public virtual NumericMetadataItemBuilder<double?> Configure(Expression<Func<TModel, double?>> expression)
        {
            return new NumericMetadataItemBuilder<double?>(Append<NumericMetadataItem, double?>(expression));
        }

        public virtual ObjectMetadataItemBuilder Configure(Expression<Func<TModel, bool>> expression)
        {
            return new ObjectMetadataItemBuilder(Append<ObjectMetadataItem, bool>(expression));
        }

        public virtual ObjectMetadataItemBuilder Configure(Expression<Func<TModel, bool?>> expression)
        {
            return new ObjectMetadataItemBuilder(Append<ObjectMetadataItem, bool?>(expression));
        }

        public virtual ObjectMetadataItemBuilder Configure<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            return new ObjectMetadataItemBuilder(Append<ObjectMetadataItem, TValue>(expression));
        }

        private TItem Append<TItem, TType>(Expression<Func<TModel, TType>> expression) where TItem : ModelMetadataItemBase, new()
        {
            Invariant.IsNotNull(expression, "expression");

            string property = ExpressionHelper.GetExpressionText(expression);

            TItem item = new TItem();
            items[property] = item;

            return item;
        }
    }
}