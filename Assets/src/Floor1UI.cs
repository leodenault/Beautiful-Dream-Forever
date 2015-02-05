using UnityEngine;
using System.Collections;

public class Floor1UI : MonoBehaviour {

	TabMenuController controller;

	public void Start() {
		controller = TabMenuController.GetInstance();
	}              

	public void LoadFormal() {
		controller.Forward("Formal Shop");
	}
}
