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
		
		function createStates(game) {
			game.state.add(STATES.MAIN_MENU, new ns.states.MainMenu(), true);
			game.state.add(STATES.WARDROBE, new ns.states.Wardrobe());
		}

		return {
			game: null,
			stateTracker: null,

			start: function() {
				// Create the Phaser game
				this.game = new Phaser.Game(WIDTH, HEIGHT, Phaser.CANVAS, 'canvas', {});

				// Create the state tracker
				this.stateTracker = new ns.StateTracker();
				
				// Generate the game states
				createStates(this.game);
			}
		}
	} 

	ns.GameWrapper.STATES = STATES;
}(DUB.model));

