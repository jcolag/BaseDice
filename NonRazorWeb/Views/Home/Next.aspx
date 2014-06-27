<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/BaseDice.master" %>
<asp:Content ContentPlaceHolderID="MainContent" ID="MainContentContent" runat="server">
	<div id="current">
	    <%= ViewData["Message"] %>
	</div>
	<%=Html.ActionLink("Next", "Next") %>
	<hr>
	<div id="history">
		<%= ViewData["History"] %>
	</div>
</asp:Content>