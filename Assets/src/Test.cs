using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
	private TabMenuController controller;
	
	public void Start() {
		controller = TabMenuController.GetInstance();
	}
	
	public void ChangeToTest2() {
		controller.Forward("test2");
	}
	
	public void Back() {
		controller.Back();
	}
}
