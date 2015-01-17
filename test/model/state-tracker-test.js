var assert = buster.referee.assert;
var refute = buster.referee.refute;
var StateTracker = DUB.model.StateTracker;
var CoreController = DUB.controller.CoreController;

buster.testCase("State Tracker tests", {
	setUp: function () {
		this.tracker = new StateTracker();
	},

	"back returns StateTracker.EMPTY when empty": function () {
		assert.equals(StateTracker.EMPTY, this.tracker.back());
	},
	
	"back returns a state when not empty": function () {
		this.tracker.forward("state1");
		this.tracker.forward("state2");
		this.tracker.back();
		refute.equals(StateTracker.EMPTY, this.tracker.back());
	},
	
	"forward adds state to stack": function() {
		var state = "next-state";
		this.tracker.forward(state);
		assert.equals(state, this.tracker.back());
	},
	
	"forward adds state to stack and removes first state if gone forward over max": function() {
		var state = "next-state";
		for (var i = 0; i < 251; i++) {
			this.tracker.forward(state + i.toString());
		}
		
		var lastState, current = this.tracker.EMPTY;
		while ((current = this.tracker.back()) != StateTracker.EMPTY) {
			lastState = current;
		}
		
		refute.equals(StateTracker.EMPTY, lastState);
		assert.equals(state + (1).toString(), lastState);
	},
	
	"forward throws error if not given string": function() {
		var tracker = this.tracker;
		assert.exception(function () {
			tracker.forward(123);
		}, { name: "TypeError",
			message: "State Tracker only accepts strings"} );
	},
});
