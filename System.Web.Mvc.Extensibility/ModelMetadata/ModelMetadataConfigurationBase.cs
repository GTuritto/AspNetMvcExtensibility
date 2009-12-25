#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using ComponentModel;
    using Diagnostics;
    using Linq.Expressions;

    public abstract class ModelMetadataConfigurationBase<TModel> : IModelMetadataConfiguration, IFluentSyntax where TModel : class
    {
        private readonly Type modelType = typeof(TModel);
        private readonly IDictionary<string, ModelMetadataItemBase> configurations = new Dictionary<string, ModelMetadataItemBase>(StringComparer.OrdinalIgnoreCase);

        public virtual Type ModelType
        {
            [DebuggerStepThrough, EditorBrowsable(EditorBrowsableState.Never)]
            get
            {
                return modelType;
            }
        }

        public virtual IDictionary<string, ModelMetadataItemBase> Configurations
        {
            [DebuggerStepThrough, EditorBrowsable(EditorBrowsableState.Never)]
            get
            {
                return configurations;
            }
        }

        protected StringMetadataItemBuilder Configure(Expression<Func<TModel, string>> expression)
        {
            return new StringMetadataItemBuilder(Append<StringMetadataItem, string>(expression));
        }

        protected BooleanMetadataItemBuilder Configure(Expression<Func<TModel, bool>> expression)
        {
            return new BooleanMetadataItemBuilder(Append<BooleanMetadataItem, bool>(expression));
        }

        protected BooleanMetadataItemBuilder Configure(Expression<Func<TModel, bool?>> expression)
        {
            return new BooleanMetadataItemBuilder(Append<BooleanMetadataItem, bool?>(expression));
        }

        protected ValueTypeMetadataItemBuilder<DateTime> Configure(Expression<Func<TModel, DateTime>> expression)
        {
            return new ValueTypeMetadataItemBuilder<DateTime>(Append<ValueTypeMetadataItem, DateTime>(expression));
        }

        protected ValueTypeMetadataItemBuilder<DateTime?> Configure(Expression<Func<TModel, DateTime?>> expression)
        {
            return new ValueTypeMetadataItemBuilder<DateTime?>(Append<ValueTypeMetadataItem, DateTime?>(expression));
        }

        protected ValueTypeMetadataItemBuilder<byte> Configure(Expression<Func<TModel, byte>> expression)
        {
            return new ValueTypeMetadataItemBuilder<byte>(Append<ValueTypeMetadataItem, byte>(expression));
        }

        protected ValueTypeMetadataItemBuilder<byte?> Configure(Expression<Func<TModel, byte?>> expression)
        {
            return new ValueTypeMetadataItemBuilder<byte?>(Append<ValueTypeMetadataItem, byte?>(expression));
        }

        protected ValueTypeMetadataItemBuilder<short> Configure(Expression<Func<TModel, short>> expression)
        {
            return new ValueTypeMetadataItemBuilder<short>(Append<ValueTypeMetadataItem, short>(expression));
        }

        protected ValueTypeMetadataItemBuilder<short?> Configure(Expression<Func<TModel, short?>> expression)
        {
            return new ValueTypeMetadataItemBuilder<short?>(Append<ValueTypeMetadataItem, short?>(expression));
        }

        protected ValueTypeMetadataItemBuilder<int> Configure(Expression<Func<TModel, int>> expression)
        {
            return new ValueTypeMetadataItemBuilder<int>(Append<ValueTypeMetadataItem, int>(expression));
        }

        protected ValueTypeMetadataItemBuilder<int?> Configure(Expression<Func<TModel, int?>> expression)
        {
            return new ValueTypeMetadataItemBuilder<int?>(Append<ValueTypeMetadataItem, int?>(expression));
        }

        protected ValueTypeMetadataItemBuilder<long> Configure(Expression<Func<TModel, long>> expression)
        {
            return new ValueTypeMetadataItemBuilder<long>(Append<ValueTypeMetadataItem, long>(expression));
        }

        protected ValueTypeMetadataItemBuilder<long?> Configure(Expression<Func<TModel, long?>> expression)
        {
            return new ValueTypeMetadataItemBuilder<long?>(Append<ValueTypeMetadataItem, long?>(expression));
        }

        protected ValueTypeMetadataItemBuilder<float> Configure(Expression<Func<TModel, float>> expression)
        {
            return new ValueTypeMetadataItemBuilder<float>(Append<ValueTypeMetadataItem, float>(expression));
        }

        protected ValueTypeMetadataItemBuilder<float?> Configure(Expression<Func<TModel, float?>> expression)
        {
            return new ValueTypeMetadataItemBuilder<float?>(Append<ValueTypeMetadataItem, float?>(expression));
        }

        protected ValueTypeMetadataItemBuilder<double> Configure(Expression<Func<TModel, double>> expression)
        {
            return new ValueTypeMetadataItemBuilder<double>(Append<ValueTypeMetadataItem, double>(expression));
        }

        protected ValueTypeMetadataItemBuilder<double?> Configure(Expression<Func<TModel, double?>> expression)
        {
            return new ValueTypeMetadataItemBuilder<double?>(Append<ValueTypeMetadataItem, double?>(expression));
        }

        protected ValueTypeMetadataItemBuilder<decimal> Configure(Expression<Func<TModel, decimal>> expression)
        {
            return new ValueTypeMetadataItemBuilder<decimal>(Append<ValueTypeMetadataItem, decimal>(expression));
        }

        protected ValueTypeMetadataItemBuilder<decimal?> Configure(Expression<Func<TModel, decimal?>> expression)
        {
            return new ValueTypeMetadataItemBuilder<decimal?>(Append<ValueTypeMetadataItem, decimal?>(expression));
        }

        protected ObjectMetadataItemBuilder<TModel> Configure<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            return new ObjectMetadataItemBuilder<TModel>(Append<ObjectMetadataItem, TValue>(expression));
        }

        protected virtual TItem Append<TItem, TType>(Expression<Func<TModel, TType>> expression) where TItem : ModelMetadataItemBase, new()
        {
            Invariant.IsNotNull(expression, "expression");

            string property = ExpressionHelper.GetExpressionText(expression);

            TItem item = new TItem();
            configurations.Add(property, item);

            return item;
        }
    }
}