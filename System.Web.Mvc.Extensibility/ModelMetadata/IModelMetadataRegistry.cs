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

    public interface IModelMetadataRegistry : IFluentSyntax
    {
        IModelMetadataRegistry Register<TModel>(Action<ModelMetadataItemConfigurator<TModel>> configuration);

        [EditorBrowsable(EditorBrowsableState.Never)]
        IDictionary<string, ModelMetadataItemBase> Matching(Type modelType);

        [EditorBrowsable(EditorBrowsableState.Never)]
        ModelMetadataItemBase Matching(Type modelType, string propertyName);
    }
}