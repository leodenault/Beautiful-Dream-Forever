using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ClothingSlotSystem : MonoBehaviour {
	private IList<ClothingSelection> slotList;
	private ClothingSelection activeSlot;
	private ClothingArea clothingArea;

	public delegate void SelectSlotCallback(Sprite activeSprite);

	public ClothingSelection wigSlot;
	public ClothingSelection topSlot;
	public ClothingSelection bottomSlot;
	public ClothingSelection shoesSlot;
	public ClothingSelection accessorySlot;
	public ClothingSelection dressSlot;

	// TODO: I'd like to avoid this kind of initialization in the future, though as far as I know
	// there's no way to share script instances between GameObjects in the inspector
	public void Init(ClothingArea clothingArea, SelectSlotCallback selectSlotCallback) {
		slotList = new List<ClothingSelection>();
		this.clothingArea = clothingArea;
		Button[] clothingSlots = GetComponentsInChildren<Button>();

		// Add the button click listeners for the clothing slots
		foreach (Button button in clothingSlots) {
			ClothingSelection slotSelection = button.GetComponentInChildren<ClothingSelection>();
			button.onClick.AddListener(() => { selectSlot(slotSelection, selectSlotCallback); });
		}

		slotList.Add(wigSlot);
		slotList.Add(topSlot);
		slotList.Add(bottomSlot);
		slotList.Add(shoesSlot);
		slotList.Add(accessorySlot);
		slotList.Add(dressSlot);
	}

	public bool MakeActive(string name) {
		foreach (ClothingSelection slot in slotList) {
			if (slot.Clothing != null && slot.Clothing.Name.Equals(name)) {
				activeSlot = slot;
				return true;
			}
		}

		return false;
	}

	public void UpdateActiveSlot(ClothingData data) {
		Sprite sprite = Resources.Load<Sprite>(data.Path);
		Image slot = clothingArea.GetImageForSlot(data.Slot);

		slot.sprite = sprite;
		slot.rectTransform.sizeDelta = new Vector2(sprite.rect.width, sprite.rect.height);
		slot.rectTransform.localPosition = data.Location;
		slot.gameObject.SetActive(true);

		activeSlot = getClothingSelectionForSlot(data.Slot);
		activeSlot.Clothing = data;
	}

	public Sprite UnsetActiveSlot() {
		Sprite activeSprite = null;

		if (activeSlot != null) {
			// Remove clothing area sprite
			Image slotTarget = clothingArea.GetImageForSlot(activeSlot.Clothing.Slot);
			slotTarget.sprite = null;
			slotTarget.gameObject.SetActive(false);

			// Unset the active slot
			activeSlot.Clothing = null;
			activeSlot = null;

			// See if another slot is currently filled. If it is, make it the active slot
			ClothingSelection slot = findNextEquipped();
			if (slot != null) {
				activeSprite = slot.Sprite;
				activeSlot = slot;
			}
		}
		return activeSprite;
	}

	private void selectSlot(ClothingSelection slot, SelectSlotCallback callback) {
		Sprite target = slot.Sprite;
		if (target != null) {
			activeSlot = slot;
			callback(target);
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

	private ClothingSelection findNextEquipped() {
		foreach (ClothingSelection slot in slotList) {
			if (slot.Clothing != null) {
				return slot;
			}
		}

		return null;
	}
}
