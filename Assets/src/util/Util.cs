using System;

public static class Util
{
	private static float SCALE_MODIFIER = 0.85f;

	public static float computeScale(float width1, float width2, float height1, float height2)
	{
		float wScale = width1 / width2;
		float hScale = height1 / height2;

		return Math.Min(wScale, hScale) * SCALE_MODIFIER;
	}
}
