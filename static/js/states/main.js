window.onload = function() {
	var game = new Phaser.Game(800, 450, Phaser.CANVAS, 'canvas', {});
	game.state.add("main-menu", new DUB.states.MainMenu(), true);
};
