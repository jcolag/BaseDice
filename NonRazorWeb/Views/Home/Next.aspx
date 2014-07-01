<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/BaseDice.master" %>
<asp:Content ContentPlaceHolderID="Scripts" ID="Scripts" runat="server">
    <script type="text/javascript">
	$(document).ready(function() {
		$("#btnSubmit").removeAttr("hidden");
		setBonuses();
	});
	function getNextRoll() {
	    var selected = getBonus();
		var URL = "/Home/Roll" + selected;
		$.get(URL, function(data) {
			$("#Result").html(data);
			var lines = data.split("<br>");
			$.each(lines, function(i, val) {
				data = data.replace("<br><br>", "<br>");
				data = data.replace("<br>\n<br>", "<br>");
			});
			$("#History").append(data + "<br>");
		});
		setBonuses();
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
	</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" ID="MainContentContent" runat="server">
	<div id="Result">
	    <%= ViewData["Message"] %>
	</div>
	<br>
	<label for="selBonuses">Available Bonuses</label>
	<select id="selBonuses" name="selBonuses"></select>
	<br>
	<input type="button" id="btnSubmit" value="Next" onclick="javascript:getNextRoll();" hidden="true" />
	<br>
	<noscript>
	<%=Html.ActionLink("Next", "Next") %>
	</noscript>
	<hr>
	<div id="History">
		<%= ViewData["History"] %>
	</div>
	<noscript>
	<%=Html.ActionLink("Next", "Next") %>
	</noscript>
</asp:Content>