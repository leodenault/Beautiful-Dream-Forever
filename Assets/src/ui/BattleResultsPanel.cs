using UnityEngine;
using UnityEngine.UI;

public class BattleResultsPanel : MonoBehaviour {
	private BattleController controller;
	public BattleController Controller {
		set { controller = value; }
	}
	private DialogueManager dialogueManager;

	public float prizeSideLength;
	public GameObject blurbObject;
	public Text blurb;
	public Text money;
	public GameObject resultsPanel;
	public GameObject winPrizeMessage;
	public GameObject winMoneyMessage;
	public GameObject loseMessage;
	public Image prize;

	public void EndBattle() {
		controller.EndBattle();

		resultsPanel.gameObject.SetActive(true);
		if (controller.IsSuccessful()) {
			if (controller.WonPrize()) {
				float maxWidth = prize.rectTransform.rect.width;
				float maxHeight = prize.rectTransform.rect.height;
				Util.ScaleImageToMaxDimensions(prize, controller.PrizeSprite(), maxWidth, maxHeight, 1.0f);
				winPrizeMessage.gameObject.SetActive(true);
			} else {
				money.text = controller.MoneyWon.ToString();
				winMoneyMessage.gameObject.SetActive(true);
			}
		} else {
			loseMessage.gameObject.SetActive(true);
		}
	}

	public void ShowResultsBlurb() {
		blurb.text = controller.GetResultsBlurb();
		blurbObject.gameObject.SetActive(true);
	}
	
}
