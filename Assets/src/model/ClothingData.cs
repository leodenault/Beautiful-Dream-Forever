using UnityEngine;
using System.Collections;

public class ClothingData {
	public enum ClothingSlot {
		WIG, TOP, BOTTOM, DRESS, SHOES, ACCESSORY
	}

	public enum ClothingStyle {
		IDEALIST, HARDCORE, ATHLETIC, UNIFORM, FORMAL, COSPLAY, PREPPY, HIPSTER
	}

	public enum ClothingEssence {
		CUTE, COOL, CLASSY
	}

	public enum ClothingTexture {
		PVC, METAL, DENIM, COTTON, PATTERN, LEATHER
	}
	
	public string Name;
	public string Path;
	public string Description;
	public Vector2 Location;
	public ClothingSlot Slot;
	public ClothingStyle Style;
	public ClothingEssence Essence;
	public ClothingTexture Texture;
}
