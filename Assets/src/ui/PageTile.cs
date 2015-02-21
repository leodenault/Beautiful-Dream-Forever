using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class PageTile : MonoBehaviour {
	private static float ITEM_WIDTH = 42.0f;
	private static float ITEM_HEIGHT = 35.0f;

	public Image clothingPreview;

	private ClothingData clothing;
	public ClothingData Clothing {
		get { return clothing; }
		set {
			this.clothing = value;
			Sprite sprite = Resources.Load<Sprite>(clothing.Path);
			clothingPreview.sprite = sprite;

			float spriteWidth = sprite.rect.width;
			float spriteHeight = sprite.rect.height;
			float scale = Util.computeScale(ITEM_WIDTH, spriteWidth, ITEM_HEIGHT, spriteHeight);

			clothingPreview.rectTransform.sizeDelta = new Vector2(spriteWidth * scale, spriteHeight * scale);
			clothingPreview.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		}
	}

	public void SetClothing(ClothingData clothing) {
		
	}
}
