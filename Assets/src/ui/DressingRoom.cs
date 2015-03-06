using UnityEngine;

class DressingRoom : MonoBehaviour {
	private GlobalController controller;

	public void Start() {
		controller = GlobalController.GetInstance();
	}

	public void LoadBattle() {
		controller.Forward("Battle Screen");
	}
}
