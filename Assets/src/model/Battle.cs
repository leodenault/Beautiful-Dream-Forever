using System;

public class Battle {
	private ClothingManager manager;
	private ClothingData[] clothingPool;
	private Random rnd;

	private ClothingData currentItem;
	public ClothingData CurrentItem {
		get { return currentItem; }
	}

	public Battle(ClothingManager manager) {
		this.manager = manager;
		clothingPool = manager.GetClothingData(ClothingData.ClothingStyle.NONE);
		rnd = new Random();
	}

	public ClothingData GenerateRandomItem() {
		currentItem = clothingPool[rnd.Next(clothingPool.Length)];
		return currentItem;
	}

	public string[] GetItemPaths() {
		string[] paths = new string[clothingPool.Length];

		for (int i = 0; i < clothingPool.Length; i++ ) {
			paths[i] = clothingPool[i].Path;
		}

		return paths;
	}
}
