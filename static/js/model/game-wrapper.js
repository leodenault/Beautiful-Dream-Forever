var DUB = DUB || {};
DUB.model = DUB.model || {};

(function (ns) {
	var STATES = {
			MAIN_MENU:	"main-menu",
			WARDROBE:	"wardrobe"
	};
	
	ns.GameWrapper = function () {
		var WIDTH = 800;
		var HEIGHT = 450;

		return {
			game: null,
			stateTracker: null,

			start: function() {
				this.game = new Phaser.Game(WIDTH, HEIGHT, Phaser.CANVAS, 'canvas', {});
				this.game.state.add(STATES.MAIN_MENU, new ns.states.MainMenu(), true);
				this.game.state.add(STATES.WARDROBE, new ns.states.Wardrobe());
				this.stateTracker = new ns.StateTracker();
			}
		}
	} 

	ns.GameWrapper.STATES = STATES;
}(DUB.model));
