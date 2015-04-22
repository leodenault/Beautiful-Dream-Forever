using System;
using System.Collections.Generic;
using UnityEngine;

public class PrizeController {

	private static PrizeController INSTANCE;
	private static int NUM_PRIZES = 3;
	private static int MIN_MONEY = 5;
	private static int MAX_MONEY = 15;
	private static int SHOPPER_BATTLE_TARGET_SCORE = 2250;

	private const string SHOPKEEPER_PREFIX = "shopkeepers/Shopkeeper_";

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

	// TODO: Remove string conversion for shopkeepers
	private DialogueManager.DialogueCharacterEnum opponent;
	public DialogueManager.DialogueCharacterEnum Opponent {
		get {
			DialogueManager.DialogueCharacterEnum result;

			if (shopStyle == ClothingData.ClothingStyle.NONE) {
				result = opponent;
			} else {
				string characterString = string.Format("SHOPKEEPER_{0}", shopStyle.ToString());
				result = (DialogueManager.DialogueCharacterEnum)
					Enum.Parse(typeof(DialogueManager.DialogueCharacterEnum), characterString);
			}

			return result;
		}
		set { opponent = value; }
	}

	private Sprite opponentSprite;
	public Sprite OpponentSprite {
		get { return opponentSprite; }
		set { opponentSprite = value; }
	}

	private int moneyWon;
	public int MoneyWon {
		get { return moneyWon; }
	}

	private IDictionary<ClothingData.ClothingStyle, Shop> shops;

	private PrizeController() {
		shopStyle = ClothingData.ClothingStyle.ATHLETIC;
		shops = new Dictionary<ClothingData.ClothingStyle, Shop>();

		ClothingData.ClothingStyle[] styles = Enum.GetValues(typeof(ClothingData.ClothingStyle)) as ClothingData.ClothingStyle[];
		foreach (ClothingData.ClothingStyle style in styles) {
			if (style != ClothingData.ClothingStyle.NONE) {
				Shop shop = new Shop(style);
				shop.GenerateBattlePrizes(NUM_PRIZES);
				shops.Add(style, shop);
			}
		}
	}

	public static PrizeController GetInstance() {
		if (INSTANCE == null) {
			INSTANCE = new PrizeController();
		}

		return INSTANCE;
	}

	public IList<ClothingData> GetPrizes() {
		return shops[shopStyle].Prizes;
	}

	public void AwardPrize() {
		if (shopStyle == ClothingData.ClothingStyle.NONE) {
			moneyWon = generateRandomMonetaryPrize();
			Protagonist.GetInstance().modifyBalance(moneyWon);
		} else {
			Shop shop = shops[shopStyle];
			shop.AwardPrize(prize);
			shop.GenerateBattlePrizes(NUM_PRIZES);
		}
	}

	public void Buy(ClothingData item) {
		Shop shop = shops[item.Style];
		shop.Buy(item);
	}

	public bool ShopBattled(ClothingData.ClothingStyle shopStyle) {
		return shops[shopStyle].Battled;
	}

	public void BattleShop(ClothingData.ClothingStyle shopStyle) {
		shops[shopStyle].Battled = true;
	}

	public bool WonPrize() {
		return !(shopStyle == ClothingData.ClothingStyle.NONE);
	}

	public bool IsOwned(ClothingData item, ClothingData.ClothingStyle shopStyle) {
		return shopStyle != ClothingData.ClothingStyle.NONE && shops[shopStyle].IsOwned(item);
	}

	public bool AllItemsAreOwned(ClothingData.ClothingStyle shopStyle) {
		return shopStyle != ClothingData.ClothingStyle.NONE && shops[shopStyle].AllItemsAreOwned();
	}

	public void AcquireAllItems() {
		foreach (Shop shop in shops.Values) {
			shop.AcquireAllItems();
		}
	}

	public void ReturnAllItems() {
		foreach (Shop shop in shops.Values) {
			shop.ReturnAllItems();
		}
	}

	public int GetTargetScore() {
		return (shopStyle == ClothingData.ClothingStyle.NONE) ? SHOPPER_BATTLE_TARGET_SCORE : shops[shopStyle].TargetScore;
	}

	private int generateRandomMonetaryPrize() {
		System.Random random = new System.Random();
		return random.Next(MIN_MONEY, MAX_MONEY);
	}
}
