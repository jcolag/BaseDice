function playMusic() {
	//SC.initialize({
	//	client_id: 'YOUR_APP_ID'
	//});
	//var track_url = 'https://soundcloud.com/madebyrobot/ucreate-music-sample-track';
	//SC.oEmbed(track_url, { auto_play: true }, function(oEmbed) {
	//	console.log('oEmbed response: ' + oEmbed);
	//});
}
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
	var URL = "/Home/Score/";
	for(i = 1; i <= 9; i++) {
		$.get(URL + i, function(data) {
		    var parts = data.split(",");
	        var inning = parts[0];
	        var score = parts[1];
	        if (score == -1) {
	            score = "&mdash;";
	        }
		    $("#inning_" + inning).html(score);
		});
	}
}
