var DUB = DUB || {};
DUB.controller = DUB.controller || {};

var GameWrapper = DUB.model.GameWrapper;

(function (ns) {

	ns.CoreController = function (gameWrapper) {
		var stateManager = gameWrapper.game.state;

		return {
			forward: (function () {
				var fns = [
					function (toState) {
						fns[1](stateManager.current, toState);
					},
					function (fromState, toState) {
						if (fromState !== toState) {
							for (var prop in GameWrapper.STATES) {
								if (GameWrapper.STATES[prop] === toState) {
									gameWrapper.stateTracker.forward(fromState);
									stateManager.start(toState);
									return;
								}
							}
							throw new Error(
								"Cannot move forward to nonexistent state");
						}
					}
				]
				
				function forward(fromState, toState) {
					var fnIndex;
					fnIndex = arguments.length;
					if (fnIndex > forward.length) {
						fnIndex = forward.length;
					}
					return fns[fnIndex - 1].call(this, fromState, toState);
				}
				return forward;
			}()),
	
			back: function () {
				var state = gameWrapper.stateTracker.back();
				if (state != DUB.model.StateTracker.EMPTY) {
					stateManager.start(state);
				}
			}
		}
	}
}(DUB.controller));

