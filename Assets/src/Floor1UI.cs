using UnityEngine;
using System.Collections;

public class Floor1UI : MonoBehaviour {

	TabMenuController controller;

	public void Start() {
		controller = TabMenuController.GetInstance();
	}              

	public void LoadPreppy() {
		controller.Forward("Preppy Shop");
	}

    public void LoadUniform() {
        controller.Forward("Uniform Shop");
    }

    public void LoadFloor() {
        controller.Forward("Floor 2");
    }
}
