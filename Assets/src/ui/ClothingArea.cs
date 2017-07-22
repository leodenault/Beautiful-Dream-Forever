using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class ClothingArea : MonoBehaviour {
	private IDictionary<ClothingData.ClothingSlot, ClothingSlot> enumSlotDictionary;

	public void SetSlot(ClothingData data) {
		ClothingSlot slot = getClothingSlotForData(data);
		slot.SetSlot(data);
		orderInLayer(slot, data.Layer);
	}

	public void ClearSlot(ClothingData data) {
		getClothingSlotForData(data).Clear();
	}

	private ClothingSlot getClothingSlotForData(ClothingData data) {
		if (enumSlotDictionary == null) {
			initDictionary();
		}

		return enumSlotDictionary[data.Slot];
	}

	private void initDictionary() {
		enumSlotDictionary = new Dictionary<ClothingData.ClothingSlot, ClothingSlot>();
		ClothingSlot[] clothingSlots = GetComponentsInChildren<ClothingSlot>(true);
		ClothingData.ClothingSlot[] slots = (ClothingData.ClothingSlot[])Enum.GetValues(typeof(ClothingData.ClothingSlot));

		for (int i = 0; i < slots.Length && i < clothingSlots.Length; i++) {
			ClothingData.ClothingSlot slot = slots[i];
			ClothingSlot clothingSlot = clothingSlots[i];
			enumSlotDictionary.Add(slot, clothingSlot);
		}
	}

	// Layers that are lower in the ClothingSlot need to be displayed HIGHER. Example:
	// an item on layer 3 displays OVER an item on layer 1. Unity's layers are the opposite of this.
	// You can thank Rachel for this "feature"
	private void orderInLayer(ClothingSlot slot, int layer) {
		List<ClothingSlot> slots = new List<ClothingSlot>(enumSlotDictionary.Values);
		slots.Sort((x, y) => x.image.transform.GetSiblingIndex().CompareTo(y.image.transform.GetSiblingIndex()));
		slots.Reverse();
		bool placed = false;

		// Move through the list and see where it fits
		foreach (ClothingSlot current in slots) {
			if (slot.Layer <= current.Layer) {
				slot.image.transform.SetSiblingIndex(current.image.transform.GetSiblingIndex());
				placed = true;
				break;
			}
		}

		// If it didn't fit anywhere, then move it to the lowest layer
		if (!placed) {
			slot.image.transform.SetAsFirstSibling();
		}
	}
}
