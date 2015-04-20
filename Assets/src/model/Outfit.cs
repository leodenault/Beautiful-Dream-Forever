using System;
using System.Collections.Generic;

public class Outfit {
	private IDictionary<ClothingData.ClothingSlot, OutfitItem> clothing;
	private ISynergyManager synergyManager;

	public Outfit() {
		init(SynergyManager.GetInstance());
	}

	public Outfit(ISynergyManager synergyManager) {
		init(synergyManager);
	}

	private void init(ISynergyManager synergyManager) {
		clothing = new Dictionary<ClothingData.ClothingSlot, OutfitItem>();
		this.synergyManager = synergyManager;

		ClothingData.ClothingSlot[] slots = Enum.GetValues(typeof(ClothingData.ClothingSlot)) as ClothingData.ClothingSlot[];
		foreach (ClothingData.ClothingSlot slot in slots) {
			clothing.Add(slot, new OutfitItem());
		}
	}
	
	public int GetPoints() {
		int points = 0;
		applySynergies();
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
		handleDresses(item.Slot);
	}

	public void RemoveItem(ClothingData item) {
		clothing[item.Slot].RemoveItem();
	}

	public void Clear() {
		foreach (OutfitItem item in clothing.Values) {
			item.RemoveItem();
		}
	}

	private void handleDresses(ClothingData.ClothingSlot slot) {
		if (slot == ClothingData.ClothingSlot.TOP || slot == ClothingData.ClothingSlot.BOTTOM) {
			clothing[ClothingData.ClothingSlot.DRESS].RemoveItem();
		} else if (slot == ClothingData.ClothingSlot.DRESS) {
			clothing[ClothingData.ClothingSlot.TOP].RemoveItem();
			clothing[ClothingData.ClothingSlot.BOTTOM].RemoveItem();
		}
	}

	// TODO: Reduce complexity so it's not O(n^2 x m)
	private void applySynergies() {
		ISynergy[] synergies = synergyManager.GetSynergies();
		foreach (OutfitItem left in clothing.Values) {
			left.ClearSynergies();
			foreach (OutfitItem right in clothing.Values) {
				if (!left.Equals(right)) {
					foreach (ISynergy synergy in synergies) {
						if (synergy.IsSynergetic(left.Item, right.Item)) {
							left.ApplySynergy(synergy);
						}
					}
				}
			}
		}
	}
}
