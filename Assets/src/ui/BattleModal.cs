using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BattleModal : MonoBehaviour {

	private ShopController shopController;
	private IList<PrizeButton> prizeData;
	private PrizeButton activeButton;

	public int numPrizes;
	public ClothingData.ClothingStyle shopStyle;
	public GameObject prizeButtonPrefab;
	public GameObject prizeSelector;
	public Button acceptButton;
	public GameObject selectItemText;

	public void Start () {
		shopController = ShopController.GetInstance();
		shopController.ShopStyle = shopStyle;
		ClothingData[] prizes = shopController.GenerateBattlePrizes(numPrizes);
		prizeData = new List<PrizeButton>();

		for (int i = 0; i < prizes.Length; i++) {
			GameObject prize = Instantiate(prizeButtonPrefab) as GameObject;
			prize.transform.SetParent(prizeSelector.transform);
			prize.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			
			PrizeButton prizeButton = prize.GetComponent<PrizeButton>();
			prizeButton.Data = prizes[i];

			Button button = prize.GetComponentInChildren<Button>();
			button.onClick.AddListener(() => { selectItem(prizeButton); });
			prizeData.Add(prizeButton);
		}
	}

	public void Hide() {
		acceptButton.interactable = false;
		selectItemText.gameObject.SetActive(true);

		if (activeButton != null) {
			setOutlineTransparency(0.0f, activeButton);
			activeButton = null;
		}
		gameObject.SetActive(false);
	}

	public void AcceptItem() {
		shopController.Prize = activeButton.Data;
		GlobalController.GetInstance().Forward("Battle Screen");
	}

	private void selectItem(PrizeButton selected) {
		acceptButton.interactable = true;
		selectItemText.gameObject.SetActive(false);

		if (activeButton != null) {
			setOutlineTransparency(0.0f, activeButton);
		}

		activeButton = selected;
		setOutlineTransparency(1.0f, activeButton);
	}

	private void setOutlineTransparency(float transparency, PrizeButton button) {
		Outline outline = button.GetComponentInChildren<Outline>();
		Color color = outline.effectColor;
		outline.effectColor = new Color(color.r, color.g, color.b, transparency);
	}
}
