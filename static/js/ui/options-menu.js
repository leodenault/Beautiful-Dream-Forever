$(document).ready(function () {
	var outerOptionsMenu = $("#outer-options-menu");
	outerOptionsMenu.hide();

	$("#exit").click(function (event) {
		outerOptionsMenu.fadeOut(250);
	});
	
	$("#music").click(function (event) {
		$("#music").toggleClass("music-off");
	});
});
