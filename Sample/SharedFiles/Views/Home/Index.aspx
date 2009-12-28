<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Html.Encode(ViewData["Message"]) %></h2>
    <ul>
        <li>IoC Containers rules everywhere.</li>
        <li><%= Html.ActionLink("ModelMetadata Provider", "Index", "Product")%></li>
        <li>Area Manager (Under Development).</li>
    </ul>
    <p>
        To learn more about ASP.NET MVC Extensibility visit <a href="http://weblogs.asp.net/rashid" title="ASP.NET MVC Extensibility Blog">http://weblogs.asp.net/rashid</a>.
    </p>
</asp:Content>