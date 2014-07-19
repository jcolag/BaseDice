<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/BaseDice.master" %>
<asp:Content ContentPlaceHolderID="Scripts" ID="Scripts" runat="server">
	<script src="/Scripts/comm.js"></script>
	<!--
	<script src="http://connect.soundcloud.com/sdk.js"></script>
	-->
    <script type="text/javascript">
	$(document).ready(function() {
		$("#btnSubmit").removeAttr("hidden");
		setBonuses();
		playMusic();
	});
	</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" ID="MainContentContent" runat="server">
	<div id="Result">
	    <p>
	        <%= ViewData["Message"] %>
	    </p>
	</div>
	<div id="Controls">
		<br>
		<label for="selBonuses">Available Bonuses</label>
		<select id="selBonuses" name="selBonuses"></select>
		<br>
		<input type="button" id="btnSubmit" value="Next" onclick="javascript:getNextRoll();" hidden="true" />
		<br>
		<noscript>
		<%=Html.ActionLink("Next", "Next") %>
		</noscript>
		<br>
		<table id="ScoreBoard">
		    <tr>
		        <th>&nbsp;</th>
		        <th>1</th>
		        <th>2</th>
		        <th>3</th>
		        <th>4</th>
		        <th>5</th>
		        <th>6</th>
		        <th>7</th>
		        <th>8</th>
		        <th>9</th>
		    </tr>
		    <tr>
				<th>Player</th>
				<td id="inning_1">&mdash;</td>
				<td id="inning_2">&mdash;</td>
				<td id="inning_3">&mdash;</td>
				<td id="inning_4">&mdash;</td>
				<td id="inning_5">&mdash;</td>
				<td id="inning_6">&mdash;</td>
				<td id="inning_7">&mdash;</td>
				<td id="inning_8">&mdash;</td>
				<td id="inning_9">&mdash;</td>
		    </tr>
		</table>
	</div>
	<div id="History">
		<%= ViewData["History"] %>
	</div>
	<noscript>
	<%=Html.ActionLink("Next", "Next") %>
	</noscript>
</asp:Content>