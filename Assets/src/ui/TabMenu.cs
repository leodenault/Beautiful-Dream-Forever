using UnityEngine;
using System.Collections;

public class TabMenu : MonoBehaviour {
	private GlobalController controller;
	
	public void Start() {
		controller = GlobalController.GetInstance();
	}
	
	public void ChangeToWardrobe() {
		controller.Forward("Wardrobe");
	}
	
	public void Back() {
		controller.Back();
	}
}
