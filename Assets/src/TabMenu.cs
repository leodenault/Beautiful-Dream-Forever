using UnityEngine;
using System.Collections;

public class TabMenu : MonoBehaviour {
	private TabMenuController controller;
	
	public void Start() {
		controller = TabMenuController.GetInstance();
	}
	
	public void ChangeToWardrobe() {
		controller.Forward("Wardrobe");
	}
	
	public void Back() {
		controller.Back();
	}
}
