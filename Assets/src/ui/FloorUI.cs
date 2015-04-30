using UnityEngine;
using UnityEngine.UI;

public class FloorUI : MonoBehaviour {

	private PrizeController prizeController;
	private EscalatorAttendantController escalatorAttendantController;

	protected GlobalController controller;

	public string upperFloorName;
	public Button escalator;
	public ClothingData.ClothingStyle shopStyle1;
	public ClothingData.ClothingStyle shopStyle2;
	public Button escalatorAttendant;
	public GameObject attendantText;
	public GameObject blockingBlurb;
	public GameObject questionBlurb;
	public GameObject ABlurb;
	public GameObject BBlurb;

	public void Start() {
		controller = GlobalController.GetInstance();
		prizeController = PrizeController.GetInstance();
		escalatorAttendantController = EscalatorAttendantController.GetInstance();

		bool showAttendant = !escalatorAttendantController.IsAnswered(upperFloorName);

		if (escalator != null) {
			escalator.interactable = shopsBattled() && !showAttendant;
		}

		if (escalatorAttendant != null) {
			escalatorAttendant.gameObject.SetActive(showAttendant);
		}
	}

	public void DisplayAttendantText() {
		attendantText.gameObject.SetActive(true);
		
		if (shopsBattled()) {
			showQuestionText();
		} else {
			showBlockingText();
		}
	}

	public void AnswerQuestion(bool first) {
		escalatorAttendantController.AddAnswer(upperFloorName, first);
		questionBlurb.gameObject.SetActive(false);
		if (first)
			ABlurb.gameObject.SetActive(true);
		else
			BBlurb.gameObject.SetActive(true);
	}

	public void LoadUpperFloor() {
		if (!string.IsNullOrEmpty(upperFloorName)) {
			controller.Forward(upperFloorName);
		}
	}

	private bool shopsBattled() {
		return prizeController.ShopBattled(shopStyle1)
			&& prizeController.ShopBattled(shopStyle2);
	}

	private void showBlockingText() {
		blockingBlurb.gameObject.SetActive(true);
	}

	private void showQuestionText() {
		questionBlurb.gameObject.SetActive(true);
	}
	
}
