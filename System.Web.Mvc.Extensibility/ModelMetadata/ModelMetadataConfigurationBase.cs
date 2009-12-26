#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using ComponentModel;
    using Diagnostics;
    using Linq.Expressions;

    /// <summary>
    /// Defines a base class that is used to configure metadata of a model fluently.
    /// </summary>
    public abstract class ModelMetadataConfigurationBase<TModel> : IModelMetadataConfiguration, IFluentSyntax where TModel : class
    {
        private readonly Type modelType = typeof(TModel);
        private readonly IDictionary<string, ModelMetadataItemBase> configurations = new Dictionary<string, ModelMetadataItemBase>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Gets the type of the model.
        /// </summary>
        /// <value>The type of the model.</value>
        public virtual Type ModelType
        {
            [DebuggerStepThrough, EditorBrowsable(EditorBrowsableState.Never)]
            get
            {
                return modelType;
            }
        }

        /// <summary>
        /// Gets the configurations.
        /// </summary>
        /// <value>The configurations.</value>
        public virtual IDictionary<string, ModelMetadataItemBase> Configurations
        {
            [DebuggerStepThrough, EditorBrowsable(EditorBrowsableState.Never)]
            get
            {
                return configurations;
            }
        }

        /// <summary>
        /// Configures the string value.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        protected StringMetadataItemBuilder Configure(Expression<Func<TModel, string>> expression)
        {
            return new StringMetadataItemBuilder(Append<StringMetadataItem, string>(expression));
        }

        /// <summary>
        /// Configures the boolean value.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        protected BooleanMetadataItemBuilder Configure(Expression<Func<TModel, bool>> expression)
        {
            return new BooleanMetadataItemBuilder(Append<BooleanMetadataItem, bool>(expression));
        }

        /// <summary>
        /// Configures the nullable boolean value.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        protected BooleanMetadataItemBuilder Configure(Expression<Func<TModel, bool?>> expression)
        {
            return new BooleanMetadataItemBuilder(Append<BooleanMetadataItem, bool?>(expression));
        }

        /// <summary>
        /// Configures the datetime value.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        protected ValueTypeMetadataItemBuilder<DateTime> Configure(Expression<Func<TModel, DateTime>> expression)
        {
            return new ValueTypeMetadataItemBuilder<DateTime>(Append<ValueTypeMetadataItem, DateTime>(expression));
        }

        /// <summary>
        /// Configures the nullable datetime value.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        protected ValueTypeMetadataItemBuilder<DateTime?> Configure(Expression<Func<TModel, DateTime?>> expression)
        {
            return new ValueTypeMetadataItemBuilder<DateTime?>(Append<ValueTypeMetadataItem, DateTime?>(expression));
        }

        /// <summary>
        /// Configures the byte value.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        protected ValueTypeMetadataItemBuilder<byte> Configure(Expression<Func<TModel, byte>> expression)
        {
            return new ValueTypeMetadataItemBuilder<byte>(Append<ValueTypeMetadataItem, byte>(expression));
        }

        /// <summary>
        /// Configures the nullable byte value.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        protected ValueTypeMetadataItemBuilder<byte?> Configure(Expression<Func<TModel, byte?>> expression)
        {
            return new ValueTypeMetadataItemBuilder<byte?>(Append<ValueTypeMetadataItem, byte?>(expression));
        }

        /// <summary>
        /// Configures the short value.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        protected ValueTypeMetadataItemBuilder<short> Configure(Expression<Func<TModel, short>> expression)
        {
            return new ValueTypeMetadataItemBuilder<short>(Append<ValueTypeMetadataItem, short>(expression));
        }

        /// <summary>
        /// Configures the nullable short value.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        protected ValueTypeMetadataItemBuilder<short?> Configure(Expression<Func<TModel, short?>> expression)
        {
            return new ValueTypeMetadataItemBuilder<short?>(Append<ValueTypeMetadataItem, short?>(expression));
        }

        /// <summary>
        /// Configures the integer value.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        protected ValueTypeMetadataItemBuilder<int> Configure(Expression<Func<TModel, int>> expression)
        {
            return new ValueTypeMetadataItemBuilder<int>(Append<ValueTypeMetadataItem, int>(expression));
        }

        /// <summary>
        /// Configures the nullable integer value.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        protected ValueTypeMetadataItemBuilder<int?> Configure(Expression<Func<TModel, int?>> expression)
        {
            return new ValueTypeMetadataItemBuilder<int?>(Append<ValueTypeMetadataItem, int?>(expression));
        }

        /// <summary>
        /// Configures the long value.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        protected ValueTypeMetadataItemBuilder<long> Configure(Expression<Func<TModel, long>> expression)
        {
            return new ValueTypeMetadataItemBuilder<long>(Append<ValueTypeMetadataItem, long>(expression));
        }

        /// <summary>
        /// Configures the nullabale long value.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        protected ValueTypeMetadataItemBuilder<long?> Configure(Expression<Func<TModel, long?>> expression)
        {
            return new ValueTypeMetadataItemBuilder<long?>(Append<ValueTypeMetadataItem, long?>(expression));
        }

        /// <summary>
        /// Configures the float value.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        protected ValueTypeMetadataItemBuilder<float> Configure(Expression<Func<TModel, float>> expression)
        {
            return new ValueTypeMetadataItemBuilder<float>(Append<ValueTypeMetadataItem, float>(expression));
        }

        /// <summary>
        /// Configures the nullable float value.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        protected ValueTypeMetadataItemBuilder<float?> Configure(Expression<Func<TModel, float?>> expression)
        {
            return new ValueTypeMetadataItemBuilder<float?>(Append<ValueTypeMetadataItem, float?>(expression));
        }

        /// <summary>
        /// Configures the double value.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        protected ValueTypeMetadataItemBuilder<double> Configure(Expression<Func<TModel, double>> expression)
        {
            return new ValueTypeMetadataItemBuilder<double>(Append<ValueTypeMetadataItem, double>(expression));
        }

        /// <summary>
        /// Configures the nullable double value.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        protected ValueTypeMetadataItemBuilder<double?> Configure(Expression<Func<TModel, double?>> expression)
        {
            return new ValueTypeMetadataItemBuilder<double?>(Append<ValueTypeMetadataItem, double?>(expression));
        }

        /// <summary>
        /// Configures the decimal value.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        protected ValueTypeMetadataItemBuilder<decimal> Configure(Expression<Func<TModel, decimal>> expression)
        {
            return new ValueTypeMetadataItemBuilder<decimal>(Append<ValueTypeMetadataItem, decimal>(expression));
        }

        /// <summary>
        /// Configures the nullable decimal value.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        protected ValueTypeMetadataItemBuilder<decimal?> Configure(Expression<Func<TModel, decimal?>> expression)
        {
            return new ValueTypeMetadataItemBuilder<decimal?>(Append<ValueTypeMetadataItem, decimal?>(expression));
        }

        /// <summary>
        /// Configures the specified value.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        protected ObjectMetadataItemBuilder<TModel> Configure<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            return new ObjectMetadataItemBuilder<TModel>(Append<ObjectMetadataItem, TValue>(expression));
        }

        /// <summary>
        /// Appends the specified configuration.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
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