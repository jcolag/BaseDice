<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/BaseDice.master" %>
<asp:Content ContentPlaceHolderID="MainContent" ID="MainContentContent" runat="server">
	<%= ViewData["Message"] %>
	<form action="" method="post" enctype="multipart/form-data">
		<label for="file">Player File:</label>
		<br>
		&nbsp;&nbsp;<input type="file" name="file" id="file" />
		<br><br>
		<input type="submit" value="Start the Game" />
	</form>
</asp:Content>