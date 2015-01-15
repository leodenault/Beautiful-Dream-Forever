var DUB = DUB || {};
DUB.model = DUB.model || {};

(function (ns) {
	ns.StateTracker = (function () {
		var MAX_STATES = 250;
		var EMPTY = -1;
		
		var stateStack = [];
		
		return {
			EMPTY: EMPTY,
			
			forward: function (state) {
				if (typeof state === "string") {
					stateStack.push(state);
					if (stateStack.length > 250) {
						stateStack.shift();
					}
				} else {
					throw new TypeError("State Tracker only accepts strings");
				}
			},
			
			back: function () {
				return stateStack.length > 0 ?
					stateStack.pop() : EMPTY;
			}
		};
		
	}());
}(DUB.model));

module.exports = DUB.model.StateTracker;
