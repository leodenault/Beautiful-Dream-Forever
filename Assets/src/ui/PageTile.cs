using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class PageTile : MonoBehaviour {
	private static float ITEM_WIDTH = 42.0f;
	private static float ITEM_HEIGHT = 35.0f;
	private static float SCALE_MODIFIER = 0.85f;

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
			float scale = computeScale(ITEM_WIDTH, spriteWidth, ITEM_HEIGHT, spriteHeight);

			clothingPreview.rectTransform.sizeDelta = new Vector2(spriteWidth * scale, spriteHeight * scale);
			clothingPreview.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		}
	}

	private float computeScale(float width1, float width2, float height1, float height2)
	{
		float wScale = width1 / width2;
		float hScale = height1 / height2;

		return Math.Min(wScale, hScale) * SCALE_MODIFIER;
	}

	public void SetClothing(ClothingData clothing) {
		
	}
}
