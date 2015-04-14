using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BattleModal : MonoBehaviour {


	private bool initialized = false;
	private PrizeController shopController;
	private PrizeButton activeButton;

	public int numPrizes;
	public ClothingData.ClothingStyle shopStyle;
	public GameObject prizeButtonPrefab;
	public GameObject prizeSelector;
	public Button acceptButton;
	public GameObject selectItemText;

	public void Start () {
		init();
		SetupPrizes();
	}

	private void init() {
		if (!initialized) {
			shopController = PrizeController.GetInstance();
			shopController.ShopStyle = shopStyle;
			initialized = true;
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

	public void SetupPrizes() {
		init();

		foreach (Transform child in prizeSelector.transform) {
			Destroy(child.gameObject);
		}

		IList<ClothingData> prizes = shopController.GetPrizes();
		foreach (ClothingData prize in prizes) {
			GameObject prizeObject = Instantiate(prizeButtonPrefab) as GameObject;
			prizeObject.transform.SetParent(prizeSelector.transform);
			prizeObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

			PrizeButton prizeButton = prizeObject.GetComponent<PrizeButton>();
			prizeButton.Data = prize;

			Button button = prizeObject.GetComponentsInChildren<Button>(true)[0];
			button.onClick.AddListener(() => { selectItem(prizeButton); });
		}
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
