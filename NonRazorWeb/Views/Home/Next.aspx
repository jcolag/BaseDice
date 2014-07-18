<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/BaseDice.master" %>
<asp:Content ContentPlaceHolderID="Scripts" ID="Scripts" runat="server">
	<!--
	<script src="http://connect.soundcloud.com/sdk.js"></script>
	-->
    <script type="text/javascript">
	$(document).ready(function() {
		$("#btnSubmit").removeAttr("hidden");
		setBonuses();
		//SC.initialize({
		//	client_id: 'YOUR_APP_ID'
		//});
		//var track_url = 'https://soundcloud.com/madebyrobot/ucreate-music-sample-track';
		//SC.oEmbed(track_url, { auto_play: true }, function(oEmbed) {
		//	console.log('oEmbed response: ' + oEmbed);
		//});
	});
	function getNextRoll() {
	    var selected = getBonus();
		var URL = "/Home/Roll" + selected;
		$.get(URL, function(data) {
			$("#Result").html("<p>\n" + data + "\n</p>\n");
			var lines = data.split("<br>");
			$.each(lines, function(i, val) {
				data = data.replace("<br><br>", "<br>");
				data = data.replace("<br>\n<br>", "<br>");
			});
			$("#History").append(data + "<br>");
		});
		setBonuses();
		$('html, body').scrollTop($(document).height());
		var flash = "<div class='flash'></div>";
		$("#Result").prepend(flash);
		$('.flash').show().fadeOut('slow');
		setScore();
	}
	function setBonuses () {
		URL = "/Home/Bonuses/";
		$("select[id$=selBonuses] > option").remove();
		$.get(URL, function(data) {
		    var lines = data.split(";");
		    $.each(lines, function(i, val) {
		        $("#selBonuses").append($("<option></option>")
		            .attr("value", val.replace(/\s/g, ''))
		            .text(val)
		        );
		    });
		});
	}
	function getBonus () {
	    var selected = $("#selBonuses").val();
	    if (selected != null && selected.length > 1) {
	        selected = "?bonus=" + selected;
	    } else {
			selected = "";
	    }
	    return selected;
	}
	function setScore () {
		var URL = "/Home/Score";
		$.get(URL, function(data) {
		    var parts = data.split(",");
		    var inning = parts[0];
		    var score = parts[1];
		    $("#inning_" + inning).html(score);
		});
	}
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
				<td>Player</td>
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