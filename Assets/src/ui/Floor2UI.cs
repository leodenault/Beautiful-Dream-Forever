using UnityEngine;
using System.Collections;

public class Floor2UI : MonoBehaviour {

	GlobalController controller;

	public void Start() {
		controller = GlobalController.GetInstance();
	}              

	public void LoadHipster() {
		controller.Forward("Hipster Shop");
	}

    public void LoadAthletic() {
        controller.Forward("Athletic Shop");
    }

    public void LoadFloor3() {
        controller.Forward("Floor 3");
    }

    public void LoadFloor2() {
        controller.Forward("Floor 1");
    }
}
