$(document).ready(function () {
	var outerOptionsMenu = $("#outer-options-menu");
	outerOptionsMenu.hide();

	$("#options").click(function (event) {
		outerOptionsMenu.fadeIn(250);
	});
	
	$("#exit").click(function (event) {
		outerOptionsMenu.fadeOut(250);
	});
	
	$("#music").click(function (event) {
		$("#music").toggleClass("music-off");
	});
});
