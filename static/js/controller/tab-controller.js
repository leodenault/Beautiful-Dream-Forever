var DUB = DUB || {};
DUB.controller = DUB.controller || {};

(function (ns) {
	ns.back = function() {
		if (game.state.current != "main-menu") {
			game.state.start("main-menu"); // TODO: Add actual back logic here
		}
	}
}(DUB.controller));
