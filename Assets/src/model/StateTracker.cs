using UnityEngine;
using System.Collections.Generic;

public class StateTracker {
	private const int MAX_STATES = 250;
	private List<string> states;
	
	public static string EMPTY = "EMPTY";
	
	public StateTracker() {
		this.states = new List<string>();
	}
	
	public void Forward(string state) {
		states.Add(state);
		if (states.Count > 250) {
			this.states.RemoveAt(0);
		}
	}
			
	public string Back() {
		if (states.Count > 0) {
			int lastIndex = states.Count - 1;
			string state = states[lastIndex];
			states.RemoveAt(lastIndex);
			return state;
		} else {
			return EMPTY;
		}
	}
}
