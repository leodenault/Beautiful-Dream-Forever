using System;

public class Battle {
	private const float START_TIME = 60.0f;

	private float elapsedTime;
	private int overallScore;
	private int outfitScore;
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
		elapsedTime = 0.0f;
		overallScore = 0;
		outfitScore = 0;
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

	public float RemainingTime(float delta) {
		if (elapsedTime + delta > START_TIME) {
			elapsedTime = START_TIME;
			return 0.0f;
		}

		elapsedTime += delta;
		return START_TIME - elapsedTime;
	}

	public bool TimeOut() {
		return START_TIME < elapsedTime || Math.Abs(START_TIME - elapsedTime) < 0.001;
	}
}
