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

	public void InitiateShopperBattle(Shopper shopper) {
		prizeController.ShopStyle = ClothingData.ClothingStyle.NONE;
		prizeController.Opponent = shopper.shopper;
		prizeController.OpponentSprite = image.sprite;
		controller.Forward("Battle Screen");
	}
}
