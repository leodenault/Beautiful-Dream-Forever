var DUB = DUB || {};
DUB.model = DUB.model || {};

(function (ns) {
	var WIDTH = 800;
	var HEIGHT = 450;
		
	ns.game;

	ns.STATES = {
		MAIN_MENU:	"main-menu",
		WARDROBE:	"wardrobe"
	};

	window.onload = function() {
		ns.game = new Phaser.Game(WIDTH, HEIGHT, Phaser.CANVAS, 'canvas', {});
		ns.game.state.add(ns.STATES.MAIN_MENU, new ns.states.MainMenu(), true);
		ns.game.state.add(ns.STATES.WARDROBE, new ns.states.Wardrobe());
	};
}(DUB.model));
