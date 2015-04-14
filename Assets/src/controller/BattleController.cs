using UnityEngine;
using System;
using System.Collections.Generic;

public class BattleController {
	private static BattleController INSTANCE;

	private Battle battle;
	private IDictionary<string, Sprite> itemSprites;
	private PrizeController prizeController;
	private DialogueManager dialogueManager;
	private DialogueManager.DialogueCharacterEnum enCharacter;

	public int TargetScore {
		get { return 70; }
	}

	public int MoneyWon {
		get { return prizeController.MoneyWon; }
	}

	public BattleController(float timeLimit) {
		prizeController = PrizeController.GetInstance();
		dialogueManager = DialogueManager.GetInstance();
		ClothingManager manager = ClothingManager.GetInstance();
		// TODO: Make target score dynamic for battle
		battle = new Battle(manager, prizeController.ShopStyle, 70, timeLimit);
		itemSprites = new Dictionary<string, Sprite>();

		foreach (ClothingData datum in manager.GetClothingData()) {
			itemSprites.Add(datum.Path, Resources.Load<Sprite>(datum.Path));
		}

		enCharacter = prizeController.Opponent;
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
			prizeController.AwardPrize();
		}
	}

	public Sprite GetOpponent() {
		return prizeController.OpponentSprite;
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
		return Resources.Load<Sprite>(prizeController.Prize.Path);
	}

	public bool WonPrize() {
		return prizeController.WonPrize();
	}

	private string generateNumberFormat(int number, int index) {
		return string.Format(number < 10 ? "0{{{0}}}" : "{{{0}}}", index);
	}
}
