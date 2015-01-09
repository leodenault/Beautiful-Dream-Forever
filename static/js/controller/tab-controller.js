var DUB = DUB || {};
DUB.controller = DUB.controller || {};

(function (ns) {
	ns.back = function() {
		if (DUB.game.state.current != DUB.STATES.MAIN_MENU) {
			DUB.game.state.start(DUB.STATES.MAIN_MENU); // TODO: Add actual back logic here
		}
	}
}(DUB.controller));
