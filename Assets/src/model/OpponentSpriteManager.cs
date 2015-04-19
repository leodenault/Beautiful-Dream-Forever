using System;
using System.Collections.Generic;
using UnityEngine;

public class OpponentSpriteManager {

	public class Entry {
		public string Opponent;
		public string Path;
	}

	private static OpponentSpriteManager INSTANCE;
	private static string FILE = "data/opponentImages";
	private IDictionary<ClothingData.ClothingStyle, Sprite> shopkeeperSprites;
	private IDictionary<DialogueManager.DialogueCharacterEnum, Sprite> shopperSprites;

	private OpponentSpriteManager() {
		shopkeeperSprites = new Dictionary<ClothingData.ClothingStyle, Sprite>();
		shopperSprites = new Dictionary<DialogueManager.DialogueCharacterEnum, Sprite>();
		organize(Util.LoadXmlFile<Entry[]>(FILE));
	}

	public static OpponentSpriteManager GetInstance() {
		if (INSTANCE == null) {
			INSTANCE = new OpponentSpriteManager();
		}

		return INSTANCE;
	}

	public Sprite FetchSprite(ClothingData.ClothingStyle style) {
		return shopkeeperSprites[style];
	}

	public Sprite FetchSprite(DialogueManager.DialogueCharacterEnum character) {
		return shopperSprites[character];
	}

	private void organize(Entry[] entries) {
		foreach (Entry entry in entries) {
			Sprite sprite = Resources.Load<Sprite>(entry.Path);

			if (sprite == null) {
				Debug.LogError(string.Format("Could not load battle screen image for {0} opponent because the image file doesn't exist", entry.Opponent));
			} else {
				try {
					ClothingData.ClothingStyle style = (ClothingData.ClothingStyle)Enum.Parse(typeof(ClothingData.ClothingStyle), entry.Opponent);
					shopkeeperSprites.Add(style, sprite);
				}
				catch (ArgumentException e1) {
					try {
						DialogueManager.DialogueCharacterEnum style = (DialogueManager.DialogueCharacterEnum)Enum.Parse(typeof(DialogueManager.DialogueCharacterEnum), entry.Opponent);
						shopperSprites.Add(style, sprite);
					}
					catch (ArgumentException e2) {
						Debug.LogError(string.Format("Could not load battle screen image for {0} opponent because {0} doesn't exist as a character", entry.Opponent));
						continue;
					}
				}
			}
		}
	}
}
