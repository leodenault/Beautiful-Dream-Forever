using NUnit.Framework;

[TestFixture]
public class StateTrackerTest {
	
	private StateTracker tracker;
	
	[SetUp]
	public void SetUp() {
		this.tracker = new StateTracker();
	}

	[Test]
	public void BackReturnsEmptyWhenEmpty() {
		Assert.AreEqual(StateTracker.EMPTY, this.tracker.Back());
	}
	
	[Test]
	public void BackReturnsAStateWhenNotEmpty() {
		this.tracker.Forward("state1");
		this.tracker.Forward("state2");
		this.tracker.Back();
		Assert.AreNotEqual(StateTracker.EMPTY, this.tracker.Back());
	}
	
	[Test]
	public void ForwardAddsStateToStack() {
		string state = "next-state";
		this.tracker.Forward(state);
		Assert.AreEqual(state, this.tracker.Back());
	}
	
	[Test]
	public void ForwardAddsStateToStackAndRemovesFirstStateIfOverMax() {
		string state = "next-state";
		for (int i = 0; i < 251; i++) {
			this.tracker.Forward(state + i.ToString());
		}
	
		string lastState = StateTracker.EMPTY;
		string current = StateTracker.EMPTY;
		while ((current = this.tracker.Back()) != StateTracker.EMPTY) {
			lastState = current;
		}
		Assert.AreNotEqual(StateTracker.EMPTY, lastState);
		Assert.AreEqual(state + "1", lastState);
	}
}
