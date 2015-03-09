using System;
using UnityEngine;
using UnityEngine.UI;

public static class Util
{
	private static float PAD = 0.85f;

	public static float computeScale(float width1, float width2, float height1, float height2, float pad)
	{
		float wScale = width1 / width2;
		float hScale = height1 / height2;

		return Math.Min(wScale, hScale) * pad;
	}

	public static void ScaleImageToMaxDimensions(Image image, Sprite sprite, float maxWidth, float maxHeight) {
		ScaleImageToMaxDimensions(image, sprite, maxWidth, maxHeight, PAD);
	}

	public static void ScaleImageToMaxDimensions(Image image, Sprite sprite, float maxWidth, float maxHeight, float pad) {
		float spriteWidth = sprite.rect.width;
		float spriteHeight = sprite.rect.height;
		float scale = Util.computeScale(maxWidth, spriteWidth, maxHeight, spriteHeight, pad);

		image.sprite = sprite;
		image.rectTransform.sizeDelta = new Vector2(spriteWidth * scale, spriteHeight * scale);
	}
}
