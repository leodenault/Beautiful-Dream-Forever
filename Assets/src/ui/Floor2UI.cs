using UnityEngine;
using System.Collections;

public class Floor2UI : FloorUI {

	public void LoadHipster() {
		controller.Forward("Hipster Shop");
	}

    public void LoadAthletic() {
        controller.Forward("Athletic Shop");
    }

    public void LoadFloor2() {
        controller.Forward("Floor 1");
    }
}
