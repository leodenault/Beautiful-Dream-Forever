using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ClothingSystemController {
	private ClothingManager manager;
	private WardrobePaginator<ClothingData> paginator;

	public ClothingSystemController(ClothingData.ClothingStyle style, int pageSize) {
		this.manager = ClothingManager.GetInstance();
		paginator = new WardrobePaginator<ClothingData>(manager.GetClothingData(style), pageSize);
	}

    public void CurrentPage(Button[] wardrobeButtons) {
		fillButtons(wardrobeButtons, paginator.Current());
    }

	public void PreviousPage(Button[] wardrobeButtons) {
		fillButtons(wardrobeButtons, paginator.Previous());
	}

	public void NextPage(Button[] wardrobeButtons) {
		fillButtons(wardrobeButtons, paginator.Next());
	}

	private void fillButtons(Button[] wardrobeButtons, ClothingData[] set) {
		int i = 0;
		for (; i < set.Length && i < wardrobeButtons.Length; i++) {
			setButton(wardrobeButtons[i], set[i]);
		}

		// If the set was smaller than there are buttons on screen, empty out the buttons
		for (; i < wardrobeButtons.Length; i++) {
			setButton(wardrobeButtons[i], null);
		}
	}

	private void setButton(Button button, ClothingData data) {
		ClothingSelection pageTile = button.GetComponentInChildren<ClothingSelection>();
		pageTile.Clothing = data;
	}
}
