using UnityEngine;
using System.Collections;

public class Cheats : MonoBehaviour {
	private static CheatSequence fillInventory = new CheatSequence {
		KeyCode.UpArrow,
		KeyCode.UpArrow,
		KeyCode.DownArrow,
		KeyCode.DownArrow,
		KeyCode.LeftArrow,
		KeyCode.RightArrow,
		KeyCode.LeftArrow,
		KeyCode.RightArrow,
		KeyCode.B,
		KeyCode.A
	};

	public void Update() {
		if (Input.anyKeyDown) {
			if (fillInventory.Activate()) {
				Protagonist.GetInstance().Inventory.Items = ClothingManager.GetInstance().GetClothingData();
			}
		}
	}
}
