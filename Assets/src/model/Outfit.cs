using System;
using System.Collections.Generic;

class Outfit {
	private IDictionary<ClothingData.ClothingSlot, OutfitItem> clothing;

	public Outfit() {
		clothing = new Dictionary<ClothingData.ClothingSlot, OutfitItem>();

		ClothingData.ClothingSlot[] slots = Enum.GetValues(typeof(ClothingData.ClothingSlot)) as ClothingData.ClothingSlot[];
		foreach (ClothingData.ClothingSlot slot in slots) {
			clothing.Add(slot, new OutfitItem());
		}
	}

	
	public int GetPoints() {
		int points = 0;
		foreach (OutfitItem item in clothing.Values) {
			points += item.Points;
		}

		return points;
	}

	public ClothingData GetItem(ClothingData.ClothingSlot slot) {
		return clothing[slot].Item;
	}

	public void SetItem(ClothingData item) {
		clothing[item.Slot].Item = item;
	}

	public void RemoveItem(ClothingData item) {
		clothing[item.Slot].RemoveItem();
	}

	public void Clear() {
		foreach (OutfitItem item in clothing.Values) {
			item.RemoveItem();
		}
	}
}
