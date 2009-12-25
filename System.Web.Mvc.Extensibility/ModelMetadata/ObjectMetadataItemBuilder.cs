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

        public virtual ObjectMetadataItemBuilder<TModel> DropDownList(string viewDataKey, string optionLabel)
        {
            ModelMetadataItemDropDownListSetting dropDownListSetting = Item.AdditionalSettings
                                                                           .OfType<ModelMetadataItemDropDownListSetting>()
                                                                           .FirstOrDefault();

            if (dropDownListSetting == null)
            {
                dropDownListSetting = new ModelMetadataItemDropDownListSetting();
                Item.AdditionalSettings.Add(dropDownListSetting);
            }

            dropDownListSetting.SelectListViewDataKey = viewDataKey;
            dropDownListSetting.OptionLabel = optionLabel;

            Item.TemplateName = "DropDownList";

            return this;
        }

        // The Current methods are commented out as there is no way to support strongly typed version with
        // asp.net mvc implementation Check http://aspnet.codeplex.com/WorkItem/View.aspx?WorkItemId=5101
        // for the details.

        //public ObjectMetadataItemBuilder<TModel> DropDownList(Expression<Func<TModel, SelectList>> selectList)
        //{
        //    return DropDownList(selectList, null);
        //}

        //public virtual ObjectMetadataItemBuilder<TModel> DropDownList(Expression<Func<TModel, SelectList>> selectList, string optionLabel)
        //{
        //    ModelMetadataItemDropDownListSetting dropDownListSetting = Item.AdditionalSettings
        //                                                                   .OfType<ModelMetadataItemDropDownListSetting>()
        //                                                                   .FirstOrDefault();

        //    if (dropDownListSetting == null)
        //    {
        //        dropDownListSetting = new ModelMetadataItemDropDownListSetting();
        //        Item.AdditionalSettings.Add(dropDownListSetting);
        //    }

        //    dropDownListSetting.SelectListViewDataKey = ExpressionHelper.GetExpressionText(selectList);
        //    dropDownListSetting.OptionLabel = optionLabel;

        //    Item.TemplateName = "DropDownList";

        //    return this;
        //}
    }
}