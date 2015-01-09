var DUB = DUB || {};
DUB.controller = DUB.controller || {};

(function (ns) {
	ns.back = function() {
		if (DUB.model.game.state.current != DUB.model.STATES.MAIN_MENU) {
			DUB.model.game.state.start(DUB.model.STATES.MAIN_MENU); // TODO: Add actual back logic here
		}
	}
}(DUB.controller));
