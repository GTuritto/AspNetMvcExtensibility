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

        public virtual BooleanMetadataItemBuilder Configure(Expression<Func<TModel, bool>> expression)
        {
            return new BooleanMetadataItemBuilder(Append<BooleanMetadataItem, bool>(expression));
        }

        public virtual BooleanMetadataItemBuilder Configure(Expression<Func<TModel, bool?>> expression)
        {
            return new BooleanMetadataItemBuilder(Append<BooleanMetadataItem, bool?>(expression));
        }

        public virtual ValueTypeMetadataItemBuilder<DateTime> Configure(Expression<Func<TModel, DateTime>> expression)
        {
            return new ValueTypeMetadataItemBuilder<DateTime>(Append<ValueTypeMetadataItem, DateTime>(expression));
        }

        public virtual ValueTypeMetadataItemBuilder<DateTime?> Configure(Expression<Func<TModel, DateTime?>> expression)
        {
            return new ValueTypeMetadataItemBuilder<DateTime?>(Append<ValueTypeMetadataItem, DateTime?>(expression));
        }

        public virtual ValueTypeMetadataItemBuilder<byte> Configure(Expression<Func<TModel, byte>> expression)
        {
            return new ValueTypeMetadataItemBuilder<byte>(Append<ValueTypeMetadataItem, byte>(expression));
        }

        public virtual ValueTypeMetadataItemBuilder<byte?> Configure(Expression<Func<TModel, byte?>> expression)
        {
            return new ValueTypeMetadataItemBuilder<byte?>(Append<ValueTypeMetadataItem, byte?>(expression));
        }

        public virtual ValueTypeMetadataItemBuilder<short> Configure(Expression<Func<TModel, short>> expression)
        {
            return new ValueTypeMetadataItemBuilder<short>(Append<ValueTypeMetadataItem, short>(expression));
        }

        public virtual ValueTypeMetadataItemBuilder<short?> Configure(Expression<Func<TModel, short?>> expression)
        {
            return new ValueTypeMetadataItemBuilder<short?>(Append<ValueTypeMetadataItem, short?>(expression));
        }

        public virtual ValueTypeMetadataItemBuilder<int> Configure(Expression<Func<TModel, int>> expression)
        {
            return new ValueTypeMetadataItemBuilder<int>(Append<ValueTypeMetadataItem, int>(expression));
        }

        public virtual ValueTypeMetadataItemBuilder<int?> Configure(Expression<Func<TModel, int?>> expression)
        {
            return new ValueTypeMetadataItemBuilder<int?>(Append<ValueTypeMetadataItem, int?>(expression));
        }

        public virtual ValueTypeMetadataItemBuilder<long> Configure(Expression<Func<TModel, long>> expression)
        {
            return new ValueTypeMetadataItemBuilder<long>(Append<ValueTypeMetadataItem, long>(expression));
        }

        public virtual ValueTypeMetadataItemBuilder<long?> Configure(Expression<Func<TModel, long?>> expression)
        {
            return new ValueTypeMetadataItemBuilder<long?>(Append<ValueTypeMetadataItem, long?>(expression));
        }

        public virtual ValueTypeMetadataItemBuilder<float> Configure(Expression<Func<TModel, float>> expression)
        {
            return new ValueTypeMetadataItemBuilder<float>(Append<ValueTypeMetadataItem, float>(expression));
        }

        public virtual ValueTypeMetadataItemBuilder<float?> Configure(Expression<Func<TModel, float?>> expression)
        {
            return new ValueTypeMetadataItemBuilder<float?>(Append<ValueTypeMetadataItem, float?>(expression));
        }

        public virtual ValueTypeMetadataItemBuilder<double> Configure(Expression<Func<TModel, double>> expression)
        {
            return new ValueTypeMetadataItemBuilder<double>(Append<ValueTypeMetadataItem, double>(expression));
        }

        public virtual ValueTypeMetadataItemBuilder<double?> Configure(Expression<Func<TModel, double?>> expression)
        {
            return new ValueTypeMetadataItemBuilder<double?>(Append<ValueTypeMetadataItem, double?>(expression));
        }

        public virtual ValueTypeMetadataItemBuilder<decimal> Configure(Expression<Func<TModel, decimal>> expression)
        {
            return new ValueTypeMetadataItemBuilder<decimal>(Append<ValueTypeMetadataItem, decimal>(expression));
        }

        public virtual ValueTypeMetadataItemBuilder<decimal?> Configure(Expression<Func<TModel, decimal?>> expression)
        {
            return new ValueTypeMetadataItemBuilder<decimal?>(Append<ValueTypeMetadataItem, decimal?>(expression));
        }

        public virtual ObjectMetadataItemBuilder<TModel> Configure<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            return new ObjectMetadataItemBuilder<TModel>(Append<ObjectMetadataItem, TValue>(expression));
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