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
	private Image activeSlot;
	private IDictionary<ClothingData.ClothingSlot, Image> slotImageDictionary;
	

    public Button equipButton;
    public Sprite equipImage;
    public Sprite unequipImage;
	public Image clothingArea;
	public Image preview;

    public GameObject pageTilePanel;

    public void Start()
    {
        isEquipped = false;
		slotImageDictionary = new Dictionary<ClothingData.ClothingSlot, Image>();
        controller = WardrobeController.GetInstance();
        Button[] pageTiles = pageTilePanel.GetComponentsInChildren<Button>();
		Image[] clothingSlotImages = clothingArea.GetComponentsInChildren<Image>(true);
        controller.AssignClothingBackrounds(pageTiles);

		activeTile = pageTiles[0].GetComponentInChildren<PageTile>();
		// Add the button click listeners for the page tiles
		foreach (Button button in pageTiles) {
			PageTile pageTile = button.GetComponentInChildren<PageTile>();
			button.onClick.AddListener(() => { SelectClothing(pageTile); });
		}

		// Prepare the slot to button dictionary
		ClothingData.ClothingSlot[] slots = (ClothingData.ClothingSlot[])Enum.GetValues(typeof(ClothingData.ClothingSlot));
		for (int i = 0; i < slots.Length && i < clothingSlotImages.Length - 1; i++) {
			clothingSlotImages[i + 1].transform.SetParent(clothingArea.transform);
			slotImageDictionary.Add(slots[i], clothingSlotImages[i + 1]);
		}
    }

	public void SelectClothing(PageTile pageTile) {
		activeTile = pageTile;
		displayPreview();
		setEquip(true);
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

	private void setEquip(bool equipping) {
		if (equipping) {
			equipButton.image.sprite = equipImage;
			isEquipped = false;
		} else {
			equipButton.image.sprite = unequipImage;
			isEquipped = true;
		}
	}

	private void displayPreview() {
		if (activeTile.Clothing != null) {
			Sprite sprite = Resources.Load<Sprite>(activeTile.Clothing.Path);
			float spriteWidth = sprite.rect.width;
			float spriteHeight = sprite.rect.height;
			float scale = Util.computeScale(PREVIEW_WIDTH, spriteWidth, PREVIEW_HEIGHT, spriteHeight);

			preview.sprite = sprite;
			preview.rectTransform.sizeDelta = new Vector2(spriteWidth * scale, spriteHeight * scale);
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
			activeSlot = slot;
		}
	}

	private void unequipClothing() {
		if (activeSlot != null) {
			activeSlot.sprite = null;
			activeSlot.gameObject.SetActive(false);
		}
	}
}
