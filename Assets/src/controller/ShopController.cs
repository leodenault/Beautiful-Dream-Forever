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
		shopStyle = ClothingData.ClothingStyle.NONE;
	}

	public static ShopController GetInstance() {
		if (INSTANCE == null) {
			INSTANCE = new ShopController();
		}

		return INSTANCE;
	}

	public ClothingData[] GenerateBattlePrizes(int numPrizes) {
		ClothingData[] clothing = ClothingManager.GetInstance().GetClothingData(shopStyle);
		Random itemGenerator = new Random();

		List<int> indices = new List<int>();
		List<ClothingData> selected = new List<ClothingData>();
		for (int i = 0; i < numPrizes; i++) {
			int index = generateIndex(clothing.Length, itemGenerator, indices);
			indices.Add(index);
			selected.Add(clothing[index]);
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
