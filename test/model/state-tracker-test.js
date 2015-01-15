var buster = require("buster");
var assert = buster.referee.assert;
var refute = buster.referee.refute;
var StateTracker = require("../../static/js/model/state-tracker");

buster.testCase("State Tracker tests", {
	"back returns StateTracker.EMPTY when empty": function () {
		assert.equals(StateTracker.EMPTY, StateTracker.back());
	},
	
	"back returns a state when not empty": function () {
		StateTracker.forward("state1");
		StateTracker.forward("state2");
		StateTracker.back();
		refute.equals(StateTracker.EMPTY, StateTracker.back());
	},
	
	"forward adds state to stack": function() {
		var state = "next-state";
		StateTracker.forward(state);
		assert.equals(state, StateTracker.back());
	},
	
	"forward adds state to stack and removes first state if gone forward over max": function() {
		var state = "next-state";
		for (var i = 0; i < 251; i++) {
			StateTracker.forward(state + i.toString());
		}
		
		var lastState, current = StateTracker.EMPTY;
		while ((current = StateTracker.back()) != StateTracker.EMPTY) {
			lastState = current;
		}
		
		refute.equals(StateTracker.EMPTY, lastState);
		assert.equals(state + (1).toString(), lastState);
	},
	
	"forward throws error if not given string": function() {
		assert.exception(function () {
			StateTracker.forward(123);
		}, { name: "TypeError",
			message: "State Tracker only accepts strings"} );
	},
});
