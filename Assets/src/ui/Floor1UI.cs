using UnityEngine;
using System.Collections;

public class Floor1UI : FloorUI {

	public void LoadPreppy() {
		controller.Forward("Preppy Shop");
	}

    public void LoadUniform() {
        controller.Forward("Uniform Shop");
    }
}
