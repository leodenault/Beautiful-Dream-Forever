using UnityEngine;
using System;
using System.Collections.Generic;

public class BattleController {
	private static BattleController INSTANCE;

	private const string SHOPKEEPER_PREFIX = "shopkeepers/Shopkeeper_";

	private Battle battle;
	private IDictionary<string, Sprite> itemSprites;
	private ShopController shopController;

	public int TargetScore {
		get { return 70; }
	}

	public BattleController(float timeLimit) {
		shopController = ShopController.GetInstance();
		ClothingManager manager = ClothingManager.GetInstance();
		// TODO: Make target score dynamic for battle
		battle = new Battle(manager, shopController.ShopStyle, 70, timeLimit);
		itemSprites = new Dictionary<string, Sprite>();

		foreach (ClothingData datum in manager.GetClothingData()) {
			itemSprites.Add(datum.Path, Resources.Load<Sprite>(datum.Path));
		}
	}

	public ClothingData GenerateRandomItem() {
		return battle.GenerateRandomItem();
	}

	public Sprite GetCurrentItemSprite() {
		return itemSprites[battle.CurrentItem.Path];
	}

	public string RemainingTime(float delta) {
		int remainingTime = (int)Math.Ceiling(battle.RemainingTime(delta));
		int minutes = remainingTime / 60;
		int seconds = remainingTime - (60 * minutes);
		return string.Format(generateNumberFormat(minutes, 0) + ":" + generateNumberFormat(seconds, 1),
			minutes, seconds);
	}

	// Should be called AFTER RemainingTime() as it doesn't update the timer
	public bool TimeOut() {
		return battle.TimeOut();
	}

	public int UpdateOutfitScore(ClothingData data) {
		battle.UpdateOutfit(data);
		return battle.OutfitScore;
	}

	public int RemoveItem(ClothingData data) {
		battle.RemoveItem(data);
		return battle.OutfitScore;
	}

	public void EndBattle() {
		if (battle.IsSuccessful()) {
			shopController.AwardPrize();
		}
	}

	public Sprite GetShopkeeper() {
		string name = Enum.GetName(typeof(ClothingData.ClothingStyle), shopController.ShopStyle);
		string pathSuffix = name.Substring(0, 1) + name.Substring(1).ToLower();
		return Resources.Load<Sprite>(string.Format("{0}{1}", SHOPKEEPER_PREFIX, pathSuffix));
	}

	public bool IsSuccessful() {
		return battle.IsSuccessful();
	}

	public Sprite PrizeSprite() {
		return Resources.Load<Sprite>(shopController.Prize.Path);
	}

	private string generateNumberFormat(int number, int index) {
		return string.Format(number < 10 ? "0{{{0}}}" : "{{{0}}}", index);
	}
}
