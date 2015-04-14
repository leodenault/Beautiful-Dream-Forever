using UnityEngine.UI;

public class QuestionDialogue : Dialogue {

	public Text answerAText;
	public Text answerBText;

	public new void Start() {
		base.Start();
		answerAText.text = dialogueManager.GetResponseText(Character, DialogueManager.DialogueEventEnum.AText);
		answerBText.text = dialogueManager.GetResponseText(Character, DialogueManager.DialogueEventEnum.BText);
	}
}
