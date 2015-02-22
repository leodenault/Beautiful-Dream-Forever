using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Wardrobe : MonoBehaviour {
	private static float PREVIEW_WIDTH = 74.0f;
	private static float PREVIEW_HEIGHT = 104.0f;

    private bool isEquipped;
    private WardrobeController controller;
	private PageTile activeTile;
	private Button activeSlot;
	private IDictionary<ClothingData.ClothingSlot, Image> slotImageDictionary;
	private IDictionary<Button, Image> buttonSlotImageDictionary;
	private IDictionary<Button, Image> buttonSlotBackgroundDictionary;

    public Button equipButton;
    public Sprite equipImage;
    public Sprite unequipImage;
	public Image clothingArea;
	public Image preview;
    public GameObject pageTilePanel;

	public Button wigSlotButton;
	public Button topSlotButton;
	public Button bottomSlotButton;
	public Button shoesSlotButton;
	public Button accessorySlotButton;
	public Button dressSlotButton;

    public void Start()
    {
        isEquipped = false;
		slotImageDictionary = new Dictionary<ClothingData.ClothingSlot, Image>();
		buttonSlotImageDictionary = new Dictionary<Button, Image>();
		buttonSlotBackgroundDictionary = new Dictionary<Button, Image>();
        controller = WardrobeController.GetInstance();
        Button[] pageTiles = pageTilePanel.GetComponentsInChildren<Button>();
		Image[] clothingSlotImages = clothingArea.GetComponentsInChildren<Image>(true);
        controller.AssignClothingBackrounds(pageTiles);

		activeTile = pageTiles[0].GetComponentInChildren<PageTile>();
		// Add the button click listeners for the page tiles
		foreach (Button button in pageTiles) {
			PageTile pageTile = button.GetComponentInChildren<PageTile>();
			button.onClick.AddListener(() => { selectClothing(pageTile); });
		}

		// Prepare dictionaries
		ClothingData.ClothingSlot[] slots = (ClothingData.ClothingSlot[])Enum.GetValues(typeof(ClothingData.ClothingSlot));
		for (int i = 0; i < slots.Length && i < clothingSlotImages.Length - 1; i++) {
			ClothingData.ClothingSlot slot = slots[i];
			Image image = clothingSlotImages[i + 1];
			Button slotButton = getSlotButtonForSlot(slot);
			slotImageDictionary.Add(slot, image);
			buttonSlotImageDictionary.Add(slotButton, image);
			slotButton.onClick.AddListener(() => { selectSlot(slotButton); });
			
			// Extract the button background image and put it in the dictionary
			Image[] buttonImages = slotButton.GetComponentsInChildren<Image>(true);
			// First image is the button background, second is the text, third is
			// the actual image background overlay to which we want access
			buttonSlotBackgroundDictionary.Add(slotButton, buttonImages[2]);
		}
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
			setEquip(isEquipped);
		}
    }

	private void selectClothing(PageTile pageTile) {
		activeTile = pageTile;
		displayPreview(activeTile);
		setEquip(true);
	}

	private void selectSlot(Button slotButton) {
		Sprite target = buttonSlotImageDictionary[slotButton].sprite;
		if (target != null) {
			activeSlot = slotButton;
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

	private void displayPreview(PageTile pageTile) {
		if (pageTile.Clothing != null) {
			displayPreview(Resources.Load<Sprite>(pageTile.Clothing.Path));
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
			Image slot = slotImageDictionary[data.Slot];
			slot.sprite = sprite;
			slot.rectTransform.sizeDelta = new Vector2(sprite.rect.width, sprite.rect.height);
			slot.rectTransform.localPosition = data.Location;
			slot.gameObject.SetActive(true);
			
			activeSlot = getSlotButtonForSlot(data.Slot);
			Image slotBackground = buttonSlotBackgroundDictionary[activeSlot];
			Rect buttonRect = activeSlot.GetComponent<RectTransform>().rect;
			Util.ScaleImageToMaxDimensions(slotBackground, sprite, buttonRect.width, buttonRect.height);
			slotBackground.gameObject.SetActive(true);
		}
	}

	private void unequipClothing() {
		if (activeSlot != null) {
			Image slotTarget = buttonSlotImageDictionary[activeSlot];
			slotTarget.sprite = null;
			slotTarget.gameObject.SetActive(false);
			displayPreview(activeTile);
			buttonSlotBackgroundDictionary[activeSlot].gameObject.SetActive(false);
		}
	}

	private Button getSlotButtonForSlot(ClothingData.ClothingSlot slot) {
		switch (slot) {
			case ClothingData.ClothingSlot.ACCESSORY:
				return accessorySlotButton;
			case ClothingData.ClothingSlot.BOTTOM:
				return bottomSlotButton;
			case ClothingData.ClothingSlot.SHOES:
				return shoesSlotButton;
			case ClothingData.ClothingSlot.TOP:
				return topSlotButton;
			case ClothingData.ClothingSlot.WIG:
				return wigSlotButton;
			default:
				return dressSlotButton;
		}
	}
}
