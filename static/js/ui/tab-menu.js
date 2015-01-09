$(document).ready(function () {
	var mainMenu = $("#tab-menu");
	mainMenu.mouseenter(function () {
		$("#tab-menu > div").animate({bottom: '0px'}, 250);
	});
	mainMenu.mouseleave(function () {
		$("#tab-menu > div").animate({bottom: '-68px'}, 250);
	});
	
	$("#wardrobe").click(function (event) {
		if (DUB.model.game.state.current != DUB.model.STATES.WARDROBE) {
			DUB.model.game.state.start(DUB.model.STATES.WARDROBE);
		}
	});
	
	$("#options").click(function (event) {
		$("#outer-options-menu").fadeIn(250);
	});
	
	$("#back").click(function (event) {
		DUB.controller.back();
	});
});

