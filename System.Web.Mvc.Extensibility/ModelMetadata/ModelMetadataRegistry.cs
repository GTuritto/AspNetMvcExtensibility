#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using Diagnostics;

    /// <summary>
    /// Defines a class to store all the metadata of the models.
    /// </summary>
    public class ModelMetadataRegistry : IModelMetadataRegistry
    {
        private readonly IDictionary<Type, IDictionary<string, ModelMetadataItemBase>> configurations = new Dictionary<Type, IDictionary<string, ModelMetadataItemBase>>();

        /// <summary>
        /// Gets the configurations.
        /// </summary>
        /// <value>The configurations.</value>
        protected virtual IDictionary<Type, IDictionary<string, ModelMetadataItemBase>> Configurations
        {
            [DebuggerStepThrough]
            get
            {
                return configurations;
            }
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <param name="metadataDictionary">The metadata dictionary.</param>
        public virtual void Register(Type modelType, IDictionary<string, ModelMetadataItemBase> metadataDictionary)
        {
            Invariant.IsNotNull(modelType, "modelType");
            Invariant.IsNotNull(metadataDictionary, "metadataDictionary");

            Configurations.Add(modelType, metadataDictionary);
        }

        /// <summary>
        /// Gets the Matchings metadata of the given model.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <returns></returns>
        public virtual IDictionary<string, ModelMetadataItemBase> Matching(Type modelType)
        {
            Invariant.IsNotNull(modelType, "modelType");

            IDictionary<string, ModelMetadataItemBase> properties;

            return configurations.TryGetValue(modelType, out properties) ? properties : null;
        }

        /// <summary>
        /// Gets the Matchings metadata of the given model property.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
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