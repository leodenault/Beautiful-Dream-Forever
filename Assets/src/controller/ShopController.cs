using System;
using System.Collections.Generic;

public class ShopController {

	private static ShopController INSTANCE;

	private ClothingData.ClothingStyle shopStyle;
	public ClothingData.ClothingStyle ShopStyle {
		get { return shopStyle; }
		set { shopStyle = value; }
	}

	private ClothingData prize;
	public ClothingData Prize {
		get { return prize; }
		set { prize = value; }
	}

	private ShopController() {
		shopStyle = ClothingData.ClothingStyle.ATHLETIC;
	}

	public static ShopController GetInstance() {
		if (INSTANCE == null) {
			INSTANCE = new ShopController();
		}

		return INSTANCE;
	}

	public ClothingData[] GenerateBattlePrizes(int numPrizes) {
		ClothingData[] clothing = ClothingManager.GetInstance().GetClothingDataExceptPlayerInventory(shopStyle);
		List<ClothingData> selected;

		if (clothing.Length <= numPrizes) {
			selected = new List<ClothingData>(clothing);
		} else {
			Random itemGenerator = new Random();

			selected = new List<ClothingData>();
			List<int> indices = new List<int>();
			for (int i = 0; i < numPrizes; i++) {
				int index = generateIndex(clothing.Length, itemGenerator, indices);
				indices.Add(index);
				selected.Add(clothing[index]);
			}
		}
		return selected.ToArray();
	}

	private int generateIndex(int max, Random generator, List<int> previous) {
		int index;
		do {
			index = generator.Next(max);
		} while (previous.Contains(index));
		return index;
	}
}
