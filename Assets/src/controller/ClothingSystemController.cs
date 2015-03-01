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

    public void AssignClothingBackgrounds(ClothingData.ClothingStyle style, Button[] wardrobeButtons) {
        ClothingData[] clothingData = manager.GetClothingData(style);

        for (int i = 0; i < clothingData.Length && i < wardrobeButtons.Length; i++) {
            ClothingSelection pageTile = wardrobeButtons[i].GetComponentInChildren<ClothingSelection>();
            pageTile.Clothing = clothingData[i];
        }
    }
}
