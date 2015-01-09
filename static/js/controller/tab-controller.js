var DUB = DUB || {};
DUB.controller = DUB.controller || {};

(function (ns) {
	ns.back = function() {
		game.state.start("main-menu"); // TODO: Add actual back logic here
	}
}(DUB.controller));
