using System;
using System.Collections.Generic;

class Outfit {
	private const ClothingData EMPTY = null;

	private IDictionary<ClothingData.ClothingSlot, ClothingData> clothing;
	private SynergyManager synergyManager;

	public Outfit() {
		clothing = new Dictionary<ClothingData.ClothingSlot, ClothingData>();
		synergyManager = SynergyManager.GetInstance();
	}

	// TODO: This is a terribly slow way of getting a list of synergies applying
	// to the given outfit. It iterates through
	// ALL of the synergies from the SynergyManager. There should be some level
	// of caching to improve performance
	public IList<ISynergy> GetSynergies() {
		ISynergy[] allSynergies = synergyManager.GetSynergies();
		IList<ISynergy> synergies = new List<ISynergy>();

		foreach (ClothingData item in clothing.Values) {
			foreach (ISynergy synergy in allSynergies) {
				if (synergy.IsSynergetic(item)) {
					synergies.Add(synergy);
				}
			}
		}

		return synergies;
	}

	public ClothingData GetItem(ClothingData.ClothingSlot slot) {
		return clothing[slot];
	}

	public void SetItem(ClothingData item) {
		if (clothing.ContainsKey(item.Slot)) {
			clothing.Remove(item.Slot);
		}

		clothing.Add(item.Slot, item);
	}

	public void RemoveItem(ClothingData item) {
		if (clothing.ContainsKey(item.Slot)) {
			clothing.Remove(item.Slot);
		}
	}

	public void Clear() {
		clothing.Clear();
	}
}
