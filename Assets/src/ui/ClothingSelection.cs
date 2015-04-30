using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ClothingSelection : MonoBehaviour {
	public float maxWidth;
	public float maxHeight;

	public GameObject container;
	public Image clothingPreview;

	private ClothingRepresentation representation = new ClothingRepresentation();

	public ClothingData Clothing {
		get { return representation.Clothing; }
		set {
			if (value == null) {
				representation.Clothing = null;
				clothingPreview.gameObject.SetActive(false);
			} else {
				representation.Clothing = value;
				Util.ScaleImageToMaxDimensions(clothingPreview, representation.Sprite, maxWidth, maxHeight);
				clothingPreview.gameObject.SetActive(true);
			}
		}
	}

	public Sprite Sprite {
		get { return representation.Sprite; }
	}
}
