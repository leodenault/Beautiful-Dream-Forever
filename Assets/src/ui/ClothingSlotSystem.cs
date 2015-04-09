using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ClothingSlotSystem : MonoBehaviour {
	private IList<ClothingSelection> slotList;
	private ClothingSelection activeSlot;
	private ClothingArea clothingArea;

	public delegate void SelectSlotCallback(ClothingSelection activeSelection);

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

	public bool MakeActive(ClothingData data) {
		foreach (ClothingSelection slot in slotList) {
			if (slot.Clothing != null && slot.Clothing.Id == data.Id) {
				activeSlot = slot;
				return true;
			}
		}

		return false;
	}

	public void UpdateActiveSlot(ClothingData data) {
		clothingArea.SetSlot(data);

		activeSlot = getClothingSelectionForSlot(data.Slot);
		activeSlot.Clothing = data;
	}

	public Sprite UnsetActiveSlot() {
		Sprite activeSprite = null;

		if (activeSlot != null) {
			unsetSlot(activeSlot);
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

	public void Clear() {
		foreach (ClothingSelection slot in slotList) {
			unsetSlot(slot);
		}
	}

	private void unsetSlot(ClothingSelection slot) {
		if (slot.Clothing != null) {
			// Remove clothing area sprite
			clothingArea.ClearSlot(slot.Clothing);

			// Unset the slot
			slot.Clothing = null;
		}
	}

	private void selectSlot(ClothingSelection slot, SelectSlotCallback callback) {
		Sprite target = slot.Sprite;
		if (target != null) {
			activeSlot = slot;
			callback(slot);
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
