#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using Diagnostics;

    public class ModelMetadataRegistry : IModelMetadataRegistry
    {
        private readonly IDictionary<Type, IDictionary<string, ModelMetadataItemBase>> configurations = new Dictionary<Type, IDictionary<string, ModelMetadataItemBase>>();

        protected virtual IDictionary<Type, IDictionary<string, ModelMetadataItemBase>> Configurations
        {
            [DebuggerStepThrough]
            get
            {
                return configurations;
            }
        }

        public virtual IModelMetadataRegistry Register<TModel>(Action<ModelMetadataItemConfigurator<TModel>> configuration)
        {
            Invariant.IsNotNull(configuration, "configuration");

            IDictionary<string, ModelMetadataItemBase> items = new Dictionary<string, ModelMetadataItemBase>(StringComparer.OrdinalIgnoreCase);
            Configurations[typeof(TModel)] = items;

            ModelMetadataItemConfigurator<TModel> configurator = new ModelMetadataItemConfigurator<TModel>(items);

            configuration(configurator);

            return this;
        }

        public virtual IDictionary<string, ModelMetadataItemBase> Matching(Type modelType)
        {
            Invariant.IsNotNull(modelType, "modelType");

            IDictionary<string, ModelMetadataItemBase> properties;

            return configurations.TryGetValue(modelType, out properties) ? properties : null;
        }

        public virtual ModelMetadataItemBase Matching(Type modelType, string propertyName)
        {
            Invariant.IsNotNull(modelType, "modelType");
            Invariant.IsNotNull(propertyName, "propertyName");

            IDictionary<string, ModelMetadataItemBase> properties;

            if (!configurations.TryGetValue(modelType, out properties))
            {
                return null;
            }

            ModelMetadataItemBase propertyMetadata;

            return properties.TryGetValue(propertyName, out propertyMetadata) ? propertyMetadata : null;
        }
    }
}