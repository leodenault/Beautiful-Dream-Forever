using UnityEngine;

class DressingRoom : MonoBehaviour {
	private GlobalController controller;

	public ClothingData.ClothingStyle shopStyle;

	public void Start() {
		controller = GlobalController.GetInstance();
	}

	public void LoadBattle() {
		ShopController.GetInstance().ShopStyle = shopStyle;
		controller.Forward("Battle Screen");
	}
}
