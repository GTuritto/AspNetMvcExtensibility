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

    /// <summary>
    /// Represents an interface to store all the metadata of the models.
    /// </summary>
    public interface IModelMetadataRegistry
    {
        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <param name="metadataDictionary">The metadata dictionary.</param>
        void Register(Type modelType, IDictionary<string, ModelMetadataItemBase> metadataDictionary);

        /// <summary>
        /// Gets the Matchings metadata of the given model.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        IDictionary<string, ModelMetadataItemBase> Matching(Type modelType);

        /// <summary>
        /// Gets the Matchings metadata of the given model property.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        ModelMetadataItemBase Matching(Type modelType, string propertyName);
    }
}