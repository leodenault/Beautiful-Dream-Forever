using UnityEngine;
using System.Collections;

public class TabMenuController {

	private static TabMenuController INSTANCE;

	private StateTracker tracker;
	
	private TabMenuController() {
		this.tracker = new StateTracker();
	}
	
	public static TabMenuController GetInstance() {
		if (INSTANCE == null) {
			INSTANCE = new TabMenuController();
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
