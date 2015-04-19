using UnityEngine;
using UnityEngine.UI;

public class Shopper : MonoBehaviour {
	private GlobalController controller;
	private PrizeController prizeController;

	public DialogueManager.DialogueCharacterEnum shopper;
	public Image image;

	public void Start() {
		controller = GlobalController.GetInstance();
		prizeController = PrizeController.GetInstance();
	}

	public void InitiateShopperBattle() {
		prizeController.ShopStyle = ClothingData.ClothingStyle.NONE;
		prizeController.Opponent = shopper;
		prizeController.OpponentSprite = OpponentSpriteManager.GetInstance().FetchSprite(shopper);
		controller.Forward("Battle Screen");
	}
}
