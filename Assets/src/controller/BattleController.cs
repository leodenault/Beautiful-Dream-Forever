using UnityEngine;
using System;
using System.Collections.Generic;

public class BattleController {
	private static BattleController INSTANCE;

	private const string SHOPKEEPER_PREFIX = "shopkeepers/Shopkeeper_";

	private Battle battle;
	private IDictionary<string, Sprite> itemSprites;
	private ShopController shopController;
	private DialogueManager dialogueManager;
	private DialogueManager.DialogueCharacterEnum enCharacter;

	public int TargetScore {
		get { return 70; }
	}

	public BattleController(float timeLimit) {
		shopController = ShopController.GetInstance();
		dialogueManager = DialogueManager.GetInstance();
		ClothingManager manager = ClothingManager.GetInstance();
		// TODO: Make target score dynamic for battle
		battle = new Battle(manager, shopController.ShopStyle, 70, timeLimit);
		itemSprites = new Dictionary<string, Sprite>();

		foreach (ClothingData datum in manager.GetClothingData()) {
			itemSprites.Add(datum.Path, Resources.Load<Sprite>(datum.Path));
		}

		/* Given the choice between a gross enum-to-string-to-enum hack and
		 * a gross huge switch statement, I went with the former. 
		 * Also update this when the time comes for NPCs, obviously. */
		string characterString = string.Format("SHOPKEEPER_{0}", shopController.ShopStyle.ToString());
		enCharacter = (DialogueManager.DialogueCharacterEnum) 
			Enum.Parse(typeof(DialogueManager.DialogueCharacterEnum), characterString);
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
		string pathSuffix = Util.ConvertStyleEnumToReadable(shopController.ShopStyle);
		return Resources.Load<Sprite>(string.Format("{0}{1}", SHOPKEEPER_PREFIX, pathSuffix));
	}

	public string GetBattleBlurb() {
		DialogueManager.DialogueEventEnum enEvent = DialogueManager.DialogueEventEnum.Battle;
		return dialogueManager.GetResponseText(enCharacter, enEvent);
	}

	public string GetResultsBlurb() {
		DialogueManager.DialogueEventEnum enEvent;
		if (battle.IsSuccessful ())
			enEvent = DialogueManager.DialogueEventEnum.PlayerWin;
		else
			enEvent = DialogueManager.DialogueEventEnum.PlayerLose;
		return dialogueManager.GetResponseText(enCharacter, enEvent);
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
