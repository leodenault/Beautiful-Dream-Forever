using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ClothingSlotSystem : MonoBehaviour {
	private IList<ClothingSelection> slotList;
	private ClothingSelection activeSlot;
	private ClothingArea clothingArea;
	private Button dressButton;
	private Button topButton;
	private Button bottomButton;

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
		Button[] clothingSlots = GetComponentsInChildren<Button>(true);

		// Add the button click listeners for the clothing slots
		foreach (Button button in clothingSlots) {
			ClothingSelection slotSelection = button.GetComponentsInChildren<ClothingSelection>(true)[0]; // AAGGHHHH WHYYY UNITY!?
			button.onClick.AddListener(() => { selectSlot(slotSelection, selectSlotCallback); });

			if (slotSelection.Equals(dressSlot)) {
				dressButton = button;
			} else if (slotSelection.Equals(topSlot)) {
				topButton = button;
			} else if (slotSelection.Equals(bottomSlot)) {
				bottomButton = button;
			}
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
		handleDresses(data.Slot);

		activeSlot = getClothingSelectionForSlot(data.Slot);
		activeSlot.Clothing = data;
	}

	// Return the next slot that contains an item. If none exist, then return null
	public ClothingSelection UnsetActiveSlot() {
		if (activeSlot != null) {
			unsetSlot(activeSlot);
			activeSlot = null;

			// See if another slot is currently filled. If it is, make it the active slot
			ClothingSelection slot = findNextEquipped();
			if (slot != null) {
				activeSlot = slot;
			}
		}
		return activeSlot;
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

	private void handleDresses(ClothingData.ClothingSlot slot) {
		if (slot == ClothingData.ClothingSlot.DRESS) {
			topButton.gameObject.SetActive(false);
			bottomButton.gameObject.SetActive(false);
			dressButton.gameObject.SetActive(true);
			unsetSlot(topSlot);
			unsetSlot(bottomSlot);
		} else if (slot == ClothingData.ClothingSlot.TOP || slot == ClothingData.ClothingSlot.BOTTOM) {
			topButton.gameObject.SetActive(true);
			bottomButton.gameObject.SetActive(true);
			dressButton.gameObject.SetActive(false);
			unsetSlot(dressSlot);
		}
	}
}
