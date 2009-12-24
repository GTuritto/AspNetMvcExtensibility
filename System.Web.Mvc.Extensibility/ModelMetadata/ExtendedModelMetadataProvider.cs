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
    using Linq;
    using Globalization;

    public class ExtendedModelMetadataProvider : ModelMetadataProvider
    {
        private readonly IModelMetadataRegistry registry;

        public ExtendedModelMetadataProvider(IModelMetadataRegistry registry)
        {
            Invariant.IsNotNull(registry, "registry");

            this.registry = registry;
        }

        public override IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType)
        {
            Invariant.IsNotNull(containerType, "containerType");

            IDictionary<string, ModelMetadataItemBase> propertyMetadatas = registry.Matching(containerType);

            if (propertyMetadatas == null)
            {
                return TypeDescriptor.GetProperties(containerType)
                                     .Cast<PropertyDescriptor>()
                                     .Select(property => new ExtendedModelMetadata(this, containerType, () => property.GetValue(container), property.PropertyType, property.Name, null))
                                     .Cast<ModelMetadata>();
            }

            IEnumerable<PropertyDescriptor> propertyDescriptors = TypeDescriptor.GetProperties(containerType)
                                                                                .Cast<PropertyDescriptor>();

            List<ModelMetadata> properties = new List<ModelMetadata>();

            foreach (KeyValuePair<string, ModelMetadataItemBase> pair in propertyMetadatas)
            {
                string propertyName = pair.Key;

                PropertyDescriptor propertyDescriptor = propertyDescriptors.FirstOrDefault(property => property.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));

                if (propertyDescriptor == null)
                {
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessages.ThePropertyNameOfTypeCouldNotBeFound, containerType.FullName, propertyName));
                }

                properties.Add(CreateModelMetaData(containerType, propertyName, propertyDescriptor.PropertyType, pair.Value, () => propertyDescriptor.GetValue(container)));
            }

            return properties;
        }

        public override ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName)
        {
            Invariant.IsNotNull(containerType, "containerType");
            Invariant.IsNotNull(propertyName, "propertyName");

            ModelMetadataItemBase propertyMetaData = registry.Matching(containerType, propertyName);

            if (propertyMetaData == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessages.ThePropertyNameOfTypeCouldNotBeFound, containerType.FullName, propertyName));
            }

            PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(containerType)
                                                                  .Cast<PropertyDescriptor>()
                                                                  .FirstOrDefault(property => property.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));

            if (propertyDescriptor == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessages.ThePropertyNameOfTypeCouldNotBeFound, containerType.FullName, propertyName));
            }

            return CreateModelMetaData(containerType, propertyName, propertyDescriptor.PropertyType, propertyMetaData, modelAccessor);
        }

        public override ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType)
        {
            return new ExtendedModelMetadata(this, null, modelAccessor, modelType, null, null);
        }

        private ModelMetadata CreateModelMetaData(Type containerType, string propertyName, Type propertyType, ModelMetadataItemBase propertyMetaData, Func<object> modelAccessor)
        {
            ModelMetadata modelMetaData = new ExtendedModelMetadata(this, containerType, modelAccessor, propertyType, propertyName, propertyMetaData)
                                              {
                                                  ShowForDisplay = propertyMetaData.ShowForDisplay,
                                              };

            if (!string.IsNullOrEmpty(propertyMetaData.DisplayName))
            {
                modelMetaData.DisplayName = propertyMetaData.DisplayName;
            }

            if (!string.IsNullOrEmpty(propertyMetaData.ShortDisplayName))
            {
                modelMetaData.ShortDisplayName = propertyMetaData.ShortDisplayName;
            }

            if (!string.IsNullOrEmpty(propertyMetaData.TemplateName))
            {
                modelMetaData.TemplateHint = propertyMetaData.TemplateName;
            }

            if (!string.IsNullOrEmpty(propertyMetaData.Description))
            {
                modelMetaData.Description = propertyMetaData.Description;
            }

            if (!string.IsNullOrEmpty(propertyMetaData.NullDisplayText))
            {
                modelMetaData.NullDisplayText = propertyMetaData.NullDisplayText;
            }

            if (!string.IsNullOrEmpty(propertyMetaData.Watermark))
            {
                modelMetaData.Watermark = propertyMetaData.Watermark;
            }

            if (propertyMetaData.HideSurroundingHtml.HasValue)
            {
                modelMetaData.HideSurroundingHtml = propertyMetaData.HideSurroundingHtml.Value;
            }

            if (propertyMetaData.IsReadOnly.HasValue)
            {
                modelMetaData.IsReadOnly = propertyMetaData.IsReadOnly.Value;
            }

            if (propertyMetaData.IsRequired.HasValue)
            {
                modelMetaData.IsRequired = propertyMetaData.IsRequired.Value;
            }

            if (propertyMetaData.ShowForEdit.HasValue)
            {
                modelMetaData.ShowForEdit = propertyMetaData.ShowForEdit.Value;
            }
            else
            {
                modelMetaData.ShowForEdit = !modelMetaData.IsReadOnly;
            }

            IModelMetadataFormattableItem formattableItem = propertyMetaData as IModelMetadataFormattableItem;

            if (formattableItem != null)
            {
                modelMetaData.DisplayFormatString = formattableItem.DisplayFormat;

                if (formattableItem.ApplyFormatInEditMode && modelMetaData.ShowForEdit)
                {
                    modelMetaData.EditFormatString = formattableItem.EditFormat;
                }
            }

            StringMetadataItem stringMetadataItem = propertyMetaData as StringMetadataItem;

            if (stringMetadataItem != null)
            {
                modelMetaData.ConvertEmptyStringToNull = stringMetadataItem.ConvertEmptyStringToNull;
            }

            return modelMetaData;
        }
    }
}