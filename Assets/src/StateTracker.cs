using UnityEngine;
using System.Collections.Generic;

public class StateTracker {
	private const int MAX_STATES = 250;
	private Stack<string> stateStack;
	
	public static string EMPTY = "EMPTY";
	
	public StateTracker() {
		this.stateStack = new Stack<string>();
	}
	
	public void Forward(string state) {
		stateStack.Push(state);
		if (stateStack.Count > 250) {
			//stateStack.shift();
		}
	}
			
	public string Back() {
		return stateStack.Count > 0 ? stateStack.Pop() : EMPTY;
	}
}
