var DUB = DUB || {};

window.onload = function() {
	var gameWrapper = new DUB.model.GameWrapper();
	gameWrapper.start();
	DUB.coreController = new DUB.controller.CoreController(gameWrapper);
	DUB.gameWrapper = gameWrapper;
};
