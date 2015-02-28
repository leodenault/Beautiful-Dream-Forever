using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ClothingSelection : MonoBehaviour {
	public float maxWidth;
	public float maxHeight;

	public GameObject container;
	public Image clothingPreview;

	private ClothingData clothing;
	public ClothingData Clothing {
		get { return clothing; }
		set {
			if (value == null) {
				sprite = null;
				clothing = null;
				clothingPreview.gameObject.SetActive(false);
			} else {
				this.clothing = value;
				sprite = Resources.Load<Sprite>(clothing.Path);
				Util.ScaleImageToMaxDimensions(clothingPreview, sprite, maxWidth, maxHeight);
				clothingPreview.gameObject.SetActive(true);
			}
		}
	}

	private Sprite sprite;
	public Sprite Sprite {
		get { return sprite; }
	}
}
