using System;
using System.Collections.Generic;

public class Shop {
	private IList<ClothingData> prizes;
	public IList<ClothingData> Prizes {
		get { return prizes; }
		set { prizes = value; }
	}

	private IList<ClothingData> availableClothing;

	public Shop(ClothingData.ClothingStyle shopStyle) {
		availableClothing = new List<ClothingData>(ClothingManager.GetInstance().GetClothingDataExceptPlayerInventory(shopStyle));
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
		Protagonist.GetInstance().Inventory.Add(prize);
	}

	private int generateIndex(int max, Random generator, List<int> previous) {
		int index;
		do {
			index = generator.Next(max);
		} while (previous.Contains(index));
		return index;
	}
}
