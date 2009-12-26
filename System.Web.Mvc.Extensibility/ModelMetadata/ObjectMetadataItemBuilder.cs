#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using Linq;

    /// <summary>
    /// Defines a class to fluently configure metadata of a model.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class ObjectMetadataItemBuilder<TModel> : ModelMetadataItemBuilderBase<ObjectMetadataItem, ObjectMetadataItemBuilder<TModel>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectMetadataItemBuilder&lt;TModel&gt;"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public ObjectMetadataItemBuilder(ObjectMetadataItem item) : base(item)
        {
        }

        /// <summary>
        /// Marks the value to render as DropDownList element in edit mode.
        /// </summary>
        /// <param name="viewDataKey">The view data key, the value of the view data key must be a <seealso cref="IEnumerable{SelectListItem}"/>.</param>
        /// <returns></returns>
        public ObjectMetadataItemBuilder<TModel> AsDropDownList(string viewDataKey)
        {
            return AsDropDownList(viewDataKey, null);
        }

        /// <summary>
        /// Marks the value to render as DropDownList element in edit mode.
        /// </summary>
        /// <param name="viewDataKey">The view data key, the value of the view data key must be a <seealso cref="IEnumerable{SelectListItem}"/>.</param>
        /// <param name="optionLabel">The option label.</param>
        /// <returns></returns>
        public ObjectMetadataItemBuilder<TModel> AsDropDownList(string viewDataKey, string optionLabel)
        {
            return List("DropDownList", viewDataKey, optionLabel);
        }

        /// <summary>
        /// Marks the value to render as Select element in edit mode.
        /// </summary>
        /// <param name="viewDataKey">The view data key, the value of the view data key must be a <seealso cref="IEnumerable{SelectListItem}"/>.</param>
        /// <returns></returns>
        public ObjectMetadataItemBuilder<TModel> AsSelect(string viewDataKey)
        {
            return AsSelect(viewDataKey, null);
        }

        /// <summary>
        /// Marks the value to render as Select element in edit mode.
        /// </summary>
        /// <param name="viewDataKey">The view data key, the value of the view data key must be a <seealso cref="IEnumerable{SelectListItem}"/>.</param>
        /// <param name="optionLabel">The option label.</param>
        /// <returns></returns>
        public ObjectMetadataItemBuilder<TModel> AsSelect(string viewDataKey, string optionLabel)
        {
            return List("Select", viewDataKey, optionLabel);
        }

        /// <summary>
        /// Render the value using the specifed template.
        /// </summary>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="viewDataKey">The view data key, the value of the view data key must be a <seealso cref="IEnumerable{SelectListItem}"/>.</param>
        /// <param name="optionLabel">The option label.</param>
        /// <returns></returns>
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