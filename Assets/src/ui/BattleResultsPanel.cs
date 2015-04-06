using UnityEngine;
using UnityEngine.UI;

public class BattleResultsPanel : MonoBehaviour {
	private BattleController controller;
	public BattleController Controller {
		set { controller = value; }
	}

	public float prizeSideLength;
	public GameObject resultsPanel;
	public GameObject winMessage;
	public GameObject loseMessage;
	public Image prize;

	public void EndBattle() {
		controller.EndBattle();
		resultsPanel.gameObject.SetActive(true);

		if (controller.IsSuccessful()) {
			float maxWidth = prize.rectTransform.rect.width;
			float maxHeight = prize.rectTransform.rect.height;
			Util.ScaleImageToMaxDimensions(prize, controller.PrizeSprite(), maxWidth, maxHeight, 1.0f);
			winMessage.gameObject.SetActive(true);
		} else {
			loseMessage.gameObject.SetActive(true);
		}
	}

	public void Exit() {
		GlobalController.GetInstance().Back();
	}
}
