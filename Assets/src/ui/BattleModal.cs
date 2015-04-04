using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleModal : MonoBehaviour {

	private ShopController shopController;

	public int numPrizes;
	public ClothingData.ClothingStyle shopStyle;
	public GameObject prizeButtonPrefab;
	public GameObject prizeSelector;

	void Start () {
		shopController = ShopController.GetInstance();
		shopController.ShopStyle = shopStyle;
		ClothingData[] prizes = shopController.GenerateBattlePrizes(numPrizes);

		for (int i = 0; i < prizes.Length; i++) {
			GameObject prize = Instantiate(prizeButtonPrefab) as GameObject;
			prize.transform.SetParent(prizeSelector.transform);
			prize.GetComponent<PrizeButton>().Data = prizes[i];
			prize.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		}
	}
}
