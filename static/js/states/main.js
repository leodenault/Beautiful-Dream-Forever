window.onload = function() {
	var game = new Phaser.Game(800, 450, Phaser.CANVAS, 'canvas', {/* preload: preload, create: create */});
	game.state.add("main-menu", new MainMenu(), true);
};
