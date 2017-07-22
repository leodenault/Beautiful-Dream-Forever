using UnityEngine;

public class ClothingRepresentation {

	private ClothingData clothing;
	public ClothingData Clothing {
		get { return clothing; }
		set {
			if (value == null) {
				sprite = null;
				clothing = null;
			} else {
				clothing = value;
				sprite = Resources.Load<Sprite>(clothing.Path);
			}
		}
	}

	private Sprite sprite;
	public Sprite Sprite {
		get { return sprite; }
	}
}
