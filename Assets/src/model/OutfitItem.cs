using System.Collections.Generic;

public class OutfitItem {
	private static IDictionary<ClothingData.ClothingSlot, int> SLOT_MULTIPLIERS = new Dictionary<ClothingData.ClothingSlot, int>() {
		{ ClothingData.ClothingSlot.WIG, 10 },
		{ ClothingData.ClothingSlot.TOP, 30 },
		{ ClothingData.ClothingSlot.BOTTOM, 30 },
		{ ClothingData.ClothingSlot.DRESS, 60 },
		{ ClothingData.ClothingSlot.SHOES, 20 },
		{ ClothingData.ClothingSlot.ACCESSORY, 10 }
	};

	private const ClothingData EMPTY = null;

	private int points;
	public int Points {
		get { return (item == EMPTY) ? 0 : points * SLOT_MULTIPLIERS[item.Slot]; }
	}

	private ClothingData item;
	public ClothingData Item {
		get { return item; }
		set {
			item = value;
			points = (value == EMPTY) ? 0 : 1;
		}
	}

	public OutfitItem() {
		init(EMPTY);
	}

	public OutfitItem(ClothingData item) {
		init(item);
	}

	public void RemoveItem() {
		item = EMPTY;
	}

	public void ApplySynergy(ISynergy synergy) {
		if (item != EMPTY) {
			points += synergy.GetPoints();
		}
	}

	public void ClearSynergies() {
		points = (item == EMPTY) ? 0 : 1;
	}

	private void init(ClothingData item) {
		this.item = item;
	}
}
