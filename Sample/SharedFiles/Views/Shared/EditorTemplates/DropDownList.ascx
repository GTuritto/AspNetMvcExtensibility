<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.Web.Mvc.Extensibility"%>
<script runat="server">
    ModelMetadataItemDropDownListSetting GetDropDownListSetting()
    {
        ExtendedModelMetadata metadata = ViewData.ModelMetadata as ExtendedModelMetadata;

        return (metadata != null) ? metadata.Metadata.AdditionalSettings
                                                     .OfType<ModelMetadataItemDropDownListSetting>()
                                                     .SingleOrDefault() : null;
    }
</script>
<% ModelMetadataItemDropDownListSetting setting = GetDropDownListSetting();%>
<% if (setting != null) {%>
    <%= Html.DropDownList(null, ViewData[setting.SelectListViewDataKey] as SelectList, setting.OptionLabel) %>
<% }%>
<% else {%>
    <%= Html.DisplayForModel()%>
<% }%>