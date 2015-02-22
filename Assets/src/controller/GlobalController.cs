using UnityEngine;
using System.Collections;

public class GlobalController {

	private static GlobalController INSTANCE;

	private StateTracker tracker;
	
	private GlobalController() {
		this.tracker = new StateTracker();
	}
	
	public static GlobalController GetInstance() {
		if (INSTANCE == null) {
			INSTANCE = new GlobalController();
		}
		
		return INSTANCE;
	}
	
	public void Forward(string state) {
		if (!Application.loadedLevelName.Equals(state)) {
			tracker.Forward(Application.loadedLevelName);
			Application.LoadLevel(state);
		}
	}
	
	public void Back() {
		string state = tracker.Back();
		if (!state.Equals(StateTracker.EMPTY)) {
			Application.LoadLevel(state);
		}
	}
}
