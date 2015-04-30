using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ClothingSystemController {
	private static string DISABLED_BATTLE_BUTTON_FILE = "buttons/Battle Button GREY";
	private static string DISABLED_BUY_BUTTON_FILE = "buttons/Buy Button GREY";

	private ClothingManager manager;
	private Protagonist protagonist;
	private WardrobePaginator<ClothingData> paginator;
	private ClothingData.ClothingStyle style;
	private PrizeController prizeController;

	public ClothingSystemController(ClothingData.ClothingStyle style, int pageSize) {
		this.style = style;
		this.manager = ClothingManager.GetInstance();
		protagonist = Protagonist.GetInstance();
		paginator = new WardrobePaginator<ClothingData>(manager.GetClothingData(style), pageSize);
		prizeController = PrizeController.GetInstance();
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

	public bool IsOwned(ClothingData item) {
		return prizeController.IsOwned(item, style);
	}

	public bool AllItemsAreOwned() {
		return prizeController.AllItemsAreOwned(style);
	}

	public Sprite DisabledBattleButton() {
		return Resources.Load<Sprite>(DISABLED_BATTLE_BUTTON_FILE);
	}

	public Sprite DisabledBuyButton() {
		return Resources.Load<Sprite>(DISABLED_BUY_BUTTON_FILE);
	}

	public int GetBalance() {
		return protagonist.Balance;
	}

	public bool Purchasable(ClothingData item) {
		return protagonist.CanPurchase(item.Price);
	}

	public bool Buy(ClothingData item) {
		if (!protagonist.modifyBalance(-item.Price)) {
			return false;
		} else {
			prizeController.Buy(item);
			return true;
		}
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
