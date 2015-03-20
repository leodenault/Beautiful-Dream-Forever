using System;
using System.Collections.Generic;

public class Battle {
	private const float START_TIME = 60.0f;
	private const int SHOP_PROBABILITY = 70;
	private const int OTHER_PROBABILITY = 30;
	private const int MAX_PROBABILITY = 100;

	private float elapsedTime;
	private ClothingManager manager;
	private Outfit outfit;
	private IList<ClothingData> shopClothing;
	private IList<ClothingData> otherClothing;
	private Random categorySelector;
	private Random shopSelector;
	private Random otherSelector;

	private int overallScore;
	public int OverallScore {
		get { return overallScore; }
	}

	private int outfitScore;
	public int OutfitScore {
		get { return outfitScore; }
	}

	private ClothingData currentItem;
	public ClothingData CurrentItem {
		get { return currentItem; }
	}

	public Battle(ClothingManager manager, ClothingData.ClothingStyle style) {
		this.manager = manager;
		setupClothingSets(style);
		outfit = new Outfit();
		categorySelector = new Random();
		shopSelector = new Random();
		otherSelector = new Random();
		elapsedTime = 0.0f;
		overallScore = 0;
		outfitScore = 0;
	}

	public ClothingData GenerateRandomItem() {
		int categoryTest = categorySelector.Next(MAX_PROBABILITY);
		if (categoryTest < SHOP_PROBABILITY) {
			int item = shopSelector.Next(shopClothing.Count);
			currentItem = shopClothing[item];
		} else {
			int item = otherSelector.Next(otherClothing.Count);
			currentItem = otherClothing[item];
		}
		return currentItem;
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

	public void UpdateOutfit(ClothingData data) {
		outfit.SetItem(data);
		updateOutfitScore();
	}

	public void RemoveItem(ClothingData data) {
		outfit.RemoveItem(data);
		updateOutfitScore();
	}

	public void AcceptOutfit() {
		overallScore += outfitScore;
		outfit.Clear();
		updateOutfitScore();
	}

	private void updateOutfitScore() {
		outfitScore = outfit.GetPoints();
	}

	private void setupClothingSets(ClothingData.ClothingStyle style) {
		HashSet<ClothingData> shopSet = new HashSet<ClothingData>(manager.GetClothingData(style));
		HashSet<ClothingData> otherSet = new HashSet<ClothingData>(manager.GetClothingData(ClothingData.ClothingStyle.NONE));
		otherSet.ExceptWith(shopSet);

		shopClothing = new List<ClothingData>(shopSet);
		otherClothing = new List<ClothingData>(otherSet);
	}
}
