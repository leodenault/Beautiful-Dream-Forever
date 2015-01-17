var assert = buster.referee.assert;
var refute = buster.referee.refute;
var CoreController = DUB.controller.CoreController;
var StateTracker = DUB.model.StateTracker;
var GameWrapper = DUB.model.GameWrapper;

var Phaser = { StateManager: {} };

buster.testCase("Core Controller tests", {
	setUp: function () {
		this.wrapper = {
			game: {
				state: {
					start: this.spy()
				}
			},
			stateTracker: {
				forward: this.spy(),
				back: this.stub()
			}
		};
		this.controller = new CoreController(this.wrapper);
	},

	"forward throws error if state nonexistent": function () {
		var controller = this.controller;
		assert.exception(function () {
			controller.forward("previous-state", "nonexistent-state");
		}, { name: "Error",
			message: "Cannot move forward to nonexistent state" });
	},
	
	"forward sets game state to given state": function () {
		this.controller.forward("previous-state", GameWrapper.STATES.WARDROBE);
		assert.calledOnce(this.wrapper.game.state.start);
		assert.calledOnce(this.wrapper.stateTracker.forward);
	},
	
	"forward does nothing when already on state": function () {
		this.controller.forward("previous-state", GameWrapper.STATES.WARDROBE);
		this.controller.forward(GameWrapper.STATES.WARDROBE, GameWrapper.STATES.WARDROBE);
		assert.calledOnce(this.wrapper.game.state.start);
		assert.calledOnce(this.wrapper.stateTracker.forward);
	},
	
	"back does nothing if receive empty from tracker": function () {
		this.wrapper.stateTracker.back.returns(StateTracker.EMPTY);
		this.controller.back();
		refute.called(this.wrapper.game.state.start);
	},
	
	"back sets game state to tracked state": function () {
		this.wrapper.stateTracker.back.returns("some-state");
		this.controller.back();
		assert.called(this.wrapper.game.state.start);
	}
});
