using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class ClothingSystem : MonoBehaviour {
	private static float PREVIEW_WIDTH = 74.0f;
	private static float PREVIEW_HEIGHT = 104.0f;

	private bool isEquipped;
	private ClothingSystemController controller;
	private ClothingSelection activeTile;
	private	ClothingArea clothingArea;
	private ClothingSlotSystem clothingSlotSystem;

	public ClothingData.ClothingStyle shopStyle;

	public Button equipButton;
	public Sprite equipImage;
	public Sprite unequipImage;
	public Image preview;
	public GameObject clothingAreaContainer;
	public GameObject pageTilePanel;
	public GameObject itemSlotsPanel;

	public void Start()
	{
		isEquipped = false;
		controller = ClothingSystemController.GetInstance();
		clothingArea = clothingAreaContainer.GetComponentInChildren<ClothingArea>();
		clothingSlotSystem = itemSlotsPanel.GetComponentInChildren<ClothingSlotSystem>();
		Button[] pageTiles = pageTilePanel.GetComponentsInChildren<Button>();
		controller.AssignClothingBackgrounds(shopStyle, pageTiles);

		activeTile = pageTiles[0].GetComponentInChildren<ClothingSelection>();
		// Add the button click listeners for the page tiles
		foreach (Button button in pageTiles) {
			ClothingSelection pageTile = button.GetComponentInChildren<ClothingSelection>();
			button.onClick.AddListener(() => { selectClothing(pageTile); });
		}

		clothingSlotSystem.Init(clothingArea, selectSlotCallback);
	}

	public void Equip() {
		if (activeTile.Clothing != null) {
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

	private void selectClothing(ClothingSelection pageTile) {
		if (pageTile.Clothing != null) {
			activeTile = pageTile;
			displayPreview(activeTile.Sprite);

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
		if (activeTile.Clothing != null) {
			clothingSlotSystem.UpdateActiveSlot(activeTile.Clothing);
		}
		setEquip(false);
	}

	private void unequipClothing() {
		Sprite activeSprite = clothingSlotSystem.UnsetActiveSlot();
		setEquip(activeSprite == null);

		if (activeSprite == null) {
			activeSprite = activeTile.Sprite;
		}
		displayPreview(activeSprite);
	}
}
