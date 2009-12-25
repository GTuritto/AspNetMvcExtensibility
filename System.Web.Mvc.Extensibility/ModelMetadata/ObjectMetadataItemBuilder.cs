#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Linq;

    public class ObjectMetadataItemBuilder<TModel> : ModelMetadataItemBuilderBase<ObjectMetadataItem, ObjectMetadataItemBuilder<TModel>>
    {
        public ObjectMetadataItemBuilder(ObjectMetadataItem item) : base(item)
        {
        }

        public ObjectMetadataItemBuilder<TModel> DropDownList(string viewDataKey)
        {
            return DropDownList(viewDataKey, null);
        }

        public ObjectMetadataItemBuilder<TModel> DropDownList(string viewDataKey, string optionLabel)
        {
            return List("DropDownList", viewDataKey, null);
        }

        public ObjectMetadataItemBuilder<TModel> Select(string viewDataKey)
        {
            return Select(viewDataKey, null);
        }

        public ObjectMetadataItemBuilder<TModel> Select(string viewDataKey, string optionLabel)
        {
            return List("Select", viewDataKey, null);
        }

        protected virtual ObjectMetadataItemBuilder<TModel> List(string templateName, string viewDataKey, string optionLabel)
        {
            ModelMetadataItemSelectableElementSetting selectableElementSetting = Item.AdditionalSettings
                                                                                     .OfType<ModelMetadataItemSelectableElementSetting>()
                                                                                     .FirstOrDefault();

            if (selectableElementSetting == null)
            {
                selectableElementSetting = new ModelMetadataItemSelectableElementSetting();
                Item.AdditionalSettings.Add(selectableElementSetting);
            }

            selectableElementSetting.ViewDataKey = viewDataKey;
            selectableElementSetting.OptionLabel = optionLabel;

            Item.TemplateName = templateName;

            return this;
        }
    }
}