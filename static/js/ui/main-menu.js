$(document).ready(function () {
	var mainMenu = $("#main-menu");
	mainMenu.mouseenter(function () {
		$("#main-menu > div").animate({bottom: '0px'});
	});
	mainMenu.mouseleave(function () {
		$("#main-menu > div").animate({bottom: '-68px'});
	});
});

