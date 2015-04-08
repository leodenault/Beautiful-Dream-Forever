using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ClothingSystemController {
	private static string DISABLED_BATTLE_BUTTON_FILE = "buttons/Battle Button GREY";

	private ClothingManager manager;
	private WardrobePaginator<ClothingData> paginator;
	private ClothingData.ClothingStyle style;

	public ClothingSystemController(ClothingData.ClothingStyle style, int pageSize) {
		this.style = style;
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

	// TODO: This could be optimized for way better performance
	public bool AllItemsAreOwned() {
		Inventory inventory = Protagonist.GetInstance().Inventory;
		foreach (ClothingData item in manager.GetClothingData(style)) {
			if (!inventory.Contains(item)) {
				return false;
			}
		}
		return true;
	}

	public Sprite DisabledBattleButton() {
		return Resources.Load<Sprite>(DISABLED_BATTLE_BUTTON_FILE);
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
