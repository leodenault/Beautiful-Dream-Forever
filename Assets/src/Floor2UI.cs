using UnityEngine;
using System.Collections;

public class Floor2UI : MonoBehaviour {

	TabMenuController controller;

	public void Start() {
		controller = TabMenuController.GetInstance();
	}              

	public void LoadHipster() {
		controller.Forward("Hipster Shop");
	}

    public void LoadAthletic() {
        controller.Forward("Athletic Shop");
    }

    public void LoadFloor() {
        controller.Forward("Floor 3");
    }
}
