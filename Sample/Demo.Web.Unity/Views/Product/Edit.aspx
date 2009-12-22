<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Demo.Web.Unity.ProductEditModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Edit</h2>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm()) {%>
        <fieldset>
            <legend>Fields</legend>
            <p>
                <%= Html.LabelFor(p => p.Name) %>
                <%= Html.TextBoxFor(p => p.Name) %>
                <%= Html.ValidationMessageFor(p => p.Name) %>
            </p>
            <p>
                <%= Html.LabelFor(p => p.Category) %>
                <%= Html.DropDownListFor(p => p.Category, Model.Categories, "[Select category]")%>
                <%= Html.ValidationMessageFor(p => p.Category) %>
            </p>
            <p>
                <%= Html.LabelFor(p => p.Supplier) %>
                <%= Html.DropDownListFor(p => p.Supplier, Model.Suppliers, "[Select supplier]")%>
                <%= Html.ValidationMessageFor(p => p.Supplier) %>
            </p>
            <p>
                <%= Html.LabelFor(p => p.Price) %>
                <%= Html.TextBoxFor(p => p.Price, string.Format("{0:c}", Model.Price)) %>
                <%= Html.ValidationMessageFor(p => p.Price) %>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>
    <% } %>
    <div>
        <%= Html.ActionLink("Back to List", "Index") %>
    </div>
</asp:Content>