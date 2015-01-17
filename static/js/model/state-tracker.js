var DUB = DUB || {};
DUB.model = DUB.model || {};

(function (ns) {
	var EMPTY = -1;

	ns.StateTracker = function () {
		var MAX_STATES = 250;
		var stateStack = [];
		
		return {
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
	};
	
	ns.StateTracker.EMPTY = EMPTY;
}(DUB.model));

