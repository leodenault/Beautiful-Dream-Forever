using System.Collections.Generic;

public class OutfitItem {
	private static int WIG_MULTIPLIER = 10;
	private static int TOP_MULTIPLIER = 30;
	private static int BOTTOM_MULTIPLIER = 30;
	private static int DRESS_MULTIPLIER = 60;
	private static int SHOES_MULTIPLIER = 20;
	private static int ACCESSORY_MULTIPLIER = 10;

	private const ClothingData EMPTY = null;

	private ISynergyManager synergyManager;
	private IList<ISynergy> synergies;

	private int points;
	public int Points {
		get { return points; }
	}

	private ClothingData item;
	public ClothingData Item {
		get { return item; }
		set {
			item = value;
			computePoints();
		}
	}

	public OutfitItem() {
		init(EMPTY, SynergyManager.GetInstance());
	}

	public OutfitItem(ClothingData item) {
		init(item, SynergyManager.GetInstance());
	}

	public OutfitItem(ClothingData item, ISynergyManager synergyManager) {
		init(item, synergyManager);
	}

	public void RemoveItem() {
		item = EMPTY;
		computePoints();
	}

	private void init(ClothingData item, ISynergyManager synergyManager) {
		this.item = item;
		this.synergyManager = synergyManager;
		computePoints();
	}

	private static int getMultiplierForSlot(ClothingData.ClothingSlot slot) {
		switch (slot) {
			case ClothingData.ClothingSlot.WIG:
				return WIG_MULTIPLIER;
			case ClothingData.ClothingSlot.TOP:
				return TOP_MULTIPLIER;
			case ClothingData.ClothingSlot.BOTTOM:
				return BOTTOM_MULTIPLIER;
			case ClothingData.ClothingSlot.DRESS:
				return DRESS_MULTIPLIER;
			case ClothingData.ClothingSlot.SHOES:
				return SHOES_MULTIPLIER;
			default:
				return ACCESSORY_MULTIPLIER;
		}
	}

	private void computePoints() {
		points = 0;

		if (item != EMPTY) {
			// TODO: This is a terribly slow way of getting a list of synergies applying
			// to the given outfit. It iterates through
			// ALL of the synergies from the SynergyManager. There should be some level
			// of caching to improve performance
			ISynergy[] allSynergies = synergyManager.GetSynergies();
			synergies = new List<ISynergy>();

			foreach (ISynergy synergy in allSynergies) {
				if (synergy.IsSynergetic(item)) {
					synergies.Add(synergy);
					points += synergy.GetPoints();
				}
			}

			int multiplier = getMultiplierForSlot(item.Slot);
			points = (points == 0) ? multiplier : points * multiplier;
		}
	}
}
