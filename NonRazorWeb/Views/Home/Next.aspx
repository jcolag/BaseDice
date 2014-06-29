<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/BaseDice.master" %>
<asp:Content ContentPlaceHolderID="Scripts" ID="Scripts" runat="server">
    <script type="text/javascript">
	function getNextRoll() {
		var URL = "/Home/Roll/" + $("#Location").val();
		$.get(URL, function(data) {
			$("#Result").html(data);
		});
	}
	</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" ID="MainContentContent" runat="server">
	<div id="Result">
	    <%= ViewData["Message"] %>
	</div>
	<input type="button" id="btnSubmit" value="Next" onclick="javascript:getNextRoll();" /> 
	<br>
	<%=Html.ActionLink("Next", "Next") %>
	<hr>
	<div id="History">
		<%= ViewData["History"] %>
	</div>
</asp:Content>