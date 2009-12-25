<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.Web.Mvc.Extensibility"%>
<script runat="server">
    string GetSelectedText()
    {
        ExtendedModelMetadata metadata = ViewData.ModelMetadata as ExtendedModelMetadata;

        if (metadata != null)
        {
            ModelMetadataItemSelectableElementSetting setting = metadata.Metadata
                                                                        .AdditionalSettings
                                                                        .OfType<ModelMetadataItemSelectableElementSetting>()
                                                                        .SingleOrDefault();

            if (setting != null)
            {
                SelectList selectList = ViewData.Eval(setting.ViewDataKey) as SelectList;

                if ((selectList != null) && selectList.Any())
                {
                    return selectList.Where(item => item.Selected)
                                     .Select(item => item.Text)
                                     .FirstOrDefault();
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
    <%= Html.Encode(ViewData.TemplateInfo.FormattedModelValue) %>
<% }%>