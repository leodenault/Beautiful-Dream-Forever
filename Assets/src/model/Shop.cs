using System;
using System.Collections.Generic;

public class Shop {
	private IList<ClothingData> prizes;
	public IList<ClothingData> Prizes {
		get { return prizes; }
	}

	private bool battled;
	public bool Battled {
		get { return battled; }
		set { battled = value; }
	}

	private IList<ClothingData> availableClothing;
	private ClothingData.ClothingStyle shopStyle;
	private Protagonist protagonist;

	public Shop(ClothingData.ClothingStyle shopStyle) {
		battled = false;
		this.shopStyle = shopStyle;
		availableClothing = new List<ClothingData>(ClothingManager.GetInstance().GetClothingDataExceptPlayerInventory(shopStyle));
		protagonist = Protagonist.GetInstance();
	}

	public void GenerateBattlePrizes(int numPrizes) {
		List<ClothingData> selected;

		if (availableClothing.Count <= numPrizes) {
			selected = new List<ClothingData>(availableClothing);
		} else {
			Random itemGenerator = new Random();

			selected = new List<ClothingData>();
			List<int> indices = new List<int>();
			for (int i = 0; i < numPrizes; i++) {
				int index = generateIndex(availableClothing.Count, itemGenerator, indices);
				indices.Add(index);
				selected.Add(availableClothing[index]);
			}
		}

		prizes = selected;
	}

	public void AwardPrize(ClothingData prize) {
		availableClothing.Remove(prize);
		protagonist.Inventory.Add(prize);
		battled = true;
	}

	public void Buy(ClothingData item) {
		if (availableClothing.Contains(item)) {
			availableClothing.Remove(item);
			protagonist.Inventory.Add(item);
			replacePrize(item);
		}
	}

	public bool IsOwned(ClothingData item) {
		return !availableClothing.Contains(item);
	}

	public bool AllItemsAreOwned() {
		return availableClothing.Count == 0;
	}

	public void AcquireAllItems() {
		availableClothing = new List<ClothingData>();
		prizes = new List<ClothingData>();
	}

	public void ReturnAllItems() {
		availableClothing = new List<ClothingData>(ClothingManager.GetInstance().GetClothingDataExceptPlayerInventory(shopStyle));
		GenerateBattlePrizes(3);
	}

	private int generateIndex(int max, Random generator, List<int> previous) {
		int index;
		do {
			index = generator.Next(max);
		} while (previous.Contains(index));
		return index;
	}

	private void replacePrize(ClothingData item) {
		if (prizes.Contains(item)) {

			if (availableClothing.Count <= prizes.Count) {
				prizes = new List<ClothingData>(availableClothing);
			} else {
				prizes.Remove(item);
				List<int> indices = new List<int>();
				foreach (ClothingData prize in prizes) {
					indices.Add(availableClothing.IndexOf(prize));
				}
				prizes.Add(availableClothing[generateIndex(availableClothing.Count, new Random(), indices)]);
			}
		}
	}
}
