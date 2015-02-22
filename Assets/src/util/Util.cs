using System;
using UnityEngine;
using UnityEngine.UI;

public static class Util
{
	private static float SCALE_MODIFIER = 0.85f;

	public static float computeScale(float width1, float width2, float height1, float height2)
	{
		float wScale = width1 / width2;
		float hScale = height1 / height2;

		return Math.Min(wScale, hScale) * SCALE_MODIFIER;
	}

	public static void ScaleImageToMaxDimensions(Image image, Sprite sprite, float maxWidth, float maxHeight) {
		float spriteWidth = sprite.rect.width;
		float spriteHeight = sprite.rect.height;
		float scale = Util.computeScale(maxWidth, spriteWidth, maxHeight, spriteHeight);

		image.sprite = sprite;
		image.rectTransform.sizeDelta = new Vector2(spriteWidth * scale, spriteHeight * scale);
	}
}
