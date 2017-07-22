using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager {
	
	public enum DialogueCharacterEnum { 
		ATTENDANT_BISHOP, ATTENDANT_CHESSMAN, ATTENDANT_KNIGHT, ATTENDANT_ROOK, 
		BOSS_RED_QUEEN,
		NPC_BUSINESSPERSON, NPC_CATGIRL, NPC_CHEMIST, NPC_FLOWERS, NPC_GALLANT, NPC_GAS_MASK, NPC_GYM_RAT, NPC_MUSIC_PERSON,
		SHOPKEEPER_ATHLETIC, SHOPKEEPER_COSPLAY, SHOPKEEPER_FORMAL, SHOPKEEPER_HARDCORE, SHOPKEEPER_HIPSTER, SHOPKEEPER_IDEALIST, SHOPKEEPER_PREPPY, SHOPKEEPER_UNIFORM,
		NONE
	}
	
	public enum DialogueEventEnum {
		Battle, EnterShop, PlayerWin, PlayerLose, CheckFail, CheckPass, AnswerA, AnswerB, AText, BText, NONE
	}

	private static DialogueManager INSTANCE;
	private static string FILE = "data/dialogue";
	private XmlDocument dialogueDoc;

	private DialogueManager() {
		TextAsset dialogueAsset = (TextAsset) Resources.Load(FILE, typeof(TextAsset));
		dialogueDoc = new XmlDocument();
		dialogueDoc.LoadXml(dialogueAsset.text);
	}

	public static DialogueManager GetInstance() {
		if (INSTANCE == null) {
			INSTANCE = new DialogueManager();
		}
		return INSTANCE;
	}

	public string GetResponseText(DialogueCharacterEnum enCharacter, DialogueEventEnum enEvent) {
		string sCharacter = enCharacter.ToString();
		string sEvent = enEvent.ToString();
		XmlNode response = dialogueDoc.SelectSingleNode(string.Format("//DialogueObject[Character='{0}']/Responses/{1}",
		                                                  sCharacter, sEvent));
		return response.InnerText;
	}

}
