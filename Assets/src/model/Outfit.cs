using System;
using System.Collections.Generic;

public class Outfit {
	private static float FULL_SET_MULTIPLIER = 1.33f;
	private static float ONE_ESSENCE_MULTIPLIER = 1.2f;

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

		// Either the top/bottom set will return 1.0, or the dress set will,
		// so we can multiply the two together
		float multiplier = calculateMultiplier(topBottomSet());
		multiplier *= calculateMultiplier(dressSet());
		points = (int)Math.Round(points * multiplier);

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
						left.ApplySynergyPoints(synergy.GetPoints(left.Item, right.Item));
					}
				}
			}
		}
	}

	private IList<OutfitItem> topBottomSet() {
		return new List<OutfitItem>() {
			clothing[ClothingData.ClothingSlot.WIG],
			clothing[ClothingData.ClothingSlot.TOP],
			clothing[ClothingData.ClothingSlot.BOTTOM],
			clothing[ClothingData.ClothingSlot.SHOES],
			clothing[ClothingData.ClothingSlot.ACCESSORY]
		};
	}

	private IList<OutfitItem> dressSet() {
		return new List<OutfitItem>() {
			clothing[ClothingData.ClothingSlot.WIG],
			clothing[ClothingData.ClothingSlot.DRESS],
			clothing[ClothingData.ClothingSlot.SHOES],
			clothing[ClothingData.ClothingSlot.ACCESSORY]
		};
	}

	private float calculateMultiplier(IList<OutfitItem> clothingSet) {
		OutfitItem wig = clothing[ClothingData.ClothingSlot.WIG];

		if (wig.Item != null) {
			bool fullSet = true;
			bool oneEssence = true;
			ClothingData.ClothingStyle style = wig.Item.Style;
			ClothingData.ClothingEssence essence = wig.Item.Essence;

			// Check for full set
			foreach (OutfitItem item in clothingSet) {
				if (item.Item == null || item.Item.Style != style) {
					fullSet = false;
					break;
				}
			}

			// Check for one essence
			foreach (OutfitItem item in clothingSet) {
				if (item.Item == null || item.Item.Essence != essence) {
					oneEssence = false;
					break;
				}
			}

			float multiplier = 1.0f;

			if (fullSet) {
				multiplier *= FULL_SET_MULTIPLIER;
			}

			if (oneEssence) {
				multiplier *= ONE_ESSENCE_MULTIPLIER;
			}

			return multiplier;
		}

		return 1.0f;
	}
}
