using UnityEngine;
using System;
using System.Collections.Generic;

public class BattleController {
	private static BattleController INSTANCE;

	private Battle battle;
	private IDictionary<string, Sprite> itemSprites;

	public BattleController() {
		battle = new Battle(ClothingManager.GetInstance());
		itemSprites = new Dictionary<string, Sprite>();

		foreach (string path in battle.GetItemPaths()) {
			itemSprites.Add(path, Resources.Load<Sprite>(path));
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

	private string generateNumberFormat(int number, int index) {
		return string.Format(number < 10 ? "0{{{0}}}" : "{{{0}}}", index);
	}
}
