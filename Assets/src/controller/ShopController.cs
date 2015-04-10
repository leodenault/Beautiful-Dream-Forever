using System;
using System.Collections.Generic;

public class ShopController {

	private static ShopController INSTANCE;
	private static int NUM_PRIZES = 3;

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

	private IDictionary<ClothingData.ClothingStyle, Shop> shops;

	private ShopController() {
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

	public static ShopController GetInstance() {
		if (INSTANCE == null) {
			INSTANCE = new ShopController();
		}

		return INSTANCE;
	}

	public IList<ClothingData> GetPrizes() {
		return shops[shopStyle].Prizes;
	}

	public void AwardPrize() {
		Shop shop = shops[shopStyle];
		shop.AwardPrize(prize);
		shop.GenerateBattlePrizes(NUM_PRIZES);
	}

	public bool ShopBattled(ClothingData.ClothingStyle shopStyle) {
		return shops[shopStyle].Battled;
	}

	public void BattleShop(ClothingData.ClothingStyle shopStyle) {
		shops[shopStyle].Battled = true;
	}
}
