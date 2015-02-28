using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ClothingSystemController {
	private static ClothingSystemController INSTANCE;

	private ClothingManager manager;

	private ClothingSystemController() {
		this.manager = new ClothingManager("data/clothing");
	}

	public static ClothingSystemController GetInstance() {
		if (INSTANCE == null) {
			INSTANCE = new ClothingSystemController();
		}

		return INSTANCE;
	}

	public void AssignClothingBackrounds(Button[] wardrobeButtons) {
		ClothingData[] clothingData = manager.GetClothingData();

		for (int i = 0; i < wardrobeButtons.Length && i < clothingData.Length; i++) {
			ClothingSelection pageTile = wardrobeButtons[i].GetComponentInChildren<ClothingSelection>();

			pageTile.Clothing = clothingData[i];
		}
	}
}
