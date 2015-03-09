using UnityEngine;
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
}
