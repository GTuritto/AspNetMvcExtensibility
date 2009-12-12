<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Demo.Web.StructureMap.ProductDisplayModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Details</h2>
    <fieldset>
        <legend>Fields</legend>
        <p>
            Name:
            <%= Html.Encode(Model.Name) %>
        </p>
        <p>
            Category:
            <%= Html.Encode(Model.CategoryName) %>
        </p>
        <p>
            Supplier:
            <%= Html.Encode(Model.SupplierName) %>
        </p>
        <p>
            Price:
            <%= Html.Encode(String.Format("{0:c}", Model.Price)) %>
        </p>
    </fieldset>
    <p>
        <%= Html.ActionLink("Edit", "Edit", new { id = Model.Id }) %> |
        <%= Html.ActionLink("Delete", "Delete", new { id = Model.Id })%> |
        <%= Html.ActionLink("Back to List", "Index") %>
    </p>
</asp:Content>