var game;

window.onload = function() {
	game = new Phaser.Game(800, 450, Phaser.CANVAS, 'canvas', {});
	game.state.add("main-menu", new DUB.states.MainMenu(), true);
	game.state.add("wardrobe", new DUB.states.Wardrobe());
};
