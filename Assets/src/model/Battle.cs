using System;
using System.Collections.Generic;

public class Battle {
	private const float DEFAULT_TIME_LIMIT = 60.0f;
	private const int SHOP_PROBABILITY = 34;
	private const int PLAYER_PROBABILITY = 67;
	private const int MAX_PROBABILITY = 100;

	private float timeLimit;
	private float elapsedTime;
	private int targetScore;
	private ClothingManager manager;
	private Outfit outfit;
	private IList<ClothingData> shopClothing;
	private IList<ClothingData> playerClothing;
	private IList<ClothingData> otherClothing;
	private Random categorySelector;
	private Random shopSelector;
	private Random playerSelector;
	private Random otherSelector;

	private int outfitScore;
	public int OutfitScore {
		get { return outfitScore; }
	}

	private ClothingData currentItem;
	public ClothingData CurrentItem {
		get { return currentItem; }
	}

	public Battle(ClothingManager manager, ClothingData.ClothingStyle style, int targetScore, float timeLimit) {
		this.manager = manager;
		this.targetScore = targetScore;
		setupClothingSets(style);
		outfit = new Outfit();
		categorySelector = new Random();
		shopSelector = new Random();
		playerSelector = new Random();
		otherSelector = new Random();
		elapsedTime = 0.0f;
		outfitScore = 0;
		this.timeLimit = (timeLimit == 0) ? DEFAULT_TIME_LIMIT : timeLimit;
	}

	public ClothingData GenerateRandomItem() {
		int categoryTest = categorySelector.Next(MAX_PROBABILITY);
		if (categoryTest <= SHOP_PROBABILITY) {
			currentItem = pickRandomItem(shopSelector, shopClothing);
		} else if (categoryTest <= PLAYER_PROBABILITY) {
			if (playerClothing.Count == 0) {
				currentItem = pickRandomItem(shopSelector, shopClothing);
			} else {
				currentItem = pickRandomItem(playerSelector, playerClothing);
			}
		} else {
			currentItem = pickRandomItem(otherSelector, otherClothing);
		}
		return currentItem;
	}

	public float RemainingTime(float delta) {
		if (elapsedTime + delta > timeLimit) {
			elapsedTime = timeLimit;
			return 0.0f;
		}

		elapsedTime += delta;
		return timeLimit - elapsedTime;
	}

	public bool TimeOut() {
		return timeLimit < elapsedTime || Math.Abs(timeLimit - elapsedTime) < 0.001;
	}

	public void UpdateOutfit(ClothingData data) {
		outfit.SetItem(data);
		updateOutfitScore();
	}

	public void RemoveItem(ClothingData data) {
		outfit.RemoveItem(data);
		updateOutfitScore();
	}

	public bool IsSuccessful() {
		return outfitScore >= targetScore;
	}

	private void updateOutfitScore() {
		outfitScore = outfit.GetPoints();
	}

	private void setupClothingSets(ClothingData.ClothingStyle style) {
		HashSet<ClothingData> shopSet = new HashSet<ClothingData>(manager.GetClothingDataExceptPlayerInventory(style));
		HashSet<ClothingData> playerSet = new HashSet<ClothingData>(manager.GetClothingData(ClothingData.ClothingStyle.NONE));
		HashSet<ClothingData> otherSet = new HashSet<ClothingData>(manager.GetClothingData());
		otherSet.ExceptWith(shopSet);
		otherSet.ExceptWith(playerSet);

		shopClothing = new List<ClothingData>(shopSet);
		playerClothing = new List<ClothingData>(playerSet);
		otherClothing = new List<ClothingData>(otherSet);
	}

	private ClothingData pickRandomItem(Random selector, IList<ClothingData> group) {
		int item = selector.Next(group.Count);
		return group[item];
	}
}
