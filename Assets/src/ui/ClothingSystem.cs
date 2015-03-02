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
	private ClothingSelection activeSlot;
	private	ClothingArea clothingArea;
	private List<ClothingSelection> slotList;

	public ClothingData.ClothingStyle shopStyle;

	public Button equipButton;
	public Sprite equipImage;
	public Sprite unequipImage;
	public Image preview;
	public GameObject clothingAreaContainer;
	public GameObject pageTilePanel;
	public GameObject itemSlotsPanel;

	public ClothingSelection wigSlot;
	public ClothingSelection topSlot;
	public ClothingSelection bottomSlot;
	public ClothingSelection shoesSlot;
	public ClothingSelection accessorySlot;
	public ClothingSelection dressSlot;

	public void Start()
	{
		isEquipped = false;
		slotList = new List<ClothingSelection>();
		controller = ClothingSystemController.GetInstance();
		clothingArea = clothingAreaContainer.GetComponentInChildren<ClothingArea>();
		Button[] pageTiles = pageTilePanel.GetComponentsInChildren<Button>();
		Button[] clothingSlots = itemSlotsPanel.GetComponentsInChildren<Button>();
		controller.AssignClothingBackgrounds(shopStyle, pageTiles);

		activeTile = pageTiles[0].GetComponentInChildren<ClothingSelection>();
		// Add the button click listeners for the page tiles
		foreach (Button button in pageTiles) {
			ClothingSelection pageTile = button.GetComponentInChildren<ClothingSelection>();
			button.onClick.AddListener(() => { selectClothing(pageTile); });
		}

		// Add the button click listeners for the clothing slots
		foreach (Button button in clothingSlots)
		{
			ClothingSelection pageTile = button.GetComponentInChildren<ClothingSelection>();
			button.onClick.AddListener(() => { selectSlot(pageTile); });
		}

		slotList.Add(wigSlot);
		slotList.Add(topSlot);
		slotList.Add(bottomSlot);
		slotList.Add(shoesSlot);
		slotList.Add(accessorySlot);
		slotList.Add(dressSlot);
	}

	public void Equip() {
		if (activeTile.Clothing != null) {
			if (isEquipped)
			{
				unequipClothing();
				setEquip(activeSlot == null);
			}
			else
			{
				equipClothing();
				setEquip(false);
			}
		}
	}

	private void selectClothing(ClothingSelection pageTile) {
		if (pageTile.Clothing != null) {
			activeTile = pageTile;
			displayPreview(activeTile.Sprite);

			ClothingSelection slot = findInEquipped(activeTile.Clothing.Name);
			if (slot != null) {
				activeSlot = slot;
				setEquip(false);
			} else {
				setEquip(true);
			}
		}
	}

	private void selectSlot(ClothingSelection slot) {
		Sprite target = slot.Sprite;
		if (target != null) {
			activeSlot = slot;
			displayPreview(target);
			setEquip(false);
		}
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
			ClothingData data = activeTile.Clothing;
			Sprite sprite = Resources.Load<Sprite>(data.Path);
			Image slot = clothingArea.SlotImageDictionary[data.Slot];
			slot.sprite = sprite;
			slot.rectTransform.sizeDelta = new Vector2(sprite.rect.width, sprite.rect.height);
			slot.rectTransform.localPosition = data.Location;
			slot.gameObject.SetActive(true);
			
			activeSlot = getClothingSelectionForSlot(data.Slot);
			activeSlot.Clothing = data;
		}
	}

	private void unequipClothing() {
		if (activeSlot != null) {
			Image slotTarget = clothingArea.SlotImageDictionary[activeSlot.Clothing.Slot];
			slotTarget.sprite = null;
			slotTarget.gameObject.SetActive(false);

			activeSlot.Clothing = null;
			activeSlot = null;
			ClothingSelection slot = findNextEquipped();
			Sprite preview = activeTile.Sprite;
			if (slot != null) {
				preview = slot.Sprite;
				activeSlot = slot;
			}

			displayPreview(preview);
		}
	}

	private ClothingSelection getClothingSelectionForSlot(ClothingData.ClothingSlot slot) {
		switch (slot) {
			case ClothingData.ClothingSlot.ACCESSORY:
				return accessorySlot;
			case ClothingData.ClothingSlot.BOTTOM:
				return bottomSlot;
			case ClothingData.ClothingSlot.SHOES:
				return shoesSlot;
			case ClothingData.ClothingSlot.TOP:
				return topSlot;
			case ClothingData.ClothingSlot.WIG:
				return wigSlot;
			default:
				return dressSlot;
		}
	}

	private ClothingSelection findInEquipped(string name) {
		foreach (ClothingSelection slot in slotList) {
			if (slot.Clothing != null && slot.Clothing.Name.Equals(name)) {
				return slot;
			}
		}

		return null;
	}

	private ClothingSelection findNextEquipped() {
		foreach (ClothingSelection slot in slotList)
		{
			if (slot.Clothing != null)
			{
				return slot;
			}
		}

		return null;
	}
}
