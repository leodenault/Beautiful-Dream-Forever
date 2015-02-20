using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class WardrobeController {
	private static WardrobeController INSTANCE;

	private ClothingManager manager;

	private WardrobeController() {
		this.manager = new ClothingManager("data/clothing");
	}

	public static WardrobeController GetInstance() {
		if (INSTANCE == null) {
			INSTANCE = new WardrobeController();
		}

		return INSTANCE;
	}

	public void AssignClothingBackrounds(Button[] wardrobeButtons) {
		ClothingData[] clothingData = manager.GetClothingData();

		for (int i = 0; i < wardrobeButtons.Length && i < clothingData.Length; i++) {
			PageTile pageTile = wardrobeButtons[i].GetComponentInChildren<PageTile>();

			pageTile.SetClothing(clothingData[i]);
		}
	}
}
