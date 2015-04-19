using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

// TODO: Split this class into a base class with subclasses for wardrobe and
// dressing room variations
public class ClothingSystem : MonoBehaviour {
	private static float PREVIEW_WIDTH = 74.0f;
	private static float PREVIEW_HEIGHT = 104.0f;

	private bool isEquipped;
	private Sprite buySprite;
	private ClothingSystemController controller;
	private ClothingRepresentation selected;
	private	ClothingArea clothingArea;
	private ClothingSlotSystem clothingSlotSystem;
	private Button[] pageTiles;

	public ClothingData.ClothingStyle shopStyle;
	public Button equipButton;
	public Button battleButton;
	public Button buyButton;
	public Sprite equipImage;
	public Sprite unequipImage;
	public Image preview;
	public GameObject clothingAreaContainer;
	public GameObject pageTilePanel;
	public GameObject itemSlotsPanel;
	public BattleModal battleModal;
	public BuyModal buyModal;
	public MoneyCount moneyCount;
	public Text priceText;

	public void Start()
	{
		isEquipped = false;
		clothingArea = clothingAreaContainer.GetComponentInChildren<ClothingArea>();
		clothingSlotSystem = itemSlotsPanel.GetComponentInChildren<ClothingSlotSystem>();
		pageTiles = pageTilePanel.GetComponentsInChildren<Button>();
		controller = new ClothingSystemController(shopStyle, pageTiles.Length);
		controller.CurrentPage(pageTiles);

		if (buyButton != null) {
			buySprite = buyButton.image.sprite;
		}

		selected = new ClothingRepresentation();
		updateSelected(pageTiles[0].GetComponentInChildren<ClothingSelection>());
		// Add the button click listeners for the page tiles
		foreach (Button button in pageTiles) {
			ClothingSelection pageTile = button.GetComponentInChildren<ClothingSelection>();
			button.onClick.AddListener(() => { selectClothing(pageTile); });
		}

		clothingSlotSystem.Init(clothingArea, selectSlotCallback);
		updateBattleButtonStatus();
	}

	public void Equip() {
		if (selected.Clothing != null) {
			if (isEquipped)
			{
				unequipClothing();
			}
			else
			{
				equipClothing();
			}
		}
	}

	public void PreviousPage() {
		controller.PreviousPage(pageTiles);
	}

	public void NextPage() {
		controller.NextPage(pageTiles);
	}

	public void Buy() {
		buyModal.Controller = controller;
		buyModal.Show(selected, updateSystemWithNewPurchase);
	}

	private void selectClothing(ClothingSelection pageTile) {
		if (pageTile.Clothing != null) {
			updateSelected(pageTile);

			if (clothingSlotSystem.MakeActive(pageTile.Clothing)) {
				setEquip(false);
			} else {
				setEquip(true);
			}
		}
	}

	private void selectSlotCallback(ClothingSelection activeSelection) {
		displayPreview(activeSelection.Sprite);
		setEquip(false);
	}

	private void setEquip(bool equipping) {
		if (equipping) {
			equipButton.image.sprite = equipImage;
			isEquipped = false;
		} else {
			equipButton.image.sprite = unequipImage;
			isEquipped = true;
		}
	}

	private void displayPreview(Sprite sprite) {
		if (sprite != null) {
			Util.ScaleImageToMaxDimensions(preview, sprite, PREVIEW_WIDTH, PREVIEW_HEIGHT);
			preview.gameObject.SetActive(true);
		}
	}

	private void equipClothing() {
		if (selected.Clothing != null) {
			clothingSlotSystem.UpdateActiveSlot(selected.Clothing);
		}
		setEquip(false);
	}

	private void unequipClothing() {
		ClothingSelection activeSelection = clothingSlotSystem.UnsetActiveSlot();
		setEquip(activeSelection == null);

		if (activeSelection == null) {
			displayPreview(selected.Sprite);
		} else {
			updateSelected(activeSelection);
		}
	}

	private void updateSelected(ClothingSelection selection) {
		selected.Clothing = selection.Clothing;
		updateBuyButtonStatus(selected);
		displayPreview(selected.Sprite);

		// TODO: SERIOUSLY NEED to refactor this class!!!
		if (priceText != null) {
			priceText.text = selection.Clothing.Price.ToString();
		}
	}

	private void updateBattleButtonStatus() {
		if (controller.AllItemsAreOwned()) {
			if (battleButton != null) {
				battleButton.image.sprite = controller.DisabledBattleButton();
				battleButton.interactable = false;
			}
		}
	}

	private void updateBuyButtonStatus(ClothingRepresentation item) {
		if (buyButton != null) {
			if (controller.IsOwned(item.Clothing)) {
				buyButton.image.sprite = controller.DisabledBuyButton();
				buyButton.interactable = false;
			} else {
				buyButton.image.sprite = buySprite;
				buyButton.interactable = true;
			}
		}
	}

	private void updateSystemWithNewPurchase() {
		updateBattleButtonStatus();
		updateBuyButtonStatus(selected);
		battleModal.SetupPrizes();
		moneyCount.UpdateFunds();
	}
}
