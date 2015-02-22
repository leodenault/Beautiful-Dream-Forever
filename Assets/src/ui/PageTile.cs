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
			Util.ScaleImageToMaxDimensions(clothingPreview, sprite, ITEM_WIDTH, ITEM_HEIGHT);
			clothingPreview.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		}
	}

	public void SetClothing(ClothingData clothing) {
		
	}
}
