<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.Web.Mvc.Extensibility"%>
<script runat="server">
    string GetSelectedText()
    {
        ExtendedModelMetadata metadata = ViewData.ModelMetadata as ExtendedModelMetadata;

        if (metadata != null)
        {
            ModelMetadataItemDropDownListSetting setting = metadata.Metadata
                                                                   .AdditionalSettings
                                                                   .OfType<ModelMetadataItemDropDownListSetting>()
                                                                   .SingleOrDefault();

            if (setting != null)
            {
                SelectList selectList = ViewData.Eval(setting.SelectListViewDataKey) as SelectList;

                if ((selectList != null) && selectList.Any())
                {
                    return selectList.Select(item => item.Selected).FirstOrDefault();
                }
            }
        }

        return null;
    }
</script>
<% string selectedText = GetSelectedText(); %>
<% if (selectedText != null) {%>
    <%= Html.Encode(selectedText) %>
<% }%>
<% else {%>
    <%= Html.DisplayForModel()%>
<% }%>