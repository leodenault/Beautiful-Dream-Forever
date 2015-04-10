using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Dialogue : MonoBehaviour {

	/* Class for displaying static text blurbs (namely entering shops) */

	public DialogueManager.DialogueCharacterEnum Character;
	public DialogueManager.DialogueEventEnum Event;
	public Text blurb;

	private DialogueManager dialogueManager;

	// Use this for initialization
	void Start () {
		dialogueManager = DialogueManager.GetInstance();
		blurb.text = dialogueManager.GetResponseText(Character, Event);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
