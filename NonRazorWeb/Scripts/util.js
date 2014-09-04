function playSound(name) {
	var extension;
	if (name === "") {
	    return;
	}
	if ((new Audio()).canPlayType("audio/ogg; codecs=vorbis")) {
		extension = "ogg";
	} else if ((new Audio()).canPlayType("audio/mp3; codecs=vorbis")) {
		extension = "mp3";
	}
	if (extension) {
		var sound = new Audio("/Content/" + name + "." + extension);
		sound.play();
	}
}